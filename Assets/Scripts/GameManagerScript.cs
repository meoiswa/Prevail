using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Prevail.Model;
using UnityEngine.UI;

public class GameManagerScript : NetworkBehaviour
{

    Game game;
    Text timer;
    Slider healthbar;

    [SyncVar]
    public float TimeSecondsLeft = 600;

    // Use this for initialization
    void Start()
    {
        game = Game.instance;

        //if (!isServer)
        //{
            GameObject timerbox = GameObject.Find("Timer");
            timerbox.SetActive(false);
            timer = timerbox.GetComponent<Text>();

            GameObject slider = GameObject.Find("Slider");
            slider.SetActive(false);
            healthbar = slider.GetComponent<Slider>();
            healthbar.value = 0f;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //if (isServer)
        //{
            if (game.state == Game.GameState.inProgress)
            {
                TimeSecondsLeft -= Time.deltaTime;
                timer.text = TimeSecondsLeft.ToString();

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


        if (!isServer)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                healthbar.value += 0.1f;
            }
        }
        //}
    }


}
