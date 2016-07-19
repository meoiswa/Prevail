using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Prevail.Model
{
    public abstract class Character
    {

        public bool Alive
        {
            get
            {
                return Health >= 0;
            }
        }

        public virtual float MaxHealth { get; protected set; }
        public virtual float Health { get; protected set; }
        
        public Character(float maxHealth)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            Game.instance.RegisterCharacter(this);
        }
        
        public virtual void Damage(float ammount)
        {
            Health -= ammount;

            Health = Mathf.Clamp(Health, 0, MaxHealth);
        }
        
        public virtual void Heal(float ammount)
        {
            Damage(-ammount);
        }
    }
}
