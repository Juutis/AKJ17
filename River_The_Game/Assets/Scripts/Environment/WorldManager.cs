using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class WorldManager : MonoBehaviour
{
    public static WorldManager main;

    [SerializeField]
    private LevelConfig level;


    [SerializeField]
    private ScaleToScreenSize bgScaler;

    private WorldMovement worldMovement;

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

    [SerializeField]
    private Transform playerStart;

    public Transform PlayerStart { get { return playerStart; } }
    public void Start()
    {
        worldMovement = GetComponent<WorldMovement>();
    }

    public void StopWorldMovement()
    {
        worldMovement.StopMoving();
    }

}

[System.Serializable]
public class WorldBackground
{
    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite { get { return sprite; } }
    [SerializeField]
    private Color color;
    public Color Color { get { return color; } }
}
