using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfines : MonoBehaviour
{
    [SerializeField]
    private List<PlayerConfine> confines;
    void Start()
    {
        foreach (PlayerConfine confine in confines)
        {
            PlaceConfine(confine);
            ScaleConfine(confine);
            confine.SpriteRenderer.enabled = false;
        }
    }

    private void PlaceConfine(PlayerConfine confine)
    {
        int pixelWidth = Camera.main.pixelWidth;
        int pixelHeight = Camera.main.pixelHeight;
        float zPos = Camera.main.transform.position.z;

        ConfinePlacement placement = confine.Placement;
        Vector3 position = Vector3.zero;
        if (placement == ConfinePlacement.Left)
        {
            position = new Vector3(pixelWidth, pixelHeight / 2f, zPos);
        }
        else if (placement == ConfinePlacement.Right)
        {
            position = new Vector3(0, pixelHeight / 2f, zPos);
        }
        else if (placement == ConfinePlacement.Top)
        {
            position = new Vector3(pixelWidth / 2f, 0f, zPos);
        }
        else if (placement == ConfinePlacement.Bottom)
        {
            position = new Vector3(pixelWidth / 2f, pixelHeight, zPos);
        }
        else if (placement == ConfinePlacement.Custom)
        {
            position = new Vector3(pixelWidth * confine.CustomX, pixelHeight * confine.CustomY, zPos);

        }
        Vector3 pos = Camera.main.ScreenToWorldPoint(position);
        confine.SpriteRenderer.transform.position = new Vector2(pos.x, pos.y);
    }

    private void ScaleConfine(PlayerConfine confine)
    {
        SpriteRenderer sr = confine.SpriteRenderer;
        int pixelWidth = Camera.main.pixelWidth;
        int pixelHeight = Camera.main.pixelHeight;
        float height = sr.sprite.bounds.size.y;
        float width = sr.sprite.bounds.size.x;
        Vector2 scale = Vector2.one;

        if (confine.Scaling == ConfineScaling.FullHeight)
        {

            scale.y = pixelHeight / height;
        }
        else if (confine.Scaling == ConfineScaling.FullWidth)
        {
            scale.x = pixelWidth / width;
        }

        sr.transform.localScale = scale;
    }


}

[System.Serializable]
public class PlayerConfine
{
    public SpriteRenderer SpriteRenderer;
    public ConfineScaling Scaling;
    public ConfinePlacement Placement;
    [Range(0, 1)]
    public float CustomY;
    [Range(0, 1)]
    public float CustomX;
}

public enum ConfineScaling
{
    None,
    FullWidth,
    FullHeight
}


public enum ConfinePlacement
{
    Left,
    Right,
    Top,
    Bottom,
    Custom
}
