using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class SteamVREnabler : MonoBehaviour {

    public static SteamVREnabler Instance;

    public bool EnableInEditor = false;

    public bool IsEnabled { get; private set; }

    void Awake()
    {
        Instance = this;
        
        string debugMsg = "SteamVR Mode:";
        if (Application.isEditor)
        {
            debugMsg += " in Editor";
            if (!EnableInEditor)
            {
                debugMsg += ", explicitly disabled";
                gameObject.SetActive(false);
                IsEnabled = false;
            }
            else
            {
                debugMsg += ", enabled";
                IsEnabled = true;
            }
        }
        else
        {
            if (Environment.GetCommandLineArgs().Any(s => s == "-steamvr"))
            {
                debugMsg += ", -steamvr argument detected, enabled";
                IsEnabled = true;
            }
            else
            {
                debugMsg += ", disabled";
                gameObject.SetActive(false);
                IsEnabled = false;
            }
        }
        Debug.Log(debugMsg);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
