using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField]
    private float duration;

    [SerializeField]
    private float radius;

    private PlayerHealth player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().GetComponent<PlayerHealth>();
        Invoke("Die", duration);
        var parent = GameObject.FindGameObjectWithTag("MovingWorld");
        transform.parent = parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < radius)
        {
            player.TakeDamage();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
