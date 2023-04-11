using System;
namespace word_finder
{
    public class WordFinder
    {
        private char[,]? _matrix;

        public WordFinder(IEnumerable<string> matrix)
        {
            if (matrix.Count() > 64) return;
            var rowCount = matrix.Count();
            var colCount = matrix.First().Length;
            _matrix = new char[rowCount, colCount];
            for (var row = 0; row < rowCount; row++)
            {
                var chars = matrix.ElementAt(row).ToCharArray();
                for (var col = 0; col < colCount; col++)
                {
                    _matrix[row, col] = chars[col];
                }
            }
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var wordCounts = new Dictionary<string, int>();
            foreach (var word in wordstream.Distinct())
            {
                var horizontalCount = CountOccurrences(word, CheckHorizontal);
                var verticalCount = CountOccurrences(word, CheckVertical);
                var count = horizontalCount + verticalCount;
                if (count > 0)
                {
                    wordCounts[word] = count;
                }
            }
            return wordCounts.OrderByDescending(kv => kv.Value).Select(kv => kv.Key).Take(10);
        }

        private int CountOccurrences(string word, Func<string, int, int, int, int, bool> checkDirection)
        {
            var count = 0;
            var rowCount = _matrix.GetLength(0);
            var colCount = _matrix.GetLength(1);
            for (var row = 0; row < rowCount; row++)
            {
                for (var col = 0; col < colCount; col++)
                {
                    if (checkDirection(word, row, col, rowCount, colCount) && CheckWord(word, row, col))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private bool CheckHorizontal(string word, int row, int col, int rowCount, int colCount)
        {
            return col + word.Length <= colCount;
        }

        private bool CheckVertical(string word, int row, int col, int rowCount, int colCount)
        {
            return row + word.Length <= rowCount;
        }

        private bool CheckWord(string word, int row, int col)
        {
            var length = word.Length;
            for (var i = 0; i < length; i++)
            {
                if (word[i] != _matrix[row, col])
                {
                    return false;
                }
                if (i < length - 1)
                {
                    if (CheckOutOfBounds(row + 1, col + 1))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool CheckOutOfBounds(int row, int col)
        {
            return row >= _matrix.GetLength(0) || col >= _matrix.GetLength(1);
        }
    }

}

