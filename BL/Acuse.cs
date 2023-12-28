using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Acuse
    {
        public static ML.Result GetReport(string FechaBusqueda, string NombreBusqueda, string FechaB)
        {

            ML.Result result = new ML.Result();

            using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = context;
                cmd.CommandText = "tblSeguimientoAcRecibovsFacturaClienteGETALL";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NombreCompany", FechaBusqueda);
                cmd.Parameters.AddWithValue("@NombreBusqueda", NombreBusqueda);
                //cmd.Parameters.AddWithValue("@FechaBusqueda", FechaB);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (DataRow row in dt.Rows)
                    {
                        ML.AcuRe acuRe = new ML.AcuRe();

                        acuRe.IdSeg = (int)row[0];
                        acuRe.SegCompany = row[1].ToString();
                        acuRe.SegClient = row[2].ToString();
                        acuRe.RouteFileOriginal = row[3].ToString();
                        acuRe.FileNameComplete = row[4].ToString();
                        acuRe.RouteFileSape = row[5].ToString();
                        acuRe.Status = row[6].ToString();
                        acuRe.CreationDate = (DateTime)row[7];


                        result.Objects.Add(acuRe);

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

