using System.Collections.Generic;
using UnityEngine;

public class WaveBehaviour : MonoBehaviour
{
    public List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
    public event OnWaveFinished OnWaveFinishedEvent;
    public delegate void OnWaveFinished(Wave instance);
    [HideInInspector]
    public GameManager gameManager;

    private Graph road;
    private Wave wave;
    private float clearTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (wave != null && wave.Started && !wave.Finished)
        {
            clearTime += Time.deltaTime;
            if (!wave.Finished && WaveFinished)
            {
                wave.Clear(clearTime);
            }

            if (wave.Finished)
            {
                OnWaveFinishedEvent(this.wave);
            }
        }
    }

    public bool WaveFinished { get => enemySpawners.TrueForAll(es => es.SpanwerFinished); }

    public void StartWave(Graph road, Wave wave)
    {
        this.wave = wave;
        this.road = road;

        Debug.Log($"Starting wave: {wave.Name}");

        wave.Start();
        foreach (var enemy in wave.Spawners)
        {
            var spawner = gameObject.AddComponent<EnemySpawner>();
            spawner.gameManager = gameManager;
            spawner.Init(road, enemy);
            enemySpawners.Add(spawner);
        }
    }
}
