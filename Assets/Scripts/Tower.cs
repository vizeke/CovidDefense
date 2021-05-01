using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Tower : MonoBehaviour
{
    private bool active = false;
    private float lastAttack = 0;
    private List<Enemy> enemiesInsideRadius;
    private TowerData towerData;

    // Start is called before the first frame update
    void Start()
    {
        enemiesInsideRadius = new List<Enemy>();
        towerData = gameObject.GetComponent<TowerData>();
    }

    // Update is called once per frame
    void Update()
    {
        active = enemiesInsideRadius.Any();

        if (active)
        {
            // Debug.Log("Tower activated");
            lastAttack += Time.deltaTime;

            if (lastAttack > towerData.AttackSpeed)
            {
                DamageEnemies();
                lastAttack = 0;
            }
        }
        else
        {
            lastAttack = 0;
        }
    }

    private void DamageEnemies()
    {
        // Debug.Log("Damaging enemies");
        // Debug.Log($"{enemiesInsideRadius.Count()} enemies on sight");

        if (towerData.AreaAttack)
        {
            enemiesInsideRadius.ForEach(e =>
            {
                Debug.Log(e);
                e.TakeHit(towerData.Damage);
            });

            enemiesInsideRadius = enemiesInsideRadius.Where(e => e.enabled && !e.IsDead).ToList();
        }
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy") return;

        enemiesInsideRadius.Add(other.gameObject.GetComponent<Enemy>());
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Enemy") return;

        enemiesInsideRadius.Remove(other.gameObject.GetComponent<Enemy>());
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerStay(Collider other)
    {

    }
}
