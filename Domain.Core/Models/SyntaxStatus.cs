namespace Domain.Core.Models
{
    public class SyntaxStatus
    {
        public bool IsOk {get; set;} = true;
        public List<string> ErrorMsg {get; set;} = new List<string>();
        public List<string> ObsMsg {get; set;} = new List<string>();
    }
}
