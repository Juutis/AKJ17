using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private PickupEffect effect;
    public PickupEffect Effect { get { return effect; } }

    [SerializeField]
    private SpriteRenderer iconRenderer;
    public void Initialize()
    {
        //
        iconRenderer.sprite = effect.Sprite;
    }

    public void Collect()
    {
        ProcessEffect();
        Kill();
    }

    private void ProcessEffect()
    {
        if (effect.Type == PickupEffectType.IncreaseBubbleFireRate)
        {
            GameManager.main.IncreaseBubbleFireRate(effect.Value);
        }
        else if (effect.Type == PickupEffectType.NextLevel)
        {
            GameManager.main.NextLevel();
        }
        else if (effect.Type == PickupEffectType.MainGunUpgrade)
        {
            GameManager.main.UpgradeMainGun();
        }
        else if (effect.Type == PickupEffectType.SideGunUpgrade)
        {
            GameManager.main.UpgradeSideGun();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"OnTrigger: {other.gameObject.tag}");
        if (other.gameObject.tag == "Player")
        {
            Collect();
        }
    }
}

[System.Serializable]
public class PickupEffect
{
    [SerializeField]
    private PickupEffectType type;
    public PickupEffectType Type { get { return type; } }

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite { get { return sprite; } }

    [SerializeField]
    private int value = 1;
    public int Value { get { return value; } }

    public override string ToString()
    {
        return $"[{type}]:[{value}]";
    }
}

public enum PickupEffectType
{
    IncreaseBubbleFireRate,
    NextLevel,
    MainGunUpgrade,
    SideGunUpgrade
}
