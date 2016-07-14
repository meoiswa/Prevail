using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

class CustomNetworkManager : NetworkManager
{
    
    public GameObject bossPrefab { get; set; }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (conn.hostId == -1)
        {
            GameObject Boss = GameObject.Find("BossBase");
            NetworkServer.AddPlayerForConnection(conn, Boss, playerControllerId);
        }
        else
        {
            base.OnServerAddPlayer(conn, playerControllerId);
        }
    }
}
