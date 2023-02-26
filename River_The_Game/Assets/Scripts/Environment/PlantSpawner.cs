using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] plants;

    [SerializeField]
    private Color tint = Color.white;

    [SerializeField]
    private int sortingOrder = 0;
    [SerializeField]
    private string sortingLayerName = "Default";

    private float width = 500;

    // Start is called before the first frame update
    void Start()
    {
        generate();
    }

    private void generate()
    {
        var i = 0.0f;
        while (i < width)
        {
            var gap = Random.Range(1.5f, 3.0f);
            i += gap;

            var plant = Instantiate(plants[Random.Range(0, plants.Length)], transform);
            plant.transform.localPosition = new Vector2(i, 0.0f);
            var spriteRenderer = plant.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.color = tint;
            spriteRenderer.sortingOrder = sortingOrder;
            spriteRenderer.sortingLayerName = sortingLayerName;
            var t = Random.Range(0.9f, 1.1f);
            plant.transform.localScale = new Vector3(t, t, t);
        }
    }
}
