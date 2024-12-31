namespace Mango.Service.EmailAPI.Models
{
    public class EmailLogger
    {
        public int Id { get; set; }
        public string Enail { get; set; }
        public string Message { get; set; }
        public DateTime? EmailSent { get; set; }
    }
}
