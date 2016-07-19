using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prevail.Model
{
    public class Game
    {
        public BossCharacter Boss { get; private set; }
        public List<MobCharacter> Mob { get; private set; }

        public GameState state { get; private set; }

        public static Game instance;

        public Game()
        {
            instance = this;
            state = GameState.inProgress;
            Mob = new List<MobCharacter>();
        }

        public void RegisterCharacter(Character c)
        {
            if (c is BossCharacter)
            {
                Boss = c as BossCharacter;
            }
            else
            {
                Mob.Add(c as MobCharacter);
            }
        }

        public bool BossWins
        {
            get
            {
                
                return 
                    Mob.Count != 0 //Tiny tweak to allow game to run with 0 Mob Players
                    && Mob.All(m => m.Alive == false);
            }
        }

        public GameState End()
        {
            if (BossWins)
            {
                state = GameState.bossWins;
            }
            else
            {
                state = GameState.mobWins;
            }
            
            return state;
        }

        public enum GameState
        {
            inProgress,
            bossWins,
            mobWins
        }
    }
}
