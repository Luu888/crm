using crm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace crm.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly CurrencyContext _context;
        public CurrencyService(CurrencyContext context)
        {
            _context = context;
        }
        public IEnumerable<Currency> GetAll()
        {
            var results = _context.Currency.ToList();

            return results;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Update()
        {
            WebClient client = new WebClient();
            var strApi = client.DownloadString("https://api.exchangerate.host/latest?base=USD");
            dynamic dobj = JsonConvert.DeserializeObject<dynamic>(strApi);
            foreach(var item in _context.Currency)
            {
                if (item.Is_Sync == true)
                {
                    Console.WriteLine($"{item.Rate} - {dobj["rates"][item.Symbol]}");
                    item.Rate = dobj["rates"][item.Symbol];
                    item.Rate = Math.Round(item.Rate, 3);
                    item.Updated_at = DateTime.Now;
                }
            }
            _context.SaveChanges();
        }

    }
}
