using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Prevail.Model
{
    public class MobCharacter : Character
    {

        private float maxHealth;
        public override float MaxHealth
        {
            get
            {
                if (currentQuirk is HealthUpQuirk)
                {
                    return maxHealth * currentQuirk.Value;
                }
                return maxHealth;
            }
            protected set
            {
                maxHealth = value;
            }

        }

        private float health;
        public override float Health
        {
            get
            {
                if (currentQuirk is HealthUpQuirk)
                {
                    return health * currentQuirk.Value;
                }
                return health;
            }
            protected set
            {
                health = value;
            }

        }

        private float forwardSpeed;
        public float ForwardSpeed
        {
            get
            {
                if (currentQuirk is SpeedUpQuirk)
                {
                    return forwardSpeed * currentQuirk.Value;
                }
                return forwardSpeed;
            }
            private set
            {
                forwardSpeed = value;
            }

        }

        private float backwardSpeed;
        public float BackwardSpeed
        {
            get
            {
                if (currentQuirk is SpeedUpQuirk)
                {
                    return backwardSpeed * currentQuirk.Value;
                }
                return backwardSpeed;
            }
            private set
            {
                backwardSpeed = value;
            }
        }

        private float strafeSpeed;
        public float StrafeSpeed
        {
            get
            {
                if (currentQuirk is SpeedUpQuirk)
                {
                    return strafeSpeed * currentQuirk.Value;
                }
                return strafeSpeed;
            }
            private set
            {
                strafeSpeed = value;
            }
        }

        public float RunMultiplier { get; private set; }

        private float jumpForce;
        public float JumpForce
        {
            get
            {
                if (currentQuirk is JumpUpQuirk)
                {
                    return jumpForce * currentQuirk.Value;
                }
                return jumpForce;
            }
            private set
            {
                jumpForce = value;
            }
        }

        public KeyCode RunKey { get; private set; }

        private Quirk currentQuirk;

        public MobCharacter() : base(100)
        {
            ForwardSpeed = 12.0f;   // Speed when walking forward
            BackwardSpeed = 6.0f;  // Speed when walking backwards
            StrafeSpeed = 6.0f;    // Speed when walking sideways
            RunMultiplier = 2.0f;
            RunKey = KeyCode.LeftShift;
            JumpForce = 30f;
        }

        public void Pick(Quirk q)
        {
            currentQuirk = q;
        }

    }
}
