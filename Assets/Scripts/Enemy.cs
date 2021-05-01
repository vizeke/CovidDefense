using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Enemy : MonoBehaviour
{
    public enum EnemyStatus
    {
        Alive,
        Dead
    }

    private EnemyData enemyData;
    private EnemyMovement enemyMovement;
    private EnemyStatus status;

    // Start is called before the first frame update
    void Start()
    {
        enemyData = gameObject.GetComponent<EnemyData>();
        enemyMovement = gameObject.GetComponent<EnemyMovement>();
        status = EnemyStatus.Alive;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsDead { get => status == EnemyStatus.Dead; }

    public EnemyStatus TakeHit(float damage)
    {
        enemyData.Health -= damage;

        if (enemyData.Health <= 0)
        {
            Debug.Log("Enemy killed");
            status = EnemyStatus.Dead;
            Destroy(gameObject);
        }

        return status;
    }
}
