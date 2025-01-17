using System.ComponentModel.DataAnnotations;

namespace TicketStationMVC.ViewModels.Cart
{
    public class AllCartItemsVM : CartVM
    {
        [Display(Name="Email of user")]
        public string OwnerEmail { get; set; }
        [Display(Name ="Username")]
        public string OwnerUsername { get; set; }
    }
}
