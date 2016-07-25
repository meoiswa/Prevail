using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Prevail.Model.Net
{
    public class CitySyncMessage : MessageBase
    {
        public int Seed { get; private set; }

        public const short MsgId = 300;

        public CitySyncMessage()
        {

        }

        public CitySyncMessage(NetworkReader reader)
        {
            Deserialize(reader);
        }

        public CitySyncMessage(int seed)
        {
            Seed = seed;
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(Seed);
        }

        public override void Deserialize(NetworkReader reader)
        {
            Seed = reader.ReadInt32();
        }
    }

}
