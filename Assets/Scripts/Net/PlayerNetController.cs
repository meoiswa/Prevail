using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Prevail.Model;

public class PlayerNetController : NetworkBehaviour
{
    PlayerNetCharacter character;
    [SyncVar]
    public uint character_nId;

    public PlayerNetCharacter Character
    {
        get
        {
            if (isClient && character == null)
            {
                var obj = ClientScene.FindLocalObject(new NetworkInstanceId(character_nId));
                return obj == null ? null : obj.GetComponent<PlayerNetCharacter>();
            }
            return character;
        }
        set
        {
            character = value;
            character_nId = character.GetComponent<NetworkIdentity>().netId.Value;
        }
    }

    [SyncVar]
    public bool GameStarted = false;

    [SyncVar]
    public Color Color;
    [SyncVar]
    public string Name;

    public float Vertical;

    public float Horizontal;

    public bool Jump;

    public bool Fire;

    public bool Reset;

    bool isCameraSet = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer && Character != null && !isCameraSet)
        {
            Camera.main.transform.parent = Character.gameObject.transform;
            Camera.main.transform.localPosition = new Vector3(0, 8, -8);
            Camera.main.transform.rotation = Quaternion.Euler(new Vector3(45, 0, 0));

            isCameraSet = true;
        }
        if (isLocalPlayer && GameStarted)
        {
            Vertical = Input.GetAxis("Vertical");
            Horizontal = Input.GetAxis("Horizontal");
            Jump = Input.GetButton("Jump");
            Fire = Input.GetButton("Fire1");
            Reset = Input.GetKey(KeyCode.R);

            CmdInput(Vertical, Horizontal, Jump, Fire, Reset);
        }
    }

    [Command]
    void CmdInput(float vertical, float horizontal, bool jump, bool fire, bool reset)
    {
        Vertical = vertical;
        Horizontal = horizontal;
        Jump = jump;
        Fire = fire;
        Reset = reset;
    }


    void FixedUpdate()
    {
        if (Character != null && GameStarted)
        {
            Character.FixedUpdateInput(Vertical, Horizontal, Jump, Fire, Reset);
        } 
    }
}

