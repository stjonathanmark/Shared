namespace Shared.Dto.Requests;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class RequestTypeAttribute : Attribute
{
    public string Name { get; set; } = string.Empty;
}
