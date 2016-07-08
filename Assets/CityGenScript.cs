using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CityGenScript : MonoBehaviour {

    [SerializeField]
    public List<GameObject> Prefabs;

    public int CountX = 9;
    public int CountY = 9;

    public int SizeX = 5;
    public int SizeY = 5;

	// Use this for initialization
	void Start () {
	    if (Prefabs.Count == 0)
        {
            return;
        }

        for(int i=0; i<CountX; i++)
        {
            for (int j=0; j<CountY; j++)
            {
                var x = i - 4;
                var z = j - 4;

                var obj = GameObject.Instantiate(
                    Prefabs[Random.Range(0, Prefabs.Count)],
                    new Vector3(x * SizeX * 5 * 2 + x*5*2, 0, z * SizeY * 5 * 2 + z*5*2),
                    Quaternion.identity);

                ((GameObject)obj).transform.SetParent(this.transform,true);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
