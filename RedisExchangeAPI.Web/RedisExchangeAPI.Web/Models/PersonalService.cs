using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeAPI.Web.Models
{
    public static  class PersonalService
    {
        public static List<Personal> lstPersonal = new List<Personal>();
        

        public static void AddPersonal(Personal personal)
        {
            lstPersonal.Add(personal);
        }
    }
}
