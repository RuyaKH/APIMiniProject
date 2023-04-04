namespace NorthwindEmployeeAPI.Services
{
    public interface IOrderService <T> : INorthwindService <T> where T : class
    {
        Task<string?> HighestQuantityOfOrderAsync();

        Task<List<object>> SalesByMonthAsync();
    }
}
