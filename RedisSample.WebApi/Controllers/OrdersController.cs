using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisSample.WebApi.Models;

namespace RedisSample.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IDistributedCache distributedCache;

    public OrdersController(IDistributedCache distributedCache)
    {
        this.distributedCache = distributedCache;
    }

    [HttpGet("redis")]
    public async Task<IActionResult> GetAllOrdersUsingRedisCache()
    {
        var cacheKey = "orderList";
        var serializeOrderList = string.Empty;
        var result = new Result();

        var redisOrderList = await distributedCache.GetAsync(cacheKey);

        if (redisOrderList != null)
        {
            serializeOrderList = Encoding.UTF8.GetString(redisOrderList);
            result = JsonConvert.DeserializeObject<Result>(serializeOrderList);
        }
        else
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://northwind.netcore.io/orders.json");

            serializeOrderList = await response.Content.ReadAsStringAsync();
            redisOrderList = Encoding.UTF8.GetBytes(serializeOrderList);

            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            await distributedCache.SetAsync(cacheKey, redisOrderList, options);

            result = JsonConvert.DeserializeObject<Result>(serializeOrderList);
        }

        return Ok(result);
    }
}