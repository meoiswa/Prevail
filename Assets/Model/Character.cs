using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Prevail.Model
{
    public abstract class Character
    {

        public bool Alive { get; private set; }

        public virtual float MaxHealth { get; protected set; }
        public virtual float Health { get; protected set; }

        public virtual void Damage(float ammount)
        {
            Health -= ammount;

            Health = Mathf.Clamp(Health, 0, MaxHealth);

            if (Health <= 0)
            {
                Alive = false;
            }
        }
        
        public virtual void Heal(float ammount)
        {
            Damage(-ammount);
        }
    }
}
