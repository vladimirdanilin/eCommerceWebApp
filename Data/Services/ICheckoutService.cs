namespace eCommerceWebApp.Data.Services
{
    public interface ICheckoutService
    {
        Task GetAddresses(int userId);
    }
}
