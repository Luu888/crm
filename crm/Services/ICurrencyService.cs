using crm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.Services
{
    public interface ICurrencyService
    {
        IEnumerable<Currency> GetAll();
        public void Update();
    }
}
