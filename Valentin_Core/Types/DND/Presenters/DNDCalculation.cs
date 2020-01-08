using System;
using System.Collections.Generic;
using System.Text;

namespace Valentin_Core
{
    public static class DndCaluclation
    {
        public static DND GetCube(string botCommands)
        {
            switch (botCommands)
            {
                case "/d4":
                    D4 d4 = new D4();
                    return d4;

                case "/d8":
                    D8 d8 = new D8();
                    return d8;

                case "/d10":
                    D10 d10 = new D10();
                    return d10;

                case "/d12":
                    D12 d12 = new D12();
                    return d12;

                case "/d20":
                    D20 d20 = new D20();
                    return d20;

                case "/d100":
                    D100 d100 = new D100();
                    return d100;

                default:
                    throw new Exception();
            }
        }

        public static List<int> GetValues(DND dnd, int count)
        {
            if (count >= 1)
            {
                var valList = new List<int>(count - 1);
                for (int i = 0; i < count; i++)
                {
                    dnd.SetCubeResult();
                    valList.Add(dnd.CubeResult);
                }

                return valList;
            }

            throw new Exception();
        }
    }
}
