using System;
using System.Collections.Generic;
using System.Text;

namespace Valentin_Core
{
    public class D8 : DND
    {
        #region Constructors

        public D8()
        {
            CubeType = 8;
            CubeResult = 0;
        }
        #endregion  

        public override void SetCubeResult()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            CubeResult = rand.Next(1, 8);
        }


      
    }
}
