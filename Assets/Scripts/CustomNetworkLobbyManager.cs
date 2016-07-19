using Prevail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkLobbyManager : NetworkLobbyManager
{
    public GameObject cameraRigPrefab;

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        if (conn.hostId == -1)
        {
            var vrRig = (GameObject) GameObject.Instantiate(cameraRigPrefab, Vector3.zero, Quaternion.identity);
            vrRig.transform.localScale = Vector3.one * 40f;

            return (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            return base.OnLobbyServerCreateGamePlayer(conn, playerControllerId);
        }
    }

    public override void OnLobbyClientEnter()
    {
        new Game();
        base.OnLobbyClientEnter();
    }
}
