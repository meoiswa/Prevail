using UnityEngine;
using System.Collections;

public class TestCharacterSpawner : MonoBehaviour {

    public GameObject PlayerNetControllerPrefab;
    public GameObject PlayerNetCharacterPrefab;


	// Use this for initialization
	void Start () {
        
        var ctrl = ((GameObject)Instantiate(PlayerNetControllerPrefab, Vector3.up * 10f, Quaternion.identity)).GetComponent<PlayerNetController>();
        var chara = ((GameObject)Instantiate(PlayerNetCharacterPrefab, Vector3.up * 10f, Quaternion.identity)).GetComponent<PlayerNetCharacter>();

        ctrl.Character = chara;
        chara.Controller = ctrl;

        ctrl.offline = true;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
