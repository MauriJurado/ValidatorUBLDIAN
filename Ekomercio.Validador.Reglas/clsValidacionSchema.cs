using Ekomercio.Entidades.Validador;
using Saxon.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Ekomercio.Validador.Reglas
{
    public class clsValidacionSchema
    {
        clsInformacionError aInfoError = new clsInformacionError();
        List<string> aErrores = new List<string>();
        clsRespuestaDTO<string> aResponse = new clsRespuestaDTO<string>();
        List<clsInformacionError> aInfo = new List<clsInformacionError>();

        public clsRespuestaDTO<string> clsValidacionEstructura(string cXML) // REALIZA VALIDACION SCHEMA Y CLS REGRESA ESTATUS, MENSAJE Y LISTA DE ERROES
        {
            aResponse.dFechaEnvio = DateTime.Now.ToString("MM/dd/yyyy hh:mm");

            aErrores = clsValidateXMLFromSchema(cXML); // validacion schema
            if (aErrores.Count() > 0)
            {
                aResponse.lEstado = false; // validacion incorrecta

                int ind = 0;       //llenado de lista de errores
                while (ind != aErrores.Count)
                {
                    clsListaErrores clsError = new clsListaErrores();
                    aInfoError = clsError.clsListaErroes("100", aErrores[ind]);
                    aInfo.Insert(ind, aInfoError);
                    ind++;
                }

                aResponse.aInfo = aInfo; //asignacion a respuesta
                if (string.IsNullOrEmpty(aResponse.cMensaje)) aResponse.cMensaje = "Existen errores de Schema(XSD)";
            }  //Schema con error
            else
            {
                aResponse.lEstado = true;
                aResponse.cMensaje = "Validacion schema correcta";

            }

          return aResponse;
        }

        public clsRespuestaDTO<string> clsValidacionXSLT(string cXML)
        {
            aResponse.aInfo = clsValidarXLS(cXML);

            if (aResponse.aInfo.Count > 0) //hay errores de contenido
            {
                aResponse.lEstado = false;
                aResponse.cMensaje = "Existen errores de Contenido(XSL)";
            }
            else
            {
                aResponse.lEstado = true;
                aResponse.cMensaje = "Validacion correcta";
            }
    
            return aResponse;
        }

        ////////////////Clases de validacion de esquema y cls///////////////////////////

        private List<clsInformacionError> clsValidarXLS(string cXML) //realiza validacion de contenido
        {
            XmlReader oReader = XmlReader.Create(new StringReader(cXML));
            List<clsInformacionError> aLstErroresValidacion = new List<clsInformacionError>();
            try
            {
                    //var input = @"input Prueba";
                    // var output = new FileInfo(@"E:\DIAN\Kit Factura Electronica Validacion Previa\XSL\resultado.txt");
                   
                    // Create a Processor instance.
                    Processor processor = new Processor();
                    // Load the source document
                    XdmNode input2 = processor.NewDocumentBuilder().Build(oReader);
                    // Create a transformer for the stylesheet.
                    XsltTransformer transformer = processor.NewXsltCompiler().Compile(new Uri(clsValidadores.cXSLT)).Load();
                    // Set the root node of the source document to be the initial context node
                    transformer.InitialContextNode = input2;
                    // Create a Listener to which messages will be written
                    transformer.MessageListener = new UserMessageListener();
                    // Create a serializer, with output to the standard output stream
                    Serializer res = processor.NewSerializer();
                    MemoryStream ms = new MemoryStream();
                     res.SetOutputStream(ms);
                    try
                    {
                        transformer.Run(res);  //recorrido para valdiacion
                        oReader.Close();
                        oReader.Dispose();
                        return aInfo = UserMessageListener.aListErroresXSLT;  //regresa listra de errores
                        
                     }
                    catch (Exception)  // problemas de lectura
                    {
                        oReader.Close();
                        oReader.Dispose();
                        throw new Exception();
                    } 
            }
            catch (Exception)
            {
                throw;
            }     
        }

        private List<string> clsValidateXMLFromSchema(string cXML)
        {
                List<string> lstErrores = new List<string>();
                //string rutaxml = "Ruta prueba"; 
                //settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
                //settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
                //settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                clsXmlReaderSettings.oSettings.ValidationEventHandler += (o, args) => lstErrores.Add(args.Message);
                // settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
                clsXmlReaderSettings.oSettings.ValidationType = ValidationType.Schema;
                // Create the XmlReader object.
                XmlReader reader = XmlReader.Create(new StringReader(cXML), clsXmlReaderSettings.oSettings);
                // Parse the file. 
                try { 
                    while (reader.Read());
                    reader.Close();
                    reader.Dispose();
                }
                catch (Exception ex)
                {   //Log
                    reader.Close();
                    reader.Dispose();
                    throw ex;
                }

            return lstErrores;
        }

        ///////////////////////Obtencion de datos//////////////////////////////////////////////////////////

        private void dsObtenerData(string cXML)
        {
            try
            {
                aResponse.lEstado = true;
                XElement oXmlDocumentWithoutNs = clsRemoveAllNamespaces(XElement.Parse(cXML));
                XDocument oXdoc = XDocument.Parse(oXmlDocumentWithoutNs.ToString());
                ////Obtencion de info///
                string cElt = oXdoc.Root.Element("UUID").Value;
                if (string.IsNullOrEmpty(aResponse.cCUFE)) {
                    aResponse.cCUFE = "No se pudo obtener CUFE de documento";
                }
                else aResponse.cCUFE = cElt.ToString();
                IEnumerable<string> textSegs =
                        from seg in oXmlDocumentWithoutNs.Descendants("CompanyID")
                        select (string)seg;
                string num = textSegs.FirstOrDefault().ToString();
                if (!string.IsNullOrEmpty(num)) aResponse.cNitEmisor = num;
                else aResponse.cNitEmisor = "No se pudo recuperar Nit emisor";
                
            }
            catch
            { //log
                if (string.IsNullOrEmpty(aResponse.cCUFE)) aResponse.cCUFE = "Error en recuperación CUFE de documento";
                if (string.IsNullOrEmpty(aResponse.cNitEmisor)) aResponse.cNitEmisor = "Error en recuperación Nit emisor";
                aResponse.cMensaje = "Error al obtener Nit - CUFE";
            }
        } // obtiene nit y cufe

        private static XElement clsRemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    xElement.Add(attribute);

                return xElement;
            }
            return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => clsRemoveAllNamespaces(el)));
        }//remueve namespaces

    }
}


