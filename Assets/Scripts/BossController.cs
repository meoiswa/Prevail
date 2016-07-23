using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Prevail.Model;

public class BossController : NetworkBehaviour {

    [SyncVar]
    public Color Color;
    [SyncVar]
    public string Name;

    public BossCharacter Boss;

    public GameObject Head;
    public GameObject LHand;
    public GameObject RHand;

    private GameObject VRHead;
    private GameObject VRLHand;
    private GameObject VRRHand;

    Renderer m_renderer;
	// Use this for initialization
	void Start () {
        m_renderer = GetComponent<Renderer>();
        
        Boss = new BossCharacter();
    }
    	
	// Update is called once per frame
	void Update () {
        //if (m_renderer.material.color != Color)
        //{
        //    m_renderer.material.color = Color;
        //}
        //if (name != Name)
        //{
        //    name = Name;
        //}

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
