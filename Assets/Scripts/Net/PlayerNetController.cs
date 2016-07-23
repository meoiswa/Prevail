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
    public Color Color;
    [SyncVar]
    public string Name;

    public float Vertical;

    public float Horizontal;

    public bool Jump;

    public bool Fire;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            Vertical = Input.GetAxis("Vertical");
            Horizontal = Input.GetAxis("Horizontal");
            Jump = Input.GetButton("Jump");
            Fire = Input.GetButton("Fire1");

            CmdInput(Vertical, Horizontal, Jump, Fire);
        }
    }

    [Command]
    void CmdInput(float vertical, float horizontal, bool jump, bool fire)
    {
        Vertical = vertical;
        Horizontal = horizontal;
        Jump = jump;
        Fire = fire;
    }


    void FixedUpdate()
    {
        if (Character != null)
        {
            Character.FixedUpdateInput(Vertical, Horizontal, Jump, Fire);
        }
    }
}

