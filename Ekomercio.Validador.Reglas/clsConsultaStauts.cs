using Ekomercio.Entidades.Validador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekomercio.Validador.Reglas
{
    public class clsConsultaStauts
    {
        clsResponseDTO<string> aResponseEvento = new clsResponseDTO<string>();
        List<clsInformacionEventos> aInfoEvento = new List<clsInformacionEventos>();
        clsInformacionEventos clsInfo = new clsInformacionEventos();
        Random oRnd = new Random();   // temp

        public clsResponseDTO<string> clsConsultaEstado(string cCUFE)
        {
            aResponseEvento.cCUFE = cCUFE;
            int nRand = oRnd.Next(4);
            //REALIZA LA BSUQUEDA DEL CUFE EN LA DATA
            switch (cCUFE)
            {
                case "bf8b43bc09bdf01204dd2f1dcae2b51ea02e849f29257140fc00dc9b65aa953e":
                    aResponseEvento.lEstado = true;
                    aInfoEvento = generarEvento(nRand);
                    aResponseEvento.aInformacionEventos = aInfoEvento;
                  return aResponseEvento;
                case "200":
                    clsInfo.cNombreEvento = "Error en POST parametros incorrectos";
                    clsInfo.cCodigoEvento = "Error:200";
                    break;
                case "100":
                    clsInfo.cNombreEvento = "Token incorrecto ";
                    clsInfo.cCodigoEvento = "Error:100";
                    break;
                case "404":
                    clsInfo.cNombreEvento = "Error en conexion";
                    clsInfo.cCodigoEvento = "Error:100";
                    break;
                default:
                    aResponseEvento.lEstado = false;
                    clsInfo.cNombreEvento = "CUFE no encontrado";
                    clsInfo.cCodigoEvento = "Error:1000";
                    aInfoEvento.Add(clsInfo);
                    aResponseEvento.aInformacionEventos = aInfoEvento ;
                    break;
            }

            aInfoEvento.Add(clsInfo);
            aResponseEvento.aInformacionEventos = aInfoEvento;

            return aResponseEvento;
        }

        public List<clsInformacionEventos> generarEvento(int nEvent)
        {
            DateTime RandomDay()
            {
                DateTime start = new DateTime(2018, 1, 1);
                int range = (DateTime.Today - start).Days;
                return start.AddDays(oRnd.Next(range));
            }// random day

            aResponseEvento.cNitEmisor = oRnd.Next(100000000,999999999).ToString(); //Nit creado aleatoriamente
            List<clsInformacionEventos> aInfoEvento = new List<clsInformacionEventos>();

            for ( int x = 0; x <= nEvent; x++ )
            {
                clsInformacionEventos clsInfo = new clsInformacionEventos();
                switch (nEvent){
                    case 1:
                        clsInfo.cNombreEvento = "Uso Autorizado por PA";
                        clsInfo.cCodigoEvento = "01";
                        clsInfo.cNSU = oRnd.Next(1000000, 9999999).ToString();
                        clsInfo.dFechaRecepcion = RandomDay().ToString();
                        clsInfo.dFechavalidacion = RandomDay().ToString();
                        clsInfo.cDescripcionEvento = "Documento electrónico fue validado exitosamente por un Proveedor Autorizado para la validación de facturas";
                        clsInfo.cResponseXMLB64 = "PHM6RW52ZWxvcGUgeG1sbnM6cz0iaHR0cDovL3d3dy53My5vcmcvMjAwMy8wNS9zb2FwLWVudmVsb3BlIiB4bWxuczphPSJodHRwOi8vd3d3LnczLm9yZy8yMDA1LzA4L2FkZHJlc3NpbmciIHhtbG5zOnU9Imh0dHA6Ly9kb2NzLm9hc2lzLW9wZW4ub3JnL3dzcy8yMDA0LzAxL29hc2lzLTIwMDQwMS13c3Mtd3NzZWN1cml0eS11dGlsaXR5LTEuMC54c2QiPiA8czpIZWFkZXI+IDxhOkFjdGlvbiBzOm11c3RVbmRlcnN0YW5kPSIxIj5odHRwOi8vd2NmLmRpYW4uY29sb21iaWEvSVdjZkRpYW5DdXN0b21lclNlcnZpY2VzL0dldFhtbEJ5RG9jdW1lbnRLZXlSZXNwb25zZTwvYTpBY3Rpb24+IDxvOlNlY3VyaXR5IHM6bXVzdFVuZGVyc3RhbmQ9IjEiIHhtbG5zOm89Imh0dHA6Ly9kb2NzLm9hc2lzLW9wZW4ub3JnL3dzcy8yMDA0LzAxL29hc2lzLTIwMDQwMS13c3Mtd3NzZWN1cml0eS1zZWNleHQtMS4wLnhzZCI+IDx1OlRpbWVzdGFtcCB1OklkPSJfMCI+IDx1OkNyZWF0ZWQ+MjAxOC0xMi0xNFQxNTo1MjozNy4wOTZaPC91OkNyZWF0ZWQ+IDx1OkV4cGlyZXM+MjAxOC0xMi0xNFQxNTo1NzozNy4wOTZaPC91OkV4cGlyZXM+IDwvdTpUaW1lc3RhbXA+IDwvbzpTZWN1cml0eT4gPC9zOkhlYWRlcj4gPHM6Qm9keT4gPEdldFhtbEJ5RG9jdW1lbnRLZXlSZXNwb25zZSB4bWxucz0iaHR0cDovL3djZi5kaWFuLmNvbG9tYmlhIj4gPEdldFhtbEJ5RG9jdW1lbnRLZXlSZXN1bHQgeG1sbnM6aT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UiPiA8YjpDb2RlPk9rPC9iOkNvZGU+IDxiOk1lc3NhZ2U+RWwgWE1MIHBhcmEgZWwgdHJhY2tJZDogZjNiZTFhMmY4MzJjMTA1NjRhMThlNTA0NGUxNjg5MTczOWY3NzYzMSBmdWUgZW5jb250cmFkbzwvYjpNZXNzYWdlPiA8YjpYbWxCeXRlc0Jhc2U2ND4gYXJjaGl2byBVQkwgREZFIGVuIGJhc2UgNjQgPC9HZXRYbWxCeURvY3VtZW50S2V5UmVzdWx0PiA8L0dldFhtbEJ5RG9jdW1lbnRLZXlSZXNwb25zZT4gPC9zOkJvZHk+IDwvczpFbnZlbG9wZT4NCklEDQpZ";
                        break;
                    case 2:
                        clsInfo.cNombreEvento = "Uso Autorizado por la DIAN";
                        clsInfo.cCodigoEvento = "02";
                        clsInfo.cNSU = oRnd.Next(1000000, 9999999).ToString();
                        clsInfo.dFechaRecepcion = RandomDay().ToString();
                        clsInfo.dFechavalidacion = RandomDay().ToString();
                        clsInfo.cDescripcionEvento = "Documento electrónico fue validado exitosamente por la DIAN.";
                        clsInfo.cResponseXMLB64 = "PHM6RW52ZWxvcGUgeG1sbnM6cz0iaHR0cDovL3d3dy53My5vcmcvMjAwMy8wNS9zb2FwLWVudmVsb3BlIiB4bWxuczphPSJodHRwOi8vd3d3LnczLm9yZy8yMDA1LzA4L2FkZHJlc3NpbmciIHhtbG5zOnU9Imh0dHA6Ly9kb2NzLm9hc2lzLW9wZW4ub3JnL3dzcy8yMDA0LzAxL29hc2lzLTIwMDQwMS13c3Mtd3NzZWN1cml0eS11dGlsaXR5LTEuMC54c2QiPiA8czpIZWFkZXI+IDxhOkFjdGlvbiBzOm11c3RVbmRlcnN0YW5kPSIxIj5odHRwOi8vd2NmLmRpYW4uY29sb21iaWEvSVdjZkRpYW5DdXN0b21lclNlcnZpY2VzL0dldFhtbEJ5RG9jdW1lbnRLZXlSZXNwb25zZTwvYTpBY3Rpb24+IDxvOlNlY3VyaXR5IHM6bXVzdFVuZGVyc3RhbmQ9IjEiIHhtbG5zOm89Imh0dHA6Ly9kb2NzLm9hc2lzLW9wZW4ub3JnL3dzcy8yMDA0LzAxL29hc2lzLTIwMDQwMS13c3Mtd3NzZWN1cml0eS1zZWNleHQtMS4wLnhzZCI+IDx1OlRpbWVzdGFtcCB1OklkPSJfMCI+IDx1OkNyZWF0ZWQ+MjAxOC0xMi0xNFQxNTo1MjozNy4wOTZaPC91OkNyZWF0ZWQ+IDx1OkV4cGlyZXM+MjAxOC0xMi0xNFQxNTo1NzozNy4wOTZaPC91OkV4cGlyZXM+IDwvdTpUaW1lc3RhbXA+IDwvbzpTZWN1cml0eT4gPC9zOkhlYWRlcj4gPHM6Qm9keT4gPEdldFhtbEJ5RG9jdW1lbnRLZXlSZXNwb25zZSB4bWxucz0iaHR0cDovL3djZi5kaWFuLmNvbG9tYmlhIj4gPEdldFhtbEJ5RG9jdW1lbnRLZXlSZXN1bHQgeG1sbnM6aT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UiPiA8YjpDb2RlPk9rPC9iOkNvZGU+IDxiOk1lc3NhZ2U+RWwgWE1MIHBhcmEgZWwgdHJhY2tJZDogZjNiZTFhMmY4MzJjMTA1NjRhMThlNTA0NGUxNjg5MTczOWY3NzYzMSBmdWUgZW5jb250cmFkbzwvYjpNZXNzYWdlPiA8YjpYbWxCeXRlc0Jhc2U2ND4gYXJjaGl2byBVQkwgREZFIGVuIGJhc2UgNjQgPC9HZXRYbWxCeURvY3VtZW50S2V5UmVzdWx0PiA8L0dldFhtbEJ5RG9jdW1lbnRLZXlSZXNwb25zZT4gPC9zOkJvZHk+IDwvczpFbnZlbG9wZT4NCklEDQpZ";
                        break;
                    case 3:
                        clsInfo.cNombreEvento = "Documento Electrónico Validado por PA, y que Debería Haber Sido Rechazado";
                        clsInfo.cCodigoEvento = "03";
                        clsInfo.cNSU = oRnd.Next(1000000, 9999999).ToString();
                        clsInfo.dFechaRecepcion = RandomDay().ToString();
                        clsInfo.dFechavalidacion = RandomDay().ToString();
                        clsInfo.cDescripcionEvento = "Documento electrónico validado exitosamente por un PA, transmitido por este PA para la DIAN, pero que no cumple satisfactoriamente con todas las validaciones, y que, por lo tanto, no debiera haber sido validado exitosamente por el PA.";
                        clsInfo.cResponseXMLB64 = "PHM6RW52ZWxvcGUgeG1sbnM6cz0iaHR0cDovL3d3dy53My5vcmcvMjAwMy8wNS9zb2FwLWVudmVsb3BlIiB4bWxuczphPSJodHRwOi8vd3d3LnczLm9yZy8yMDA1LzA4L2FkZHJlc3NpbmciIHhtbG5zOnU9Imh0dHA6Ly9kb2NzLm9hc2lzLW9wZW4ub3JnL3dzcy8yMDA0LzAxL29hc2lzLTIwMDQwMS13c3Mtd3NzZWN1cml0eS11dGlsaXR5LTEuMC54c2QiPiA8czpIZWFkZXI+IDxhOkFjdGlvbiBzOm11c3RVbmRlcnN0YW5kPSIxIj5odHRwOi8vd2NmLmRpYW4uY29sb21iaWEvSVdjZkRpYW5DdXN0b21lclNlcnZpY2VzL0dldFhtbEJ5RG9jdW1lbnRLZXlSZXNwb25zZTwvYTpBY3Rpb24+IDxvOlNlY3VyaXR5IHM6bXVzdFVuZGVyc3RhbmQ9IjEiIHhtbG5zOm89Imh0dHA6Ly9kb2NzLm9hc2lzLW9wZW4ub3JnL3dzcy8yMDA0LzAxL29hc2lzLTIwMDQwMS13c3Mtd3NzZWN1cml0eS1zZWNleHQtMS4wLnhzZCI+IDx1OlRpbWVzdGFtcCB1OklkPSJfMCI+IDx1OkNyZWF0ZWQ+MjAxOC0xMi0xNFQxNTo1MjozNy4wOTZaPC91OkNyZWF0ZWQ+IDx1OkV4cGlyZXM+MjAxOC0xMi0xNFQxNTo1NzozNy4wOTZaPC91OkV4cGlyZXM+IDwvdTpUaW1lc3RhbXA+IDwvbzpTZWN1cml0eT4gPC9zOkhlYWRlcj4gPHM6Qm9keT4gPEdldFhtbEJ5RG9jdW1lbnRLZXlSZXNwb25zZSB4bWxucz0iaHR0cDovL3djZi5kaWFuLmNvbG9tYmlhIj4gPEdldFhtbEJ5RG9jdW1lbnRLZXlSZXN1bHQgeG1sbnM6aT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UiPiA8YjpDb2RlPk9rPC9iOkNvZGU+IDxiOk1lc3NhZ2U+RWwgWE1MIHBhcmEgZWwgdHJhY2tJZDogZjNiZTFhMmY4MzJjMTA1NjRhMThlNTA0NGUxNjg5MTczOWY3NzYzMSBmdWUgZW5jb250cmFkbzwvYjpNZXNzYWdlPiA8YjpYbWxCeXRlc0Jhc2U2ND4gYXJjaGl2byBVQkwgREZFIGVuIGJhc2UgNjQgPC9HZXRYbWxCeURvY3VtZW50S2V5UmVzdWx0PiA8L0dldFhtbEJ5RG9jdW1lbnRLZXlSZXNwb25zZT4gPC9zOkJvZHk+IDwvczpFbnZlbG9wZT4NCklEDQpZ";
                     break;
                    default:
                        clsInfo.cNombreEvento = "Documento Electrónico Referenciado por Otro Documento Electrónico";
                        clsInfo.cCodigoEvento = "10";
                        clsInfo.cNSU = oRnd.Next(1000000,9999999).ToString();
                        clsInfo.dFechaRecepcion = RandomDay().ToString();
                        clsInfo.dFechavalidacion = RandomDay().ToString();
                        clsInfo.cDescripcionEvento = "DFE fue referenciado en el elemento root/cac:AdditionalDocumentReference del DFE apuntado por el evento.";
                        clsInfo.cResponseXMLB64 = "PHM6RW52ZWxvcGUgeG1sbnM6cz0iaHR0cDovL3d3dy53My5vcmcvMjAwMy8wNS9zb2FwLWVudmVsb3BlIiB4bWxuczphPSJodHRwOi8vd3d3LnczLm9yZy8yMDA1LzA4L2FkZHJlc3NpbmciIHhtbG5zOnU9Imh0dHA6Ly9kb2NzLm9hc2lzLW9wZW4ub3JnL3dzcy8yMDA0LzAxL29hc2lzLTIwMDQwMS13c3Mtd3NzZWN1cml0eS11dGlsaXR5LTEuMC54c2QiPiA8czpIZWFkZXI+IDxhOkFjdGlvbiBzOm11c3RVbmRlcnN0YW5kPSIxIj5odHRwOi8vd2NmLmRpYW4uY29sb21iaWEvSVdjZkRpYW5DdXN0b21lclNlcnZpY2VzL0dldFhtbEJ5RG9jdW1lbnRLZXlSZXNwb25zZTwvYTpBY3Rpb24+IDxvOlNlY3VyaXR5IHM6bXVzdFVuZGVyc3RhbmQ9IjEiIHhtbG5zOm89Imh0dHA6Ly9kb2NzLm9hc2lzLW9wZW4ub3JnL3dzcy8yMDA0LzAxL29hc2lzLTIwMDQwMS13c3Mtd3NzZWN1cml0eS1zZWNleHQtMS4wLnhzZCI+IDx1OlRpbWVzdGFtcCB1OklkPSJfMCI+IDx1OkNyZWF0ZWQ+MjAxOC0xMi0xNFQxNTo1MjozNy4wOTZaPC91OkNyZWF0ZWQ+IDx1OkV4cGlyZXM+MjAxOC0xMi0xNFQxNTo1NzozNy4wOTZaPC91OkV4cGlyZXM+IDwvdTpUaW1lc3RhbXA+IDwvbzpTZWN1cml0eT4gPC9zOkhlYWRlcj4gPHM6Qm9keT4gPEdldFhtbEJ5RG9jdW1lbnRLZXlSZXNwb25zZSB4bWxucz0iaHR0cDovL3djZi5kaWFuLmNvbG9tYmlhIj4gPEdldFhtbEJ5RG9jdW1lbnRLZXlSZXN1bHQgeG1sbnM6aT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UiPiA8YjpDb2RlPk9rPC9iOkNvZGU+IDxiOk1lc3NhZ2U+RWwgWE1MIHBhcmEgZWwgdHJhY2tJZDogZjNiZTFhMmY4MzJjMTA1NjRhMThlNTA0NGUxNjg5MTczOWY3NzYzMSBmdWUgZW5jb250cmFkbzwvYjpNZXNzYWdlPiA8YjpYbWxCeXRlc0Jhc2U2ND4gYXJjaGl2byBVQkwgREZFIGVuIGJhc2UgNjQgPC9HZXRYbWxCeURvY3VtZW50S2V5UmVzdWx0PiA8L0dldFhtbEJ5RG9jdW1lbnRLZXlSZXNwb25zZT4gPC9zOkJvZHk+IDwvczpFbnZlbG9wZT4NCklEDQpZ";
                     break;
                }

                aInfoEvento.Add(clsInfo);
            }

            return aInfoEvento;
            
        }
 
    }
}
