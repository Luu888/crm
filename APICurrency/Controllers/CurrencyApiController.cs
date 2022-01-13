using APICurrency.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICurrency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyApiController : ControllerBase

    {
        private readonly IConfiguration _configuration;
        public CurrencyApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Authorize]
        public JsonResult Get()
        {
            string query = @"Select Id, Symbol, Rate, Updated_at from dbo.Currency";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CurrencyConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) 
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) 
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                        
                }
            }
            return new JsonResult(table);
        }
    }
}
