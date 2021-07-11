namespace ShapeDrawer.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; }

        public ErrorResponse(string message)
        {
            this.Message = message;        }
    }
}
