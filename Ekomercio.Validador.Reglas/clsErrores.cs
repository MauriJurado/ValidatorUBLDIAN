using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekomercio.Validador.Reglas
{
    public class clsErrores
    {
        private static string _Error;
        public static string Error(int ex)
        {
            switch (ex)
            {
                case 100:
                    _Error = "Token invalido, inserte un token valido";
                    break;
                case 105:
                    _Error = "Cadena vacia";
                    break;
                case 101:
                    _Error = "No se especifico el codigo";
                    break;
                case 200:
                    _Error = "Error en peticion, parametros incorrectos en el POST";
                    break;
                case 404:
                    _Error = "Error en conexion";
                    break;
                case 150:
                    _Error = "Codigo no econtrado";
                    break;
                default:
                    _Error = "No Especificado";
                    break;
            }
            return _Error;
        }
    }
}
