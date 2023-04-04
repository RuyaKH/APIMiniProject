using NorthwindEmployeeAPI.Data.Repositories;

namespace NorthwindEmployeeAPI.Data.Repository
{
    public interface IOrderRepository<T> : INorthwindRepository<T> where T : class
    {



    }
}
