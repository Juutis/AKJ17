using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator anim;

    public string FireAnimation = "Shoot";
    public string IdleAnimation = "Idle";

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        anim.Play(FireAnimation);
    }

    public void Disable()
    {
        anim.enabled = false;
        enabled = false;
    }

}
