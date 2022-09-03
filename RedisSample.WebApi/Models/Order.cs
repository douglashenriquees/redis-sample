namespace RedisSample.WebApi.Models;

public class Order
{
    public int OrderId { get; set; }

    public char CustomerId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime? RequiredDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public int ShipVia { get; set; }

    public decimal Freight { get; set; }

    public string ShipName { get; set; } = string.Empty;

    public string ShipAddress { get; set; } = string.Empty;

    public string ShipCity { get; set; } = string.Empty;

    public string ShipRegion { get; set; } = string.Empty;
}