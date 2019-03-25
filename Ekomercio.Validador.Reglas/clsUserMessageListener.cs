using Ekomercio.Entidades.Validador;
using Saxon.Api;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ekomercio.Validador.Reglas
{
    public class UserMessageListener : IMessageListener
    {
        public static List<clsInformacionError> aListErroresXSLT = new List<clsInformacionError>();

        public void Message(XdmNode content, bool terminate, IXmlLocation location)
        {
           // aListErroresXSLT.Clear();
            RegresaError(content, location, ref aListErroresXSLT);
            //Console.Out.WriteLine("MESSAGE terminate=" + (terminate ? "yes" : "no") + " at " + DateTime.Now);
            //Console.Out.WriteLine("From instruction at line " + location.LineNumber +
            //        " of " + location.BaseUri);
            //Console.Out.WriteLine(">>" + content.StringValue);
        }

        public  void RegresaError(XdmNode Mensaje, IXmlLocation Linea, ref List<clsInformacionError> aListErroresXSLT)
        {
            
            clsInformacionErrorXSL aListError = new clsInformacionErrorXSL();
            try
            {
                if (Mensaje.OuterXml.Length > 7 && Mensaje.OuterXml.Contains('['))
                {
                    aListError.nCodigoError = Mensaje.OuterXml.Substring(Mensaje.OuterXml.IndexOf('[') + 1, Mensaje.OuterXml.LastIndexOf(']') - Mensaje.OuterXml.IndexOf('[') - 1).ToString();
                    aListError.cTipoErr = Mensaje.OuterXml.Substring(0, Mensaje.OuterXml.IndexOf(':')).ToString();
                    aListError.cLineaError = Linea.LineNumber.ToString();
                    aListError.cDescripcionError = Mensaje.OuterXml.Substring(Mensaje.OuterXml.LastIndexOf(']') + 2, Mensaje.OuterXml.Length - Mensaje.OuterXml.LastIndexOf(']') - 2);
                    aListErroresXSLT.Add(aListError);
                }
            }
            catch (Exception)
            {
                aListError.cDescripcionError = Mensaje.OuterXml.ToString();
                aListError.cLineaError = Linea.LineNumber.ToString();
                aListErroresXSLT.Add(aListError);
            }

        }
    }


}
