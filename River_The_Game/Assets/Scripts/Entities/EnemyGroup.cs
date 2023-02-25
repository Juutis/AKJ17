using System.Collections;
using System.Collections.Generic;
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
        if (enemies.Count == 0)
        {
            pickupDrop.Drop(enemy.transform.position);
            Destroy(gameObject);
        }
    }
}
