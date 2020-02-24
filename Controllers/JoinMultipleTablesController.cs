using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data;
using Microsoft.Data.SqlClient;
using Join_Tables_SP.Models;

namespace Join_Tables_SP.Controllers
{
    public class JoinMultipleTablesController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = jointables();
            return View();
        }

        public static List<JoinTableClass> jointables()
        {
            List<JoinTableClass> jt = new List<JoinTableClass>();
            string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CityData;Integrated Security=True";

            using(SqlConnection sqlconn = new SqlConnection(connection))
            {
                using (SqlCommand sqlcomm = new SqlCommand("Jointables", sqlconn))
                {
                    sqlconn.Open();
                    sqlcomm.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader sdr = sqlcomm.ExecuteReader();
                    while(sdr.Read())
                    {
                        JoinTableClass jtc = new JoinTableClass();
                        jtc.Cname = sdr["Cname"].ToString();
                        jtc.Sname = sdr["Sname"].ToString();
                        jtc.Cityname = sdr["Cityname"].ToString();
                        jt.Add(jtc);
                    }
                }
                return jt;
            }
        }
    }
}