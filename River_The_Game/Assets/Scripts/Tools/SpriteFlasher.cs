using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlasher : MonoBehaviour
{

    private bool isFlashing = false;

    [SerializeField]
    private float flashInDuration = 0.2f;
    [SerializeField]
    private float flashOutDuration = 0.1f;

    private bool flashingIn = false;
    private bool flashingOut = false;
    private float flashTimer = 0f;

    [SerializeField]
    private bool hasDuration = false;
    [SerializeField]
    private float flashDuration = 0.3f;
    private float totalFlashTimer = 0f;

    [SerializeField]
    private Color flashColor;

    [SerializeField]
    private bool allChildSpritesFlash = false;

    [SerializeField]
    private List<SpriteRenderer> spritesToFlash = new List<SpriteRenderer>();

    private List<FlashingSprite> flashingSprites = new List<FlashingSprite>();

    private void Start()
    {
        if (allChildSpritesFlash)
        {
            foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
            {
                flashingSprites.Add(new FlashingSprite(sprite));
            }
        }
        else
        {
            foreach (SpriteRenderer sprite in spritesToFlash)
            {
                flashingSprites.Add(new FlashingSprite(sprite));
            }
        }
    }

    public void StartFlashing()
    {
        totalFlashTimer = 0f;
        isFlashing = true;
        flashTimer = 0f;
        flashingIn = true;
    }

    public void StopFlashing()
    {
        totalFlashTimer = 0f;
        flashingIn = false;
        flashingOut = false;
        BackToOriginalColor();
    }

    private void Update()
    {
        if (isFlashing)
        {
            HandleFlashing();
        }
    }

    private void HandleFlashing()
    {
        if (hasDuration)
        {
            totalFlashTimer += Time.deltaTime;
            if (totalFlashTimer > flashDuration)
            {
                StopFlashing();
            }
        }
        if (flashingIn || flashingOut)
        {
            flashTimer += Time.deltaTime;
        }
        if (flashingIn)
        {
            if (flashTimer >= flashInDuration)
            {
                flashingIn = false;
                flashingOut = true;
                flashTimer = 0f;
            }
            FlashIn(flashColor, flashTimer / flashInDuration);
        }
        if (flashingOut)
        {
            if (flashTimer >= flashOutDuration)
            {
                flashingOut = false;
                flashingIn = true;
                flashTimer = 0f;
            }
            FlashOut(flashColor, flashTimer / flashOutDuration);
        }
    }

    private void FlashIn(Color flashingColor, float lerpT)
    {
        foreach (FlashingSprite flashingSprite in flashingSprites)
        {
            flashingSprite.SpriteRenderer.color = Color.Lerp(flashingSprite.OriginalColor, flashingColor, lerpT);
        }
    }
    private void FlashOut(Color flashingColor, float lerpT)
    {
        foreach (FlashingSprite flashingSprite in flashingSprites)
        {
            flashingSprite.SpriteRenderer.color = Color.Lerp(flashingColor, flashingSprite.OriginalColor, lerpT);
        }
    }

    private void BackToOriginalColor()
    {
        foreach (FlashingSprite flashingSprite in flashingSprites)
        {
            flashingSprite.SpriteRenderer.color = flashingSprite.OriginalColor;
        }
    }


}


public class FlashingSprite
{
    public FlashingSprite(SpriteRenderer sr)
    {
        SpriteRenderer = sr;
        OriginalColor = sr.color;
    }
    public SpriteRenderer SpriteRenderer;
    public Color OriginalColor;
}