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
    public class RedisListTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        public RedisListTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(1);
        }
        public IActionResult Index()
        {
            List<string> lstNames = new List<string>();
            if (db.KeyExists("names"))
            {
                db.ListRange("names").ToList().ForEach(x=> { lstNames.Add(x.ToString()); });

            }
            return View(lstNames);
        }



       
        public IActionResult Add(string Name)
        {
            db.ListRightPush("names", Name); //tek tek ekler
          return  RedirectToAction("Index");
        }

        public IActionResult Remove(string Name)
        {
            db.ListRemove("names",Name);
            return RedirectToAction("Index");
        }

    }
}
