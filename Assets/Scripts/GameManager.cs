using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData GameData;
    public RoadData Road;
    public bool Started;
    private IEnumerator<Wave> waves;
    private Wave currentWave;
    private int WaveIndex = 0;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Started)
        {
            if (waves == null)
            {
                waves = GameData.Waves.GetEnumerator();
                StartCoroutine(StartNextWave(null));
            }
        }
    }

    private void StartNextWaveAfterWaveFinished(Wave wave)
    {
        StartCoroutine(StartNextWave(wave));
    }

    private IEnumerator StartNextWave(Wave wave)
    {
        // Check if wave cleared
        if (wave != null && wave.Defeated)
        {
            // TODO: Game Over
        }

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        waves.MoveNext();
        currentWave = waves.Current;

        if (currentWave == null)
        {
            // Victory
        }
        else
        {
            if (!currentWave.Started)
            {
                InstanciateWave(currentWave);
            }
        }
    }

    private void InstanciateWave(Wave wave)
    {
        Debug.Log($"Instanciating wave: {wave.Name}");
        var waveBehaviour = gameObject.AddComponent<WaveBehaviour>();

        waveBehaviour.OnWaveFinishedEvent += StartNextWaveAfterWaveFinished;

        waveBehaviour.StartWave(Road.Graph, wave);
    }

    public void WaveFinished()
    {
        Debug.Log("Wave Finished");
    }
}

