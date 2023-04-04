namespace NorthwindEmployeeAPI.Models.DTO
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }

        public string LastName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string? Title { get; set; }
        public int NumberOfOrders { get; set; }
        public int Metric2 { get; set; }

    }
}
