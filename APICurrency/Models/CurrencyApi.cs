using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICurrency.Models
{
    public class CurrencyApi
    {
        public int Id { get; set; }
        public String Symbol { get; set; }
        public double Rate { get; set; }
        public DateTime Updated_At { get; set; }

        public CurrencyApi(int id, string symbol, double rate, DateTime updated_At)
        {
            Id = id;
            Symbol = symbol;
            Rate = rate;
            Updated_At = updated_At;
        }
        public CurrencyApi()
        {

        }
    }
}
