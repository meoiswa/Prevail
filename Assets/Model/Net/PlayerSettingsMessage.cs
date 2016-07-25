using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Prevail.Model.Net
{
    public class PlayerSettingsMessage : MessageBase
    {
        public string Name { get; private set; }

        public const short MsgId = 301;

        public Color Color { get; private set; }

        public bool VR { get; private set; }

        public string Guid { get; private set; }

        public PlayerSettingsMessage(NetworkReader reader)
        {
            Deserialize(reader);
        }

        public PlayerSettingsMessage(string name, Color color, bool vr = false)
        {
            Guid = SystemInfo.deviceUniqueIdentifier;
            Name = name;
            Color = color;
            VR = vr;
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(Name);
            writer.Write(Color);
            writer.Write(VR);
            writer.Write(Guid);
        }

        public override void Deserialize(NetworkReader reader)
        {
            Name = reader.ReadString();
            Color = reader.ReadColor();
            VR = reader.ReadBoolean();
            Guid = reader.ReadString();
        }
    }

}
