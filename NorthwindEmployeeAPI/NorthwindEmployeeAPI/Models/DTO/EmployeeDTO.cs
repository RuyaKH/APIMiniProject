using NorthwindEmployeeAPI.HATEOAS;

namespace NorthwindEmployeeAPI.Models.DTO
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }

        public string LastName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string? Title { get; set; }
        public int NumberOfOrders { get; set; }
        public decimal TotalMoneyMade { get; set; }
        public decimal AverageSale { get; set; }

        public List<LinkDTO> linkDto { get; set; } = new List<LinkDTO>();

    }
}
