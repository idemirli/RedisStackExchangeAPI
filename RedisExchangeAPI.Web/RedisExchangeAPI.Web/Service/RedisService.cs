using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeAPI.Web.Service
{
    public class RedisService
    {
        private readonly string _redisHost;
        private readonly string _redisPort;

        private ConnectionMultiplexer _redis;  //Redis le haberleşecek ana sınıfımız.
        public IDatabase db { get; set; }

        //Redis Server'ı uygulamamın herhangi bir yerinde haberleşeceğim zaman burayı kullanacağım.
        //İlk olarak appsettingJson içerisindeki datayı alabilmem için IConfiguration interface'ini bu class'ın constructor'ında almam lazım.
        //"Host" ve "Report"u alacağım için değişiken tanımladık
        //Veritabanı ile haberleşeceğimden dolayı IDatabase gerekiyor.
        //Redis Server'ım hazır olduktan sonra uygulamam ilk ayağa kalktığında direkt Redis server'ımla haberleşeceğim. (connect methodu)
        //Redis Server'ımızla haberleşecek Class'ımız ConnectionMultiplexer classımızı tanımladık
        //Bu API üzerinden Redis Desktop Manager'da istediğimiz veritabanlarından birisiyle çalışabiliriz.
        //RedisService'ı kullanabilmek için yani Dependency Injection olarak kullanabilmek için servis olarak eklemem gerekiyor. Startup.cs içinde ConfigureServices içinde Singleton olarak ekliyorum.

        //Redis in implementasyonuyla alakalı farklı yollarda var.

        public RedisService(IConfiguration configuration)
        {
            _redisHost = configuration["Redis:Host"];
            _redisPort = configuration["Redis:Port"];

        }

        public void Connect()
        {
            var configString = $"{_redisHost}:{_redisPort}";

            _redis = ConnectionMultiplexer.Connect(configString);
        }

        public IDatabase GetDb(int db)
        {
            return _redis.GetDatabase(db);  //RDM de 0 dan 16 ya kadar database'den hangisini kullanacağıma karar veriyorum
        }
    }
}
