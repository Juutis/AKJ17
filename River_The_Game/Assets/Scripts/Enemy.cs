using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float hp;

    private EnemyGroup enemyGroup;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void RegisterGroup(EnemyGroup group)
    {
        enemyGroup = group;
    }

    private void OnDestroy()
    {
        PickupDrop drop = GetComponent<PickupDrop>();
        if (drop != null)
        {
            drop.Drop(transform.position);
        }
        if (enemyGroup != null)
        {
            enemyGroup.EnemyKilled(this);
        }
    }
}
