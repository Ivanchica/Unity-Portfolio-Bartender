using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Timers;
using TreeEditor;
using Unity.Mathematics;
using UnityEngine;
using UnityEditor;
using UnityEngineInternal;

public class CustomerSpawner : MonoBehaviour
{
    public Transform[] spawnPosition;
    public GameObject customerPrefab;

    private float[] timeSinceLastSpawn;
    private float[] spawnTimer;
    private float[] deltaTimer;

    public static int customerCount;


    private void Awake()
    {
        spawnTimer = new float[4] { 12, 13, 14, 15 };
        timeSinceLastSpawn = new float[4];
        deltaTimer = new float[4];
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetCustomerSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameEnding.GameState == GameState.Playing || GameEnding.GameState == GameState.RestartLevel)
        {
            checkSpawning();
        }
    }

    void checkSpawning()
    {
        for (int i = 0; i < 4; i++)
        {
            deltaTimer[i] = Time.time - timeSinceLastSpawn[i];
            if (deltaTimer[i] > spawnTimer[i])
            {
                timeSinceLastSpawn[i] = Time.time;
                SpawnCustomer(i);
            }
        }
    }

    void SpawnCustomer(int row)
    {
        Instantiate(customerPrefab, spawnPosition[row].position, spawnPosition[row].rotation);
    }

    public void ResetCustomerSpawn()
    {
        for (int i = 0; i < 4; i++)
        {
            timeSinceLastSpawn[i] = Time.time;
            SpawnCustomer(i);
        }
    }
}

