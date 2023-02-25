using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowEnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float followPhaseXPos;
    [SerializeField]
    private float yMovementSpeed;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        bool isInTargetXPos = transform.position.x < followPhaseXPos;
        bool isNotCloseToPlayerY = transform.position.y > player.position.y + 0.1f || transform.position.y < player.position.y - 0.1f;
        if (isInTargetXPos)
        {
            float yPos = transform.position.y;
            if (isNotCloseToPlayerY)
            {
                float yDir = transform.position.y < player.transform.position.y ? 1 : -1;
                yPos = transform.position.y + yMovementSpeed * yDir * Time.deltaTime;
            }
            transform.position = new Vector3(followPhaseXPos, yPos, transform.position.z);
        }
    }
}
