using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class CityGenScript : NetworkBehaviour
{

    public GameObject CentralPlaza;
    


    [SerializeField]
    public List<GameObject> Prefabs;
    [SerializeField]
    public List<GameObject> Quirks;

    public int CountX = 9;
    public int CountY = 9;

    public int SizeX = 5;
    public int SizeY = 5;

    [SyncVar]
    public int Seed;

    void Generate(int value)
    {        
        Random.seed = Seed;
        for (int i = 0; i < CountX; i++)
        {
            for (int j = 0; j < CountY; j++)
            {
                var x = i - 4;
                var z = j - 4;

                GameObject obj;

                if (x == 0 && z == 0)
                {
                    obj = (GameObject)GameObject.Instantiate(
                    CentralPlaza,
                    new Vector3(x * SizeX * 5 * 2 + x * 5 * 2, 0, z * SizeY * 5 * 2 + z * 5 * 2),
                    Quaternion.identity);
                }
                else
                {
                    obj = (GameObject)GameObject.Instantiate(
                    Prefabs[Random.Range(0, Prefabs.Count)],
                    new Vector3(x * SizeX * 5 * 2 + x * 5 * 2, 0, z * SizeY * 5 * 2 + z * 5 * 2),
                    Quaternion.Euler(new Vector3(0, Random.Range(0, 3) * 90, 0)));
                }

                obj.transform.SetParent(this.transform, true);

                //Static batching for the newly instantiated city, improves fps twofold
                StaticBatchingUtility.Combine(obj);
            }
        }
    }
    void GenerateQuirks()
    {
        Random.seed = Seed;
        for (int i = 0; i < CountX-1; i++)
        {
            for (int j = 0; j < CountY-1; j++)
            {
                var x = i - 4;
                var z = j - 4;

                GameObject obj;
                if (Random.Range(0, 100) > 80)
                {
                    obj = (GameObject)GameObject.Instantiate(
                    Quirks[Random.Range(0, Quirks.Count)],
                    new Vector3(x * 60 + 30, 1, z * 60 + 30),
                    Quaternion.identity);
                    obj.transform.SetParent(this.transform, true);

                    NetworkServer.Spawn(obj);
                }

            }
        }
    }
    // Use this for initialization
    void Start()
    {
        if (Prefabs.Count == 0)
        {
            return;
        }

        if (isServer)
        {
            Seed = (int)System.DateTime.UtcNow.Ticks;
            GenerateQuirks();
        }

        Generate(Seed);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
