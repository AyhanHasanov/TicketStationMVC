namespace TicketStationMVC.ViewModels.Account
{
    public class AccountChangePassVM
    {
        public int Id { get; set; }
        public string OldPass { get; set; }
        public string NewPass { get; set; }
        public string NewPassVerfication { get; set; }
    }
}
