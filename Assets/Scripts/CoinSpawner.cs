using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    public GameObject[] Prefabs;
    public float SpawnTime;
    public float SpawnTimeDecreaseRatio = 1;
    public float Min, Max;
    public bool YAxis;

    public void Spawn()
    {
        int choice = Random.Range(0,Prefabs.Length-1);
        float Y = 0;
        if (Min == Max)
            Y = Max;
        else Y = Random.Range(Min, Max);
        print(Y);

        if (YAxis)
            Instantiate(Prefabs[choice], new Vector3(transform.position.x, Y, 0), Quaternion.identity);
        else
            if (choice < 0.5)
            Instantiate(Prefabs[choice], new Vector3(Y, transform.position.y, 0), Quaternion.identity);

        SpawnTime *= SpawnTimeDecreaseRatio;
        Invoke("Spawn", SpawnTime);

    }

    // Use this for initialization
    void Start()
    {
        Invoke("Spawn", SpawnTime);
    }
}
