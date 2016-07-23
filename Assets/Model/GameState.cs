using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Prevail.Model
{
    public struct ObjectState
    {
        public uint nId;
        public float x, y, z;
        public float vx, vy, vz;

        public ObjectState(Rigidbody r, uint nid)
        {
            nId = nid;
            x = r.position.x;
            y = r.position.y;
            z = r.position.z;
            vx = r.velocity.x;
            vy = r.velocity.y;
            vz = r.velocity.z;
        }
    }

    public class GameState
    {
        public ObjectState[] objectStates;
        public float Time { get; private set; }

        public float PhysicsTime { get; private set; }

        static uint idCounter = 1;
        public uint Id { get; private set; }

        public GameState(IEnumerable<ObjectState> states)
        {
            Id = idCounter++;
            objectStates = states.ToArray();
            Time = NetworkTransport.GetNetworkTimestamp() / 1000f;
            PhysicsTime = UnityEngine.Time.fixedTime;
        }

        public void ClientApply(float time)
        {
            foreach(var s in objectStates)
            {
                var gobj = ClientScene.FindLocalObject(new NetworkInstanceId(s.nId));
                var rbody = gobj.GetComponent<Rigidbody>();

                rbody.MovePosition(Vector3.Lerp(rbody.position, new Vector3(s.x, s.y, s.z), time - PhysicsTime));
                
                
                //rbody.MovePosition(new Vector3(s.x, s.y, s.z));
                rbody.velocity = new Vector3(s.vx, s.vy, s.vz);
            }
        }

        static GameState LastState;
        public static List<GameObject> TrackedObjects = new List<GameObject>();
        public static GameState GetState()
        {
            if (LastState != null && LastState.PhysicsTime == UnityEngine.Time.fixedTime)
            {
                return LastState; //Cache state if physics haven't updated
            }
            else
            {
                var states = new List<ObjectState>();
                foreach (var o in TrackedObjects)
                {
                    var r = o.GetComponent<Rigidbody>();
                    var n = o.GetComponent<NetworkIdentity>();

                    var os = new ObjectState(r, n.netId.Value);

                    states.Add(new ObjectState(r, n.netId.Value));
                }
                return new GameState(states.ToArray());
            }
        }
    }
}
