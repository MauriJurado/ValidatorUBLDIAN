using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekomercio.Entidades.Validador
{
    public class clsInformacionEventos
    {
        public string cNombreEvento { get; set; }
        public string cNSU  { get; set; }
        public string cCodigoEvento { get; set; } 
        //public string dFechaEmision { get; set; }
        public string dFechaRecepcion { get; set; }
        public string dFechavalidacion { get; set; }
        public string cDescripcionEvento { get; set; }
        public string cResponseXMLB64 { get; set; }
    }
}
