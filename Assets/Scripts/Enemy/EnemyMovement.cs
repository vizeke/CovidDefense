using System;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Graph road;
    private Vertex currentSource = null;
    private float currentSourceSwitchTime;
    private Vertex currentDestination = null;
    private float timeForCurrentPath;
    [HideInInspector]
    public GameManager gameManager;
    private EnemyData enemyData;

    public event OnEnemyToTheEnd OnEnemyToTheEndEvent;
    public delegate void OnEnemyToTheEnd(EnemyMovement instance);

    // Start is called before the first frame update
    void Start()
    {
        enemyData = gameObject.GetComponent<EnemyData>();
    }

    public void Init(Graph road = null)
    {
        this.road = road;

        if (this.road != null) CalculateNextPath(road.Source);
    }

    // Update is called once per frame
    void Update()
    {
        if (road == null) return;

        // 1
        float currentTimeOnPath = Time.time - currentSourceSwitchTime;
        gameObject.transform.position = Vector3.Lerp(currentSource.Point, currentDestination.Point, currentTimeOnPath / timeForCurrentPath);
        // 3 
        if (gameObject.transform.position.Equals(currentDestination.Point))
        {
            // TODO: check sink
            if (currentDestination.Edges.Count() > 0)
            {
                // 3.a 
                CalculateNextPath(currentDestination);
                // TODO: Rotate into move direction
            }
            else
            {
                InfectPlayer();
                // 3.b 


                // AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                // AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                // TODO: deduct health
            }
        }
    }

    private void InfectPlayer()
    {
        gameManager.ApplyInfectionDelta(enemyData.Infection);
        OnEnemyToTheEndEvent(this);
        Destroy(gameObject);
    }

    private void CalculateNextPath(Vertex source)
    {
        // 1 
        currentSource = source;
        currentDestination = road.AdjacencyList.First(e => e.Name == currentSource.Edges.First());
        currentSourceSwitchTime = Time.time;

        float pathLength = Vector3.Distance(source.Point, currentDestination.Point);
        timeForCurrentPath = pathLength / gameObject.GetComponent<EnemyData>().Speed;
    }
}
