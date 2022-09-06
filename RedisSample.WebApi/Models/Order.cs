namespace RedisSample.WebApi.Models;

public class Order
{
    public int id { get; set; }

    public string customerId { get; set; } = string.Empty;

    public int employeeId { get; set; }

    public DateTime? orderDate { get; set; }

    public DateTime? requiredDate { get; set; }

    public int shipVia { get; set; }

    public decimal freight { get; set; }

    public string shipName { get; set; } = string.Empty;

    public string shipAddress { get; set; } = string.Empty;

    public string shipCity { get; set; } = string.Empty;

    public string shipPostalCode { get; set; } = string.Empty;

    public string shipCountry { get; set; } = string.Empty;
}