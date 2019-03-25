using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Ekomercio.Entidades.Validador
{
    public class clsXML
    {
        public string zXMLB64 { get; set;}
    }

    public class clsCUFE
    {
        public string cCUFE { get; set; }
    }

    public class clsReponse
    {
        public string cCUFE {get; set;}
        public string cNitEmisor{get; set;}
        public string cNitReceptor { get; set; }
        public string cResponseB64 { get; set; }
    }

    public static class clsValidadores
    {
        public static string cXSD { get; set; }
        public static string cXSLT { get; set; }
    }

    public static class clsXmlReaderSettings
    {
        public static XmlReaderSettings oSettings { get; set; }
    }

    
}
