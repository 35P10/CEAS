namespace Domain.Core;

public class SyntaxStatus
{
    public bool IsOk {get; set;}
    public int Errors {get; set;} = 0;
    public int Obs {get; set;} = 0;
    public string? Msg {get; set;}
}
