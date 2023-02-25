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

    public void ResizeSpriteToScreen()
    {
        if (sr == null) return;
        if (Camera.main.orthographic)
        {
            ResizeWithOrthographicCamera();
        }
        else
        {
            ResizeWithPerspectiveCamera();
        }
    }

    private void ResizeWithOrthographicCamera()
    {
        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector2 scale = transform.localScale;
        scale.x = scaleX ? worldScreenWidth / width : scale.x;
        scale.y = scaleY ? worldScreenHeight / height : scale.y;

        transform.localScale = scale;
    }
    private void ResizeWithPerspectiveCamera()
    {
        float spriteHeight = sr.sprite.bounds.size.y;
        float spriteWidth = sr.sprite.bounds.size.x;
        float distance = transform.position.z - Camera.main.transform.position.z;
        float screenHeight = 2 * Mathf.Tan(Camera.main.fieldOfView * Mathf.Deg2Rad / 2) * distance;
        float screenWidth = screenHeight * Camera.main.aspect;
        transform.localScale = new Vector3(screenWidth / spriteWidth, screenHeight / spriteWidth, 1);
    }

}
