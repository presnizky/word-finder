using Microsoft.AspNetCore.Mvc;

namespace word_finder.Controllers;

[ApiController]
[Route("[controller]")]
public class WordFinderController : ControllerBase
{

    [HttpPost]
    public IEnumerable<string> FindWords([FromBody] WordFinderRequest request)
    {
        var wordFinder = new WordFinder(request.Matrix);
        return wordFinder.Find(request.Wordstream);
    }

    public class WordFinderRequest
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public IEnumerable<string> Wordstream { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public IEnumerable<string> Matrix { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}