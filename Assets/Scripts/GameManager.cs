using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RoadData Road;

    public List<EnemySpawner> enemySpawners;

    public bool initiated = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!initiated)
        {
            foreach (var enemy in FirstWave)
            {
                var spawner = gameObject.AddComponent<EnemySpawner>();
                spawner.Init(Road.Graph, enemy.Key, enemy.Value);
                enemySpawners.Add(spawner);
            }

            initiated = true;
        }
    }

    public Dictionary<string, int> FirstWave = new Dictionary<string, int>()
    {
        { "Capsule", 2 },
        { "Sphere", 5 },
    };
}
