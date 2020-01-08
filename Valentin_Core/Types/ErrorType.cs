using System;
using System.Collections.Generic;
using System.Text;

namespace Valentin_Core
{
    //TODO errortypes
    [Serializable]
    abstract class ErrorType : Exception
    {
        #region Public Properties

        public string ErrorMessage { get; set; }

        public string PathToImage { get; set; }

        #endregion

        #region Constructor




        #endregion

        #region Exception



        #endregion

    }
}
