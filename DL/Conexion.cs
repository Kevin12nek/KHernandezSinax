using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Conexion
    {
        public static string Get()
        {
            string Conexion = "Data Source=ING-KEVIN\\SQLEXPRESS;Initial Catalog=RetailsOrder;Persist Security Info=True;User ID=Admin;Password=123456789";
            return Conexion;
        }
    }
}
