using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private MovementType type;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float phaseOffset;
    [SerializeField]
    private float patternSpeed;

    private float startTime;
    private Vector3 startPos;
    private float dir = 1;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float fromStart = Time.time - startTime;
        if (type == MovementType.Sin)
        {
            transform.localPosition = startPos + Vector3.up * Mathf.Sin(fromStart * patternSpeed + phaseOffset) * 1.5f;
        }
        else if (type == MovementType.Circle)
        {
            transform.localPosition = startPos + Vector3.up * Mathf.Sin(fromStart * patternSpeed + phaseOffset) * 1.5f
                                          + Vector3.left * Mathf.Cos(fromStart * patternSpeed + phaseOffset) * 1.5f;
        }
        else if (type == MovementType.GoPastAndTurnBack)
        {
            transform.localPosition += Vector3.left * movementSpeed * Time.deltaTime;
            if (transform.position.x < -8f && dir == 1)
            {
                dir = -1;
                movementSpeed = dir * 2 * movementSpeed;
            }
            // Move forward
            // Hit a trigger at the end of view port?
            // Go down (and/or up?) and lerp forward movement speed X into -X
        }
    }
}

enum MovementType
{
    Sin,
    Circle,
    GoPastAndTurnBack
}
