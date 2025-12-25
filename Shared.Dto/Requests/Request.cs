using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Shared.Dto.Requests;

public abstract class Request
{
    public string Type
    {
        get { 
            var attributes = GetType().GetCustomAttributes(typeof(RequestTypeAttribute), true); return attributes.Length > 0 
                ? ((RequestTypeAttribute)attributes[0]).Name 
                : Regex.Replace(GetType().Name.Replace("Request", string.Empty), "(?<=[a-z])(?=[A-Z])", " ");
        }
    }   

    [JsonIgnore]
    public string DefaultErrorMessage => $"An error occurred while executing the {Type} request.";

    [JsonIgnore]
    public string DefaultSuccessMessage => $"The {Type} request was executed successfully.";
}
