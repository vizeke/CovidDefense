using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Graph road;

    private float debounce;
    private string enemy;
    private float count = 0;

    private float elapsedTime = 0;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (road != null)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= debounce)
            {
                elapsedTime = 0;
                InstanciateEnemy(enemy);
            }
        }
    }

    public void Init(Graph road, string enemy, float debounce)
    {
        this.road = road;
        this.enemy = enemy;
        this.debounce = debounce;
    }

    private void InstanciateEnemy(string name)
    {
        Debug.Log($"Instanciating {name}");
        var enemyResource = Resources.Load<GameObject>($"Prefabs/Enemies/{name}");
        var enemy = Instantiate(enemyResource, road.Source.Point, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().Init(road);
    }
}
