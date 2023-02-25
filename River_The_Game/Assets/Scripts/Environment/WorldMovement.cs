using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMovement : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed = 5;

    private bool isMoving = false;

    private void Start() {
        StartMoving();
    }

    public void StartMoving() {
        isMoving = true;
    }

    void Update() {
        if (!isMoving) {
            return;
        }
        Vector3 position = target.position;
        position.x -= speed * Time.deltaTime;
        target.position = position;
    }
}
