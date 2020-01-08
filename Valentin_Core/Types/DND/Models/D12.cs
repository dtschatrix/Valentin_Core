using System;
using System.Collections.Generic;
using System.Text;

namespace Valentin_Core
{
    public class D12 : DND
    {
        #region Constructors

        public D12()
        {
            CubeType = 12;
            CubeResult = 0;
        }
        #endregion  

        public override void SetCubeResult()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            CubeResult = rand.Next(1, 12);
        }


    }
}
