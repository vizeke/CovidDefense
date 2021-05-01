using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float EnemyGap = 3;
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject Enemy;
    private Graph road;
    private float elapsedTime = 0;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (road != null)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= EnemyGap)
            {
                elapsedTime = 0;
                InstanciateEnemy();
            }
        }
    }

    public void Init(Graph road)
    {
        this.road = road;
    }

    private void InstanciateEnemy()
    {
        var enemy = Instantiate(Enemy, road.Source.Point, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().Init(road);
    }
}
