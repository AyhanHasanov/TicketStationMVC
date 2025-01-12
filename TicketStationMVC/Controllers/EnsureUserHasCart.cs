using TicketStationMVC.Data.Entities;
using TicketStationMVC.Data;
using TicketStationMVC.Services.ServiceInterfaces;

namespace TicketStationMVC.Controllers
{
    public static class EnsureUserHasCart
    {
        public static async Task<bool> Ensure(ApplicationDbContext context, IUserService userService, int userId)
        {
            try
            {
                bool doesCurrentUserHaveCart = false;

                if (context.Carts.Count() > 0)
                {
                    doesCurrentUserHaveCart = context.Carts.Where(c => c.OwnerId.Equals(userId)).FirstOrDefault() != null;
                }

                if (!doesCurrentUserHaveCart) //doesnt have cart, create one
                {
                    Cart cart = new Cart();
                    cart.OwnerId = userId;
                    cart.Owner = await userService.GetUserByIdAsync(userId);
                    cart.CartItems = new List<CartItem>();
                    context.Carts.Add(cart);
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
        }
    }
}
