using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private int shootingLayer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        shootingLayer = anim.GetLayerIndex("Shooting Layer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        anim.Play("Shoot", shootingLayer, 0.0f);
    }
}
