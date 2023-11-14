namespace FrontEnd.Models
{
    public class SintaxisModel
    {
        public bool isOk { get; set; }
        public int errors { get; set; } = 0;
        public int obs { get; set; } = 0;
        public List<string> errorMsg { get; set; } = new List<string>();
        public List<string> obsMsg { get; set; } = new List<string>();
    }
}
