using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using Prevail.Model;
using System;
using Prevail.Model.Net;
using UnityEngine.SceneManagement;

public class PrevailClient : NetworkManager
{
    public CityGeneratorScript City;

    public string PlayerName;
    public Color PlayerColor;
    
    public GameObject playerNetControllerPrefab;
    public GameObject playerNetCharacterPrefab;
    public GameObject bossPrefab;
        
    public static PrevailClient Instance;

    
    public void Start()
    {
        Instance = this;
    }
    
    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);

        client.RegisterHandler(StateSendMessage.MsgId, OnStateReceived);
        client.RegisterHandler(CitySyncMessage.MsgId, OnCitySync);
        
        ClientScene.RegisterPrefab(playerNetControllerPrefab);
        ClientScene.RegisterPrefab(playerNetCharacterPrefab);
        ClientScene.RegisterPrefab(bossPrefab);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        if (PlayerColor.r == 0 && PlayerColor.g == 0 && PlayerColor.b == 0)
        {
            PlayerColor = new Color(UnityEngine.Random.Range(0.5f, 1f), UnityEngine.Random.Range(0.5f, 1f), UnityEngine.Random.Range(0.5f, 1f));
        }
        ClientScene.AddPlayer(conn, 0, new PlayerSettingsMessage(PlayerName, PlayerColor, false));
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        Debug.Log("Client Disconnected");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FixedUpdate()
    {
        if (lastState != null && pendingApply)
        {
            lastState.ClientApply(Time.fixedTime);
            pendingApply = false;
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
    }

    public void OnCitySync(NetworkMessage msg)
    {
        var seed = msg.ReadMessage<CitySyncMessage>().Seed;

        City.Generate(seed);
    }
}
