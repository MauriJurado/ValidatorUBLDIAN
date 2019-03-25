using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekomercio.Entidades.Validador
{
    public class clsInformacionError
    {
        public string nCodigoError { get; set; }  // 100- SCHEMA ----200- XSLT
        public string cDescripcionError { get; set; }
    }
    public class clsInformacionErrorXSL : clsInformacionError
    {
        public string cTipoErr { get; set; }
        public string cLineaError { get; set; }
    }
}
