using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Company
    {
        public static ML.Result GetCompany()
        {

            ML.Result result = new ML.Result();

            using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = context;
                cmd.CommandText = "CompanyGetAll";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (DataRow row in dt.Rows)
                    {
                        ML.Company company = new ML.Company();

                        company.IdCompany = (int)row[0];
                        company.NombreCompany = row[1].ToString();

                        result.Objects.Add(company);

                        result.Correct = true;
                    }
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No hay registros";
                }
            }
            return result;
        }
    }
}
