using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToScreenSize : MonoBehaviour
{
    [SerializeField]
    private bool scaleX = true;
    [SerializeField]
    private bool scaleY = true;

    [SerializeField]
    private SpriteRenderer sr;

    void Start()
    {
        ResizeSpriteToScreen();
    }

    public void ResizeSpriteToScreen() {
        if (sr == null) return;

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector2 scale = transform.localScale;
        scale.x = scaleX ? worldScreenWidth / width : scale.x;
        scale.y = scaleY ? worldScreenHeight / height : scale.y;

        transform.localScale = scale;
    }

}
