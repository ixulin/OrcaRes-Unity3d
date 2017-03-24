using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orca
{
    public enum EAniState
    {
        Idle = 0,
        Move = 1,
        Jump = 2,
        Attack = 3,
        Damage = 4,
        Death = 5,
        Enrage = 6,
        Laugh = 7,
    }

    public enum EAttackType
    {
        Attack0 = 0,
        Attack1 = 1,
        Attack2 = 2,
        Attack3 = 3,
    }

    public enum ELayerDef
    {
        Bullet = 8,
        WalkSurface = 9,
    }

}
