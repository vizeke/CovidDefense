using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public RoadData Road;
    public float speed = 1.0f;

    private Vertex currentSource = null;
    private float currentSourceSwitchTime;
    private Vertex currentDestination = null;
    private float timeForCurrentPath;

    // Start is called before the first frame update
    void Start()
    {
        CalculateNextPath(Road.Graph.Source);
    }

    // Update is called once per frame
    void Update()
    {
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
                // 3.b 
                Destroy(gameObject);

                Application.Quit();

                // AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                // AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                // TODO: deduct health
            }
        }
    }

    private void CalculateNextPath(Vertex source)
    {
        // 1 
        currentSource = source;
        currentDestination = Road.Graph.AdjacencyList.First(e => e.Name == currentSource.Edges.First());
        currentSourceSwitchTime = Time.time;

        float pathLength = Vector3.Distance(source.Point, currentDestination.Point);
        timeForCurrentPath = pathLength / speed;
    }
}
