using System.ComponentModel.DataAnnotations;

namespace TicketStationMVC.ViewModels.Account
{
    public class AccountChangePassVM
    {
        public int Id { get; set; }

        [DataType(DataType.Password)]
        public string OldPass { get; set; }

        [DataType(DataType.Password)]
        public string NewPass { get; set; }

        [DataType(DataType.Password)]
        public string NewPassVerfication { get; set; }
    }
}
