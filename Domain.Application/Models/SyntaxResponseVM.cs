namespace Domain.Application.Models
{
    public class SyntaxResponseVM
    {
        public bool IsOk {get; set;}
        public int Errors {get; set;} = 0;
        public int Obs {get; set;} = 0;
        public List<string> ErrorMsg {get; set;}
        public List<string> ObsMsg {get; set;}
    }
}
