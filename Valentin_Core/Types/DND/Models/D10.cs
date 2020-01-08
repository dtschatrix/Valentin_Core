using System;
using System.Collections.Generic;
using System.Text;

namespace Valentin_Core
{
    public class D10 : DND
    {
        #region Constructors

        public D10()
        {
            CubeType = 10;
            CubeResult = 0;
        }

        #endregion

        public override void SetCubeResult()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            CubeResult = rand.Next(1, 10);
        }

    }
}
