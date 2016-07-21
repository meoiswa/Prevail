using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Prevail.Model;

public class VerySimpleMoveScript : NetworkBehaviour {

    public BossCharacter Boss;

    public GameObject Head;
    public GameObject LHand;
    public GameObject RHand;

    private GameObject VRHead;
    private GameObject VRLHand;
    private GameObject VRRHand;

	// Use this for initialization
	void Start () {
        Boss = new BossCharacter();
	}
	
	// Update is called once per frame
	void Update () {
	    if (isLocalPlayer)
        {
            if (VRHead == null)
            {
                VRHead = GameObject.Find("Camera (head)");
            }
            else
            {
                Head.transform.position = VRHead.transform.position;
                Head.transform.rotation = VRHead.transform.rotation;
            }
            if (VRLHand == null)
            {
                VRLHand = GameObject.Find("Controller (left)");
            }
            else
            {
                LHand.transform.position = VRLHand.transform.position;
                LHand.transform.rotation = VRLHand.transform.rotation;
            }
            if (VRRHand == null)
            {
                VRRHand = GameObject.Find("Controller (right)");
            }
            else
            {
                RHand.transform.position = VRRHand.transform.position;
                RHand.transform.rotation = VRRHand.transform.rotation;
            }
        }
	}
}
