using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Prevail.Model;

public class VerySimpleMoveScript : NetworkBehaviour {

    public BossCharacter Boss;

	// Use this for initialization
	void Start () {
        Boss = new BossCharacter();
	}
	
	// Update is called once per frame
	void Update () {
	    if (isLocalPlayer)
        {
            var pos = transform.position;

            pos.x += Input.GetAxis("Horizontal");
            pos.z += Input.GetAxis("Vertical");

            transform.position = pos;
        }
	}
}
