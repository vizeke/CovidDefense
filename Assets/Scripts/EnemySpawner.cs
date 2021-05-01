using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Graph road;

    private SpawningEnemy spawningEnemy;
    private float elapsedTime = 0;
    private bool started = false;
    private int enemiesSpawned = 0;

    private List<Enemy> enemies = new List<Enemy>();

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (road != null)
        {
            elapsedTime += Time.deltaTime;

            if (started)
            {
                if (enemiesSpawned < spawningEnemy.Amount && elapsedTime >= spawningEnemy.Debounce)
                {
                    elapsedTime = 0;
                    InstanciateEnemy(spawningEnemy.Enemy);
                }
            }
            else
            {
                started = elapsedTime >= spawningEnemy.Delay;
            }
        }
    }

    public bool SpanwerCleared { get => enemiesSpawned > 0 && enemies.Count == 0; }

    public void Init(Graph road, SpawningEnemy spawningEnemy)
    {
        this.road = road;
        this.spawningEnemy = spawningEnemy;
    }

    private void InstanciateEnemy(string name)
    {
        Debug.Log($"Instanciating {name}");
        enemiesSpawned++;

        var enemyResource = Resources.Load<GameObject>($"Prefabs/Enemies/{name}");
        var enemyGameObject = Instantiate(enemyResource, road.Source.Point, Quaternion.identity);
        var enemy = enemyGameObject.GetComponent<Enemy>();

        enemy.OnKillEvent += (enemy) =>
        {
            enemies.Remove(enemy);
        };

        enemy.Init(road);
        enemies.Add(enemy);
    }
}
