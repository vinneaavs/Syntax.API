namespace Syntax.API.Models
{
    public class ErrorResponse
    {
        public List<string> Errors { get; set; }
        public ErrorResponse()
        {
            Errors = new List<string>();
        }
    }

}
