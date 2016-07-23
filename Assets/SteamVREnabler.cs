using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class SteamVREnabler : MonoBehaviour {

    void Awake()
    {
        if (Application.isEditor || !Environment.GetCommandLineArgs().Any(s => s == "-steamvr"))
        {
            gameObject.SetActive(false);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
