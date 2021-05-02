using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [HideInInspector]
    public GameManager gameManager;

    private Graph road;
    private SpawningEnemy spawningEnemy;
    private float elapsedTime = 0;
    private bool started = false;
    private int enemiesSpawned = 0;
    private int enemiesToTheEnd = 0;
    private int deadEnemies = 0;

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

    public bool SpanwerFinished { get => deadEnemies + enemiesToTheEnd == spawningEnemy.Amount; }

    public void Init(Graph road, SpawningEnemy spawningEnemy)
    {
        this.spawningEnemy = spawningEnemy;
        this.road = road;
    }

    private void InstanciateEnemy(string name)
    {
        Debug.Log($"Instanciating {name}");
        enemiesSpawned++;

        var enemyResource = Resources.Load<GameObject>($"Prefabs/Enemies/{name}");
        var enemyGameObject = Instantiate(enemyResource, road.Source.Point, Quaternion.identity);
        var enemy = enemyGameObject.GetComponent<Enemy>();
        var enemyMovement = enemyGameObject.GetComponent<EnemyMovement>();
        enemyMovement.gameManager = this.gameManager;

        enemy.OnDeathEvent += (enemy) =>
        {
            deadEnemies++;
            enemies.Remove(enemy);
        };

        enemyMovement.OnEnemyToTheEndEvent += (enemyMovement) =>
        {
            enemiesToTheEnd++;
        };

        enemy.Init(road);
        enemies.Add(enemy);
    }
}
