using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prevail.Model
{
    public class SpeedUpQuirk : Quirk
    {
        
        public SpeedUpQuirk(float value = 2f) : base(value)
        {
            Value = value;
        }
    }
}
