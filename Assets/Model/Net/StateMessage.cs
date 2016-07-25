using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;
using System.Text;

namespace Prevail.Model.Net
{
    public class StateAckMessage : MessageBase
    {
        public const short MsgId = 302;

        public uint Last { get; private set; }
        public int ConnectionId { get; private set; }

        public StateAckMessage()
        {

        }

        public StateAckMessage(uint last, int connId)
        {
            Last = last;
            ConnectionId = connId;
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(Last);
            writer.Write(ConnectionId);
        }

        public override void Deserialize(NetworkReader reader)
        {
            Last = reader.ReadUInt32();
            ConnectionId = reader.ReadInt32();
        }
    }

    public class StateRequestMessage : MessageBase
    {
        public const short MsgId = 300;

        public uint Last { get; private set; }
        public int ConnectionId { get; private set; }

        public StateRequestMessage()
        {

        }

        public StateRequestMessage(uint last, int connId)
        {
            Last = last;
            ConnectionId = connId;
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(Last);
            writer.Write(ConnectionId);
        }

        public override void Deserialize(NetworkReader reader)
        {
            Last = reader.ReadUInt32();
            ConnectionId = reader.ReadInt32();
        }
    }

    public class StateSendMessage : MessageBase
    {
        public GameState state;

        public const short MsgId = 301;

        public StateSendMessage()
        {

        }
        public StateSendMessage(GameState s)
        {
            state = s;
        }
        public StateSendMessage(NetworkReader r)
        {
            Deserialize(r);
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(state.Id);
            writer.Write((double) state.PhysicsTime);
            writer.Write((double) state.Time);

            var count = state.objectStates.Count();
            writer.Write(count);
            
            for(short i=0;i< count;i++)
            {
                var os = state.objectStates[i];
                writer.Write(os.nId);
                writer.Write((double)os.x);
                writer.Write((double)os.y);
                writer.Write((double)os.z);
                writer.Write((double)os.vx);
                writer.Write((double)os.vy);
                writer.Write((double)os.vz);
            }
        }

        public override void Deserialize(NetworkReader reader)
        {
            var nid = reader.ReadUInt32();
            var ptime = (float)reader.ReadDouble();
            var time = (float)reader.ReadDouble();

            var count = reader.ReadInt32();

            var list = new List<ObjectState>();

            for (short i=0;i<count;i++)
            {
                list.Add(new ObjectState()
                {
                    nId = reader.ReadUInt32(),
                    x = (float)reader.ReadDouble(),
                    y = (float)reader.ReadDouble(),
                    z = (float)reader.ReadDouble(),
                    vx = (float)reader.ReadDouble(),
                    vy = (float)reader.ReadDouble(),
                    vz = (float)reader.ReadDouble()
                });
            }

            state = new GameState(list);
        }

    }
}
