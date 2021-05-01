using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RoadData Road;

    public List<EnemySpawner> enemySpawners;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemySpawners.ForEach(s => s.Init(Road.Graph));
    }
}
