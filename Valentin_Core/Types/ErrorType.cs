using System;
using System.Collections.Generic;
using System.Text;

namespace Valentin_Core
{
    //TODO errortypes
    [Serializable]
    public class ErrorType : Exception
    {
        #region Public Properties

        public string ErrorMessage { get; set; }


        #endregion

 

    }
}
