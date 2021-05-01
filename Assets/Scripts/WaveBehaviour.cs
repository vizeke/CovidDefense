using System.Collections.Generic;
using UnityEngine;

public class WaveBehaviour : MonoBehaviour
{
    public List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
    private Graph road;
    private Wave wave;
    private float clearTime = 0;

    public event OnWaveFinished OnWaveFinishedEvent;
    public delegate void OnWaveFinished(Wave instance);

    // Update is called once per frame
    void Update()
    {
        if (wave != null && wave.Started && !wave.Finished)
        {
            clearTime += Time.deltaTime;
            if (!wave.Cleared && WaveCleared)
            {
                wave.Clear(clearTime);
            }

            if (wave.Cleared)
            {
                OnWaveFinishedEvent(this.wave);
            }
        }
    }

    public bool WaveCleared { get => enemySpawners.TrueForAll(es => es.SpanwerCleared); }

    public void StartWave(Graph road, Wave wave)
    {
        this.wave = wave;
        this.road = road;

        Debug.Log($"Starting wave: {wave.Name}");

        wave.Start();
        foreach (var enemy in wave.Spawners)
        {
            var spawner = gameObject.AddComponent<EnemySpawner>();
            spawner.Init(road, enemy);
            enemySpawners.Add(spawner);
        }
    }
}
