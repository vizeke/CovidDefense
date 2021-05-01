using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WaveData WaveData;
    public RoadData Road;
    public SnappingGrid Grid;
    public bool Started;
    private IEnumerator<Wave> waves;
    private Wave currentWave;
    private int WaveIndex = 0;

    public GameObject coronavirus;

    // Start is called before the first frame update
    //void Start() {
    //    StartCoroutine(testDie());
    //}

    //IEnumerator testDie()
    //{
    //    yield return new WaitForSeconds(3);

    //    coronavirus.GetComponent<Enemy>().Die();
    //}

    // Update is called once per frame
    void Update()
    {
        if (Started)
        {
            if (waves == null)
            {
                waves = WaveData.Waves.GetEnumerator();
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

    #region [ Health ]

    public int infectionPercentage = 20;
    private PlayerFund playerFund = new PlayerFund(100);

    public int GetInfection()
    {
        return infectionPercentage;
    }

    public int ApplyInfectionDelta(int delta)
    {
        infectionPercentage += delta;
        return infectionPercentage;
    }
    #endregion

    #region [ Funds ]
    public int GetFund()
    {
        return playerFund.Amount;
    }

    public bool Spend(int value)
    {
        return playerFund.Spend(value);
    }

    public void Earn(int value)
    {
        playerFund.Earn(value);
    }
    #endregion
}
