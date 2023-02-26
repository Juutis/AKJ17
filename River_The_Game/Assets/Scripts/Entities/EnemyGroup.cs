using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    private List<Enemy> enemies = new List<Enemy>();
    [SerializeField]
    private PickupDrop pickupDrop;
    void Start()
    {
        foreach (Transform child in transform)
        {
            Enemy enemy = child.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.RegisterGroup(this);
                enemies.Add(enemy);
            }
        }
    }
    public void EnemyKilled(Enemy enemy)
    {
        enemies.Remove(enemy);
        if (enemies.All(x => !x.Alive))
        {
            pickupDrop.Drop(enemy.transform.position);
            Invoke("DestroyThis", 10f);
        }
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
