using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Prevail.Model;

[RequireComponent(typeof(Collider))]
public class QuirkScript : NetworkBehaviour {

    public Quirk quirk;

	// Use this for initialization
	void Start () {
        if (gameObject.tag == "SpeedQuirk")
            quirk = new SpeedUpQuirk();
        else if (gameObject.tag == "HealthQuirk")
            quirk = new HealthUpQuirk();
        else if (gameObject.tag == "JumpQuirk")
            quirk = new HealthUpQuirk();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [Command]
    public void CmdDestroy()
    {
        NetworkServer.Destroy(this.gameObject);
    }

    public void Destroy()
    {
        if (isServer)
        {
            NetworkServer.Destroy(this.gameObject);
        }
        else
        {
            CmdDestroy();
        }
    }
}
