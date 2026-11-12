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
    [SerializeField] private Transform[] spawnPosition;
    [SerializeField] private GameObject customerPrefab;

    private List<GameObject> activeCustomers = new List<GameObject>();

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
        GameObject newCustomer = Instantiate(customerPrefab, spawnPosition[row].position, spawnPosition[row].rotation);
        activeCustomers.Add(newCustomer);

        // Subscribe to the OnCustomerDestroyed event of the Customer script
        Customer customerScript = newCustomer.GetComponent<Customer>();
        if (customerScript != null)
        {
            // Unsubscribe before subscribing to prevent multiple event handler registrations.
            customerScript.OnCustomerDestroyed -= HandleCustomerDestruction;
            customerScript.OnCustomerDestroyed += HandleCustomerDestruction;
        }
        else
        {
            Debug.LogError("Customer script not found on the instantiated customer prefab.");
        }
    }

    public void HandleCustomerDestruction(GameObject customer)
    {
        if (activeCustomers.Contains(customer))
        {
            activeCustomers.Remove(customer);
            Destroy(customer);
        }
    }

    public void ResetCustomerSpawn()
    {
        for (int i = 0; i < 4; i++)
        {
            timeSinceLastSpawn[i] = Time.time;
            SpawnCustomer(i);
        }
    }

    public void ClearActiveCustomers()
    {
        StopAllCoroutines(); // Stop any ongoing coroutines to prevent new customers from spawning while clearing the list.
        
        foreach (GameObject customer in activeCustomers)
        {
            if (customer != null)
            {
                Destroy(customer);
            }
        }
        activeCustomers.Clear();
    }
}

