using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prevail.Model
{
    public class BossCharacter : Character
    {

        public float TeleportCooldown { get; private set; }


        public BossCharacter() : base(1000)
        {
        }
    }
}
