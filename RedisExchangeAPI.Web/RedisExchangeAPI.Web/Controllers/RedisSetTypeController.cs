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
    public class RedisSetTypeController : Controller
    {
        //Redis tarafındaki her bir veri tipinin OOP tarafta bir karşılığı vardır.
        //Redis Set 'in karşılığı  HashSet veri tipidir.
        //Bu veri tipi List'e benzer , Redis Set'te olduğu gibi c# ta HashSet'e ekleyeceğiniz veriler sırasız bir şekilde eklenir.
        //Eklenen datalar "Unique" tir.

        private readonly RedisService _redisService;
        private readonly IDatabase db;
        private string listkey = "hashBooks";
        public RedisSetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(2);
        }
        public IActionResult Index()
        {
            HashSet<string> namesList = new HashSet<string>();
            if (db.KeyExists(listkey))
            {
                db.SetMembers(listkey).ToList().ForEach(x => { namesList.Add(x.ToString()); });
            }
            return View(namesList);
        }

        [HttpPost]
        public IActionResult Add(Personal personal)
        {
            //if (!db.KeyExists(listkey))
            //{
            //    db.KeyExpire(listkey, DateTime.Now.AddMinutes(2));  //Sliding time vermiş gibi kullanabiliriz
            //}
            db.KeyExpire(listkey, DateTime.Now.AddMinutes(2));
            db.SetAdd(listkey, personal.Name);
            return RedirectToAction("Index");

        }

      
        public IActionResult Remove(string name)
        {
            db.SetRemove(listkey, name);
            return RedirectToAction("Index");

        }
    }
}
