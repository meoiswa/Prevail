using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using Prevail.Model;
using System;

class PlayerSettingsMessage : MessageBase
{
    public string Name { get; private set; }

    public Color Color { get; private set; }

    public bool VR { get; private set; }

    public PlayerSettingsMessage(NetworkReader reader)
    {
        Deserialize(reader);
    }

    public PlayerSettingsMessage(string name, Color color, bool vr = false)
    {
        Name = name;
        Color = color;
        VR = vr;
    }

    public override void Serialize(NetworkWriter writer)
    {
        writer.Write(Name);
        writer.Write(Color);
        writer.Write(VR);
    }

    public override void Deserialize(NetworkReader reader)
    {
        Name = reader.ReadString();
        Color = reader.ReadColor();
        VR = reader.ReadBoolean();
    }
}

public class AuthoritativeNetworkManager : NetworkManager
{

    public string PlayerName;
    public Color PlayerColor;
    public bool PlayerVR;

    public GameObject playerNetControllerPrefab;
    public GameObject playerNetCharacterPrefab;
    public GameObject bossCharacterPrefab;

    public Dictionary<int, uint> LastAck;
    
    public static AuthoritativeNetworkManager Instance;

    bool isHost = false;

    public void Start()
    {
        Instance = this;
        PlayerVR = SteamVREnabler.Instance.IsEnabled;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        //NetworkServer.RegisterHandler(StateRequestMessage.MsgId, OnStateRequest);
        NetworkServer.RegisterHandler(StateAckMessage.MsgId, OnStateAck);
        LastAck = new Dictionary<int, uint>();
        isHost = true;
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        isHost = false;
        LastAck.Clear();
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
        if (!isHost)
        {
            client.RegisterHandler(StateSendMessage.MsgId, OnStateReceived);
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        if (PlayerColor.r == 0 && PlayerColor.g == 0 && PlayerColor.b == 0)
        {
            PlayerColor = new Color(UnityEngine.Random.Range(0.5f, 1f), UnityEngine.Random.Range(0.5f, 1f), UnityEngine.Random.Range(0.5f, 1f));
        }
        ClientScene.AddPlayer(conn, 0, new PlayerSettingsMessage(PlayerName, PlayerColor, PlayerVR));
    }
    
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        LastAck.Remove(conn.connectionId);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader reader)
    {
        var message = new PlayerSettingsMessage(reader);

        if (!message.VR)
        { 
            var playerNetControllerObject = (GameObject)Instantiate(playerNetControllerPrefab);
            var playerNetController = playerNetControllerObject.GetComponent<PlayerNetController>();

            var playerNetCharacterObject = (GameObject)Instantiate(playerNetCharacterPrefab, Vector3.up * 10f, Quaternion.identity);
            var playerNetCharacter = playerNetCharacterObject.GetComponent<PlayerNetCharacter>();

            NetworkServer.Spawn(playerNetCharacterObject);
            NetworkServer.AddPlayerForConnection(conn, playerNetControllerObject, playerControllerId);

            playerNetController.Character = playerNetCharacter;
            playerNetController.Name = message.Name;
            playerNetController.Color = message.Color;

            playerNetCharacter.Controller = playerNetController;

            GameState.TrackedObjects.Add(playerNetCharacterObject);

            LastAck[conn.connectionId] = 0;
        }
        else
        {
            //boss spawn code
            var bossCharacter = (GameObject) Instantiate(bossCharacterPrefab);
            

            NetworkServer.Spawn(bossCharacter);
            NetworkServer.AddPlayerForConnection(conn, bossCharacter,playerControllerId);

            var bossScript = bossCharacter.GetComponent<BossController>();
            
        }
    }

    public void FixedUpdate()
    {
        if (isHost)
        {
            var state = GameState.GetState();
            var msg = new StateSendMessage(state);

            NetworkServer.SendUnreliableToAll(StateSendMessage.MsgId, msg);

            var writer = new NetworkWriter();
            msg.Serialize(writer);
            
        }
        else
        {
            if (lastState != null)
            {
                lastState.ClientApply(Time.fixedTime);
            }
        }
    }
    

    GameState lastState;
    bool isRequested;
    bool pendingApply;

    public void OnStateReceived(NetworkMessage msg)
    {
        var state = msg.ReadMessage<StateSendMessage>().state;

        lastState = state;
        pendingApply = true;

        client.Send(StateAckMessage.MsgId,
            new StateAckMessage(
                lastState.Id,
                client.connection.connectionId
                )
            );
    }

    public void OnStateAck(NetworkMessage msg)
    {
        var request = msg.ReadMessage<StateAckMessage>();

        if (!LastAck.ContainsKey(request.ConnectionId) || LastAck[request.ConnectionId] < request.Last)
        {
            LastAck[request.ConnectionId] = request.Last;
        }
    }
}
