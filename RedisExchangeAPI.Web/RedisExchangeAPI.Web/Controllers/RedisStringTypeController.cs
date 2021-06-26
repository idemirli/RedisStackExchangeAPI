using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Service;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeAPI.Web.Controllers
{
    public class RedisStringTypeController : Controller
    {
        //Controllerda kullanmak istiyorsak Inject işlemi yapıyoruz.
        private readonly RedisService _redisService;
        private readonly IDatabase db;

        public RedisStringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }
        public IActionResult Index()
        {

            db.StringSet("name", "ibrahim");
            db.StringSet("ziyaretci", 100);

            return View();
        }

        public IActionResult Show()
        {
            //Redis-cli komutlarını ezberlemek yerine burdaki methodları kullanıyoruz.
            db.StringIncrement("ziyaretci", 10);  //1 arttırdık
            db.StringDecrementAsync("ziyaretci", 1).Wait();
            var nameRange = db.StringGetRange("name", 0, 2);
            var nameLength = db.StringLength("name");


            var ziyaretciValue = db.StringGet("ziyaretci");
            if (ziyaretciValue.HasValue)
            {
                ViewBag.ziyaretci = ziyaretciValue.ToString();
            }
            var NameValue = db.StringGet("name");
            if (NameValue.HasValue)
            {
                ViewBag.name = NameValue.ToString();
            }
            return View();
        }
    }
}
