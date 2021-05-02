using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Tower : MonoBehaviour
{
    public GameObject towerModel;
    public PlaceableObject placeableObject;

    private bool active = false;
    private float lastAttack = 0;
    private List<Enemy> enemiesInsideRadius;
    private TowerData towerData;
    private Animator towerAnimatorController;
    private SphereCollider attackRangeCollider;

    // Start is called before the first frame update
    void Start()
    {
        if (towerModel != null)
        {
            towerAnimatorController = towerModel.GetComponent<Animator>();
            if (towerAnimatorController != null) towerAnimatorController.SetInteger("AnimState", 0);
        }

        attackRangeCollider = GetComponent<SphereCollider>();
        enemiesInsideRadius = new List<Enemy>();
        towerData = gameObject.GetComponent<TowerData>();

        placeableObject.OnChangeStateEvent += (placeableObject, state) =>
        {
            if (placeableObject.IsPlaced())
            {
                attackRangeCollider.enabled = true;
                if (towerAnimatorController != null) towerAnimatorController.SetInteger("AnimState", 1);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (towerAnimatorController != null)
        {
            // Debug.Log(towerAnimatorController.GetCurrentAnimatorStateInfo(0).IsName("tower_idle"));
        }

        active = (enemiesInsideRadius.Count > 0);

        if (active)
        {
            lastAttack += Time.deltaTime;

            if (lastAttack > towerData.AttackSpeed)
            {
                if (towerAnimatorController != null) towerAnimatorController.SetInteger("AnimState", 2);
                DamageEnemies();
                lastAttack = 0;
            }
        }
        else
        {
            lastAttack = 0;
            if (towerAnimatorController != null) towerAnimatorController.SetInteger("AnimState", 1);
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
                e.TakeHit(towerData.Damage);
            });

            enemiesInsideRadius = enemiesInsideRadius.Where(e => e.enabled && !e.IsDead).ToList();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy") return;

        enemiesInsideRadius.Add(other.gameObject.GetComponent<Enemy>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Enemy") return;

        enemiesInsideRadius.Remove(other.gameObject.GetComponent<Enemy>());
    }
}
