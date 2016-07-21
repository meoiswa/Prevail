using Prevail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkLobbyManager : NetworkLobbyManager
{
    public GameObject cameraRig;

    NetworkConnection hostConn;

    public override void OnLobbyStartServer()
    {
        base.OnLobbyStartServer();
    }

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        if (conn.hostId == -1)
        {
            hostConn = conn;
            return (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            return base.OnLobbyServerCreateGamePlayer(conn, playerControllerId);
        }
    }

    public override void OnLobbyServerSceneChanged(string sceneName)
    {
        base.OnLobbyServerSceneChanged(sceneName);

        cameraRig.SetActive(true);
    }

    public override void OnLobbyClientEnter()
    {
        new Game();
        base.OnLobbyClientEnter();
    }
}
