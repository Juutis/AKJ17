using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private MovementType type;
    [SerializeField]
    private float phaseOffset;
    [SerializeField]
    private float patternSpeed;
    [SerializeField]
    private float baseMovementSpeed;

    [Header("Circle and sin type")]
    [SerializeField]
    private float patternAmplitude;

    [Header("Go past types")]
    [SerializeField]
    private float movementSpeed;

    private float startTime;
    private Vector3 startPos;
    private float dir = 1;
    private bool movementEnabled;
    private bool turnBack = false;
    private float currentMovementSpeed;
    private float turnBackT = 0;
    private Vector3 playerDirection;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        startTime = Time.time;
        startPos = transform.localPosition;
        movementEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!movementEnabled) return;

        float fromStart = Time.time - startTime;
        if (type == MovementType.Sin)
        {
            transform.localPosition = startPos
                + Vector3.up * Mathf.Sin(fromStart * patternSpeed + phaseOffset) * patternAmplitude;

            startPos += Vector3.left * baseMovementSpeed * Time.deltaTime;
        }
        else if (type == MovementType.Circle)
        {
            transform.localPosition = startPos
                + Vector3.up * Mathf.Sin(fromStart * patternSpeed + phaseOffset) * patternAmplitude
                + Vector3.left * Mathf.Cos(fromStart * patternSpeed + phaseOffset) * patternAmplitude;

            startPos += Vector3.left * baseMovementSpeed * Time.deltaTime;
        }
        else if (type == MovementType.GoPastAndTurnBack)
        {

            transform.localPosition += Vector3.left * currentMovementSpeed * Time.deltaTime;
            if (turnBack)
            {
                dir = -1;
                currentMovementSpeed = Mathf.Lerp(movementSpeed, dir * 2 * movementSpeed, turnBackT);
                turnBackT += Time.deltaTime * 2f;
                // Lerp movement up/down?
            }
            else
            {
                currentMovementSpeed = movementSpeed;
            }
        }
        else if (type == MovementType.GoPastAndTowardPlayer)
        {
            if (turnBackT >= 1)
            {
                transform.localPosition += playerDirection * currentMovementSpeed * Time.deltaTime;
            }
            else
            {
                transform.localPosition += Vector3.left * currentMovementSpeed * Time.deltaTime;
                playerDirection = (transform.position - player.position).normalized;
            }

            if (turnBack)
            {
                dir = -1;
                currentMovementSpeed = Mathf.Lerp(movementSpeed, dir * 2 * movementSpeed, turnBackT);
                turnBackT += Time.deltaTime * 2f;
            }
            else
            {
                currentMovementSpeed = movementSpeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyActivator")
        {
            movementEnabled = true;
        }
        if (collision.tag == "EnemyTurnActivator")
        {
            turnBack = true;
        }
    }
}

enum MovementType
{
    Sin,
    Circle,
    GoPastAndTurnBack,
    GoPastAndTowardPlayer
}
