using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class AcuRe
    {
        public int IdSeg { get; set; }
        public string SegCompany { get; set; }
        public string SegClient { get; set; }
        public string RouteFileOriginal { get; set; }
        public string FileNameComplete { get; set; }
        public string RouteFileSape { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }

        public string FechaBusqueda { get; set; }
        public string NombreBusqueda { get; set; }
        public string FechaB { get; set; }

        public ML.Company Companyes { get; set; }

        public List<Object> AcureS { get; set; }
    }
}
