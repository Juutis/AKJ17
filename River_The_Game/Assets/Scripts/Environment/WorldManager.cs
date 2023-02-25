using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager main;

    private void Awake()
    {
        main = this;
    }

    [SerializeField]
    private Transform pickupContainer;

    public Transform PickupContainer
    {
        get
        {
            return pickupContainer;
        }
    }
}
