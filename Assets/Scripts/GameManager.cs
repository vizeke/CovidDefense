using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public WaveData WaveData;
    public SnappingGrid Grid;
    public bool Started;
    private IEnumerator<Wave> waves;
    private Wave currentWave;
    private Graph road;
    private List<GameObject> roadBlocks;

    public GameObject coronavirus;

    public event OnInfectionChange OnInfectionChangeEvent;
    public delegate void OnInfectionChange();

    public event OnFundsChange OnFundsChangeEvent;
    public delegate void OnFundsChange();

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
                road = RoadProvider.GetDefaultRoad(Grid);
                roadBlocks = RoadProvider.GetRoadBlocks(road);
                waves = WaveData.Waves.GetEnumerator();
                StartCoroutine(StartNextWave(null));
            }
        }
    }

    private IEnumerator StartNextWave(Wave wave)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        waves.MoveNext();
        currentWave = waves.Current;

        if (currentWave == null)
        {
            // TODO: Victory
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
        waveBehaviour.gameManager = this;

        waveBehaviour.OnWaveFinishedEvent += (wave) => StartCoroutine(StartNextWave(wave));

        waveBehaviour.StartWave(road, wave);
    }

    public void WaveFinished()
    {
        Debug.Log("Wave Finished");
    }

    public void StartGame()
    {
        this.Started = true;
    }

    #region [ Health ]

    public int infectionPercentage = 0;
    private PlayerFund playerFund = new PlayerFund(100);

    public int GetInfection()
    {
        return infectionPercentage;
    }

    public int ApplyInfectionDelta(int delta)
    {
        infectionPercentage += delta;

        if (infectionPercentage >= 100)
        {
            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
        }

        OnInfectionChangeEvent();

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
        OnFundsChangeEvent();

        playerFund.Earn(value);
    }
    #endregion
}
