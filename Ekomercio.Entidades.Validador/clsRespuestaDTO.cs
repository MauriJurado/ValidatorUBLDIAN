using System;
using System.Collections.Generic;
using System.Text;

namespace Ekomercio.Entidades.Validador
{
    public class clsRespuestaDTO<T>
    {
        public bool lEstado { get; set; }
        public string cNitEmisor { get; set; }
        public string cCUFE { get; set; }      
        public string dFechaEnvio { get; set; }
        public string cMensaje { get; set; }
        public List<clsInformacionError> aInfo { get; set; }
    }

    public class clsResponseDTO<T>
    {
        public bool lEstado { get; set; }
        public string cNitEmisor { get; set; }
        public string cCUFE { get; set; }
        public List<clsInformacionEventos> aInformacionEventos { get; set; }
    }
}
