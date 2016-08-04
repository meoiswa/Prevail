using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MenuScript : MonoBehaviour {

    public InputField nameField;
    public InputField ipField;

    public GameObject Panel;
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void OnButtonConnect()
    {
        PrevailClient.Instance.PlayerName = nameField.text;
        PrevailClient.Instance.networkAddress = ipField.text;
        PrevailClient.Instance.StartClient();
        Panel.SetActive(false);
    }
}
