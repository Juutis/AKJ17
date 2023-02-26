using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator anim;

    public string FireAnimation = "Shoot";
    public string IdleAnimation = "Idle";
    private int shootingLayer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play(IdleAnimation);
        shootingLayer = anim.GetLayerIndex("Shooting Layer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        anim.Play(FireAnimation, shootingLayer, 0.0f);
    }

    public void Disable()
    {
        anim.enabled = false;
        enabled = false;
    }

}
