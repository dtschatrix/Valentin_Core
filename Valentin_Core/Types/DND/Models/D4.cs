using System;


namespace Valentin_Core
{
    public class D4 : DND
    {
        #region Constructors

        public D4()
        {
            CubeType = 4;
            CubeResult = 0;
        }
        #endregion  

        public override void SetCubeResult()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            CubeResult = rand.Next(1, 4);
        }

    }
}
