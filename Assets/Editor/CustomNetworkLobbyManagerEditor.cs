using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{

    [CustomEditor(typeof(CustomNetworkLobbyManager))]
    public class CustomNetworkLobbyManagerInspector : NetworkManagerEditor
    {
        public override void OnInspectorGUI()
        {
            CustomNetworkLobbyManager myTarget = (CustomNetworkLobbyManager)target;

            myTarget.cameraRig = (GameObject)EditorGUILayout.ObjectField("SteamVR Camera Rig", myTarget.cameraRig, typeof(GameObject), true);

            base.OnInspectorGUI();
        }
    }
}
