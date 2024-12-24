namespace ZyphCare.Api.Common.Endpoints;

public class Request
{
    public string? Data { get; set; }    
}

public class Response
{
    public string Health { get; set; } = "Healthy";
}