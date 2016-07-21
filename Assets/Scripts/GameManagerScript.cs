using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Prevail.Model;

public class GameManagerScript : NetworkBehaviour
{

    Game game;

    [SyncVar]
    public float TimeSecondsLeft = 600;

    // Use this for initialization
    void Start()
    {
        game = Game.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            if (game.state == Game.GameState.inProgress)
            {
                TimeSecondsLeft -= Time.deltaTime;

                if (TimeSecondsLeft <= 0 || (game.Boss != null && (!game.Boss.Alive || game.BossWins)) )
                {
                    game.End();
                }
            }
            else if (game.state == Game.GameState.bossWins)
            {
                
            }
            else
            {

            }
        }
    }


}
