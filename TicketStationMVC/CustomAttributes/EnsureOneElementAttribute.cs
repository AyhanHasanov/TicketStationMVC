using System.ComponentModel.DataAnnotations;

namespace TicketStationMVC.CustomAttributes
{
    public class EnsureOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is List<int> list)
            {
                return list.Count > 0;
            }

            return false;
        }
    }
}
