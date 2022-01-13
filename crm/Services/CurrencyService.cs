using crm.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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
                    if (dobj["rates"][item.Symbol] != null)
                    {
                        Console.WriteLine($"{item.Symbol} - Stary kurs: {item.Rate} - Nowy Kurs: {dobj["rates"][item.Symbol]}");
                        item.Rate = dobj["rates"][item.Symbol];
                        item.Rate = Math.Round(item.Rate, 3);
                        item.Updated_at = DateTime.Now;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{item.Symbol} Nieobsługiwana lub bledny symbol waluty");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
            }
            _context.SaveChanges();
        }

    }
}
