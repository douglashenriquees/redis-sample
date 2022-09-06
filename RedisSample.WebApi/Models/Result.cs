namespace RedisSample.WebApi.Models;

public class Result
{
    public ICollection<Wrapper> results { get; set; } = new List<Wrapper>();
}