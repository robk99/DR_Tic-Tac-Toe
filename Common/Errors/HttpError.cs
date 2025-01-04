using System.Text.Json;

namespace DR_Tic_Tac_Toe.Common.Errors
{
    public class HttpError
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
