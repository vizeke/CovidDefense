using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Enemy : MonoBehaviour
{
    public GameObject enemyModel;
    public ParticleSystem deathParticles;

    public enum EnemyStatus
    {
        Alive,
        Dead
    }

    private Graph road;
    private EnemyData enemyData;
    private EnemyMovement enemyMovement;
    private EnemyStatus status;

    public event Onkill OnDeathEvent;
    public delegate void Onkill(Enemy instance);

    // Start is called before the first frame update
    void Start()
    {
        enemyData = gameObject.GetComponent<EnemyData>();
        enemyMovement = gameObject.GetComponent<EnemyMovement>();

        status = EnemyStatus.Alive;
        enemyMovement.Init(road);
    }

    public bool IsDead { get => status == EnemyStatus.Dead; }

    public void Init(Graph road)
    {
        this.road = road;
    }

    public EnemyStatus TakeHit(float damage)
    {
        enemyData.Health -= damage;

        if (enemyData.Health <= 0 && status == EnemyStatus.Alive)
        {
            Die();
        }

        return status;
    }

    public void Die()
    {
        Debug.Log($"Enemy killed {this.gameObject.GetInstanceID()}");

        status = EnemyStatus.Dead;

        if (enemyModel != null) enemyModel.SetActive(false);

        if (OnDeathEvent != null) this.OnDeathEvent(this);

        if (deathParticles != null) deathParticles.gameObject.SetActive(true);

        var totalDuration = (deathParticles != null) ? (deathParticles.duration + deathParticles.startLifetime) : 0;
        Destroy(gameObject, totalDuration);
    }
}
