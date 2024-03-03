namespace backend.ApiContracts;

public class ErrorContract
{
    public ErrorContract(string message = "")
    {
        Message = message;
    }

    public string Message { get; set; }
}