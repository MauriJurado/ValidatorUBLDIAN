using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Ekomercio.Validador.Reglas
{
    public class clsObtenerDatos
    {
        public string clsObtenerNIT(XDocument oXdoc, XElement oXmlDocumentWithoutNs)
        {
            IEnumerable<string> textSegs =
                            from seg in oXmlDocumentWithoutNs.Descendants("CompanyID")
                            select (string)seg;
            string num = textSegs.FirstOrDefault().ToString();

            if (!string.IsNullOrEmpty(num)) return num;
            return "No se pudo recuperar Nit emisor";
        }

        public string clsObtenerCUFE(XDocument oXdoc)
        {
            string cElt = oXdoc.Root.Element("UUID").Value;
            if (string.IsNullOrEmpty(cElt))
            {
                return  "No se pudo obtener CUFE de documento";
            }
            else return cElt;
        }
    }      
}
