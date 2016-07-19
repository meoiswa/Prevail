using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


public class CustomNetworkLobbyManager : NetworkLobbyManager
{
    public GameObject bossPrefab { get; set; }
    
    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        if (conn.hostId == -1)
        {

            return (GameObject) GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            return base.OnLobbyServerCreateGamePlayer(conn, playerControllerId);
        }
    }
}
