using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prevail.Model
{
    public class HealthUpQuirk : Quirk
    {
        
        public HealthUpQuirk(float value = 1.5f) :base(value)
        {
            Value = value;
        }

    }
}
