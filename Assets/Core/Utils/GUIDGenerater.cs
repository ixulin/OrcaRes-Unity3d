using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orca
{
    public class GUIDGenerater
    {
        private static long msActorGUID = 0;
        public static long GenerateUnitGUID()
        {
            return ++msActorGUID;
        }
    }
}
