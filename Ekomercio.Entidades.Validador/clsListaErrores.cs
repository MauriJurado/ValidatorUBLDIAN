using System;
using System.Collections.Generic;
namespace Ekomercio.Entidades.Validador
{
    public class clsListaErrores
    {
        clsInformacionError cList = new clsInformacionError();

        public clsInformacionError clsListaErroes(string nError,string cInfo)
        {
            cList.nCodigoError = nError;
            cList.cDescripcionError = cInfo;
            
            return cList;
        }
    }
}
