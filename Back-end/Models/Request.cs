namespace Back_end.Models
{
    public class Request
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } //prašymas/pasiūlymas/skundas
        public string Message { get; set; }              
        public string Status { get; set; } //pateiktas/įvykdytas

    }
}
