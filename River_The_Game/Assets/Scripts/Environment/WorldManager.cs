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
    private SpriteRenderer waterBackground;
    [SerializeField]
    private SpriteRenderer waterForeground;
    [SerializeField]
    private SpriteRenderer skyBackground;
    [SerializeField]
    private SpriteShapeRenderer groundBackground;

    [SerializeField]
    private ScaleToScreenSize bgScaler;

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
        SetupVisuals();
    }

    private void SetupVisuals()
    {
        waterBackground.sprite = level.WaterBackground.Sprite;
        waterBackground.color = level.WaterBackground.Color;
        waterForeground.sprite = level.WaterForeground.Sprite;
        waterForeground.color = level.WaterForeground.Color;
        skyBackground.sprite = level.SkyBackground.Sprite;
        skyBackground.color = level.SkyBackground.Color;
        groundBackground.color = level.GroundBackground.Color;
        bgScaler.ResizeSpriteToScreen();
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
