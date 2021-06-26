using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Models;
using RedisExchangeAPI.Web.Service;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeAPI.Web.Controllers
{
    public class RedisSortedSetController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        private string listkey = "sortedListKey";


        //Sorted Set'in normal Set'ten tek farkı Set içerisinde array'e bir item eklerken belirttiğimiz score'a göre item'ları sıralı ekleyebiliriz.
        public RedisSortedSetController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(3);
        }
        public IActionResult Index()
        {
            SortedSet<string> nameList = new SortedSet<string>();
            if (db.KeyExists(listkey))
            {
                //db.SortedSetScan(listkey).ToList().ForEach(x => {
                //    nameList.Add(x.ToString());
                //});
                db.SortedSetRangeByRank(listkey,order : Order.Ascending).ToList().ForEach(x => {
                    nameList.Add(x.ToString());
                });
            }
            return View(nameList);
        }

        [HttpPost]
        public IActionResult Add(Personal personal)
        {
            //if (!db.KeyExists(listkey))
            //{
            //    db.KeyExpire(listkey, DateTime.Now.AddMinutes(2));  //Sliding time vermiş gibi kullanabiliriz
            //}
            db.KeyExpire(listkey, DateTime.Now.AddMinutes(2));
            db.SortedSetAdd(listkey,personal.Name,personal.Score);
            return RedirectToAction("Index");

        }


        public IActionResult Remove(string name)
        {
            db.SortedSetRemove(listkey, name);
            return RedirectToAction("Index");

        }
    }
}
