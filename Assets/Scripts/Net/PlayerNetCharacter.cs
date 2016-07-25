using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Prevail.Model;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class PlayerNetCharacter : NetworkBehaviour
{
    public static float maxPosError = 0.1f;

    PlayerNetController controller;
    [SyncVar]
    uint controller_nId;
    public PlayerNetController Controller
    {
        get
        {
            if (isClient && controller == null)
            {
                var obj = ClientScene.FindLocalObject(new NetworkInstanceId(controller_nId));
                return obj == null ? null : obj.GetComponent<PlayerNetController>();
            }
            return controller;
        }
        set
        {
            controller = value;
            controller_nId = controller.GetComponent<NetworkIdentity>().netId.Value;
        }
    }

    private Rigidbody m_rbody;
    private Renderer m_renderer;

    // Use this for initialization
    void Start()
    {
        m_rbody = GetComponent<Rigidbody>();
        m_renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller != null)
        {
            if (m_renderer.material.color != Controller.Color)
            {
                m_renderer.material.color = Controller.Color;
            }
            if (name != Controller.Name)
            {
                name = Controller.Name;
            }
        }
    }
    
    public void FixedUpdateInput(float vertical, float horizontal, bool jump, bool fire, bool reset)
    {
        if (reset)
        {
            m_rbody.MovePosition(Vector3.zero + Vector3.up * 2f);
        }
        m_rbody.AddForce(new Vector3(horizontal, jump ? 1f : 0f, vertical) * 10f);
    }
}

