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
    public class RedisHashTypeController : BaseController
    {
       
        private string listkey = "hashKey";

        public RedisHashTypeController(RedisService redisService) : base(redisService)
        {
           
        }

        public IActionResult Index()
        {
            Dictionary<string,string> namesList = new Dictionary<string, string>();
            if (db.KeyExists(listkey))
            {
                db.HashGetAll(listkey).ToList().ForEach(x => { namesList.Add(x.Name.ToString(),x.Value); });
            }
            return View(namesList);
        }

        [HttpPost]
        public IActionResult Add(string key , string value)
        {
            db.HashSet(listkey, key, value);
            return RedirectToAction("Index");

        }


        public IActionResult Remove(string name)
        {
            db.HashDelete(listkey, name);
            return RedirectToAction("Index");

        }
    }
}
