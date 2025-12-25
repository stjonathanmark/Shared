namespace Shared.Dto.Responses;

public class Response 
{
    public Response() { }

    public Response(bool successful, string message = "", Dictionary<string, string>? data = null)
    {
        Successful = successful;
        Message = message;
        Data = data ?? [];
    }

    protected Response(Response response)
    {
        Successful = response.Successful;
        Message = response.Message;
        Data = response.Data;
    }

    public bool Successful { get; set; }

    public string Message { get; set; } = string.Empty;

    public Dictionary<string, string> Data { get; set; } = [];
}
