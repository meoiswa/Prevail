using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class VerySimpleMoveScript : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	
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
