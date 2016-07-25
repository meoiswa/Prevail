using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class CityGeneratorScript : MonoBehaviour
{

    public GameObject CentralPlaza;

    [SerializeField]
    public List<GameObject> Prefabs;

    List<GameObject> Blocks;

    public int CountX = 9;
    public int CountY = 9;

    public int SizeX = 5;
    public int SizeY = 5;

    public int Seed;

    public void Generate()
    {
        Generate((int)System.DateTime.UtcNow.Ticks);
    }

    public void Generate(int value)
    {
        if (Blocks == null)
        {
            Blocks = new List<GameObject>();
        }
        else
        {
            foreach(var p in Blocks)
            {
                Destroy(p);
            }
            Blocks.Clear();
        }

        Seed = value;
        Random.seed = value;
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
                //StaticBatchingUtility.Combine(obj);

                Blocks.Add(obj);
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
        Generate();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
