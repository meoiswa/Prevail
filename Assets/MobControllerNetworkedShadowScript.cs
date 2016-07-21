using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using NewtonVR;
using System;

[RequireComponent(typeof(NVRInteractableItem))]
public class MobControllerNetworkedShadowScript : NetworkBehaviour
{
    
    public MobController Target;

    public NVRInteractableItem item;
    

    [SyncVar]
    public bool isAttached = false;

    Rigidbody rbody;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        item = GetComponent<NVRInteractableItem>();
    }
    
    public void Attach(MobController target)
    {
        Target = target;
        target.shadowRBody = rbody;
        RpcAttach(target.GetComponent<NetworkIdentity>().netId);
    }
    
    [ClientRpc]
    public void RpcAttach(NetworkInstanceId id)
    {
        Target = ClientScene.FindLocalObject(id).GetComponent<MobController>();
        Target.shadowRBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            if (isServer)
            {
                isAttached = item.IsAttached;

                if (!isAttached)
                {
                    rbody.MovePosition(Target.transform.position);
                    rbody.MoveRotation(Target.transform.rotation);
                    rbody.velocity = Target.GetComponent<Rigidbody>().velocity;
                }
            }
            Target.isAttached = isAttached;
        }
    }
}
