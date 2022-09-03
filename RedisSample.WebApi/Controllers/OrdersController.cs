using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisSample.WebApi.Models;

namespace RedisSample.WebApi.Controllers;

[ApiController]
[Route("[conroller]")]
public class OrdersController : ControllerBase
{
    private readonly IDistributedCache distributedCache;
    private readonly NorthwindContext context;

    public OrdersController(IDistributedCache distributedCache, NorthwindContext context)
    {
        this.distributedCache = distributedCache;
        this.context = context;
    }

    [HttpGet("redis")]
    public async Task<IActionResult> GetAllOrdersUsingRedisCache()
    {
        var cacheKey = "orderList";
        var serializeOrderList = string.Empty;
        var orderList = new List<Order>();

        var redisOrderList = await distributedCache.GetAsync(cacheKey);

        if (redisOrderList != null)
        {
            serializeOrderList = Encoding.UTF8.GetString(redisOrderList);
            orderList = JsonConvert.DeserializeObject<List<Order>>(serializeOrderList);
        }
        else
        {
            orderList = await context?.Orders?.ToListAsync();
            serializeOrderList = JsonConvert.SerializeObject(orderList);
            redisOrderList = Encoding.UTF8.GetBytes(serializeOrderList);

            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            await distributedCache.SetAsync(cacheKey, redisOrderList, options);
        }

        return Ok(orderList);
    }
}