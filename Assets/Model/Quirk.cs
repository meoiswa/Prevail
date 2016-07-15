using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prevail.Model
{
    public abstract class Quirk
    {

        public float Value { get; protected set; }
        
        public Quirk(float value)
        {
            Value = value;
        }
    }
}
