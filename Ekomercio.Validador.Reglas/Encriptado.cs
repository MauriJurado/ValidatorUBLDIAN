using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Ekomercio.Validador.Reglas
{
    public class Encriptado
    {
        protected const int zBlockSize = 4096;
        protected string cLlavePrivada = "YmsetgDkuZ/pMnhW9MdTgKhnkwT/j3FI0e/hh8cF9Po=";
        protected string cVectorEntrada = "bbUSn8h471ETkOvMLzkIJw==";

        public string EncriptarRms(string cHileraEntrada)
        {
            try
            {
                var key = Convert.FromBase64String(cLlavePrivada);
                var IV = Convert.FromBase64String(cVectorEntrada);

                using (var oMemStream = new MemoryStream())
                using (var rmCrypto = new RijndaelManaged())
                using (var cryptStream = new CryptoStream(oMemStream, rmCrypto.CreateEncryptor(key, IV), CryptoStreamMode.Write))
                using (var sWriter = new StreamWriter(cryptStream, Encoding.UTF8))
                {
                    sWriter.Write(cHileraEntrada);
                    sWriter.Close();
                    var aHileraEncriptada = oMemStream.ToArray();


                    return Convert.ToBase64String(aHileraEncriptada);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string DesEncriptarRms(string cHileraEntrada)
        {
            if (string.IsNullOrEmpty(cHileraEntrada))
            {
                return string.Empty;
            }
            try
            {
                var key = Convert.FromBase64String(cLlavePrivada);
                var IV = Convert.FromBase64String(cVectorEntrada);

                var aHileraEntrada = Convert.FromBase64String(cHileraEntrada);

                using (var oMemStream = new MemoryStream(aHileraEntrada))
                using (var rmCrypto = new RijndaelManaged())
                using (var cryptStream = new CryptoStream(oMemStream, rmCrypto.CreateDecryptor(key, IV), CryptoStreamMode.Read))
                using (var sWriter = new StreamReader(cryptStream, Encoding.UTF8))
                {


                    var cHileraEncriptada = sWriter.ReadToEnd();

                    return cHileraEncriptada.Replace("//D//", string.Empty).Replace("//#//", string.Empty).Replace("//NULL//", string.Empty);
                }
            }
            catch (Exception)
            {
                return cHileraEntrada;
            }
        }
    }
}
