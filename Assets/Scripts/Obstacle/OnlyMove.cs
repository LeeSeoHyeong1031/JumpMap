using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyMove : MonoBehaviour
{
    public Vector2 minMaxRange;
    public float moveSpeed;

    private Vector3 minPos;
    private Vector3 maxPos;
    private bool movingToMax = true;

    [Header("Move Direction")]
    public bool x;
    public bool y;
    public bool z;

    private void Start()
    {
        if (x)
        {
            minPos = new Vector3(minMaxRange.x, transform.position.y, transform.position.z);
            maxPos = new Vector3(minMaxRange.y, transform.position.y, transform.position.z);
        }
        else if (y)
        {
            minPos = new Vector3(transform.position.x, minMaxRange.x, transform.position.z);
            maxPos = new Vector3(transform.position.x, minMaxRange.y, transform.position.z);
        }
        else if (z)
        {
            minPos = new Vector3(transform.position.x, transform.position.y, minMaxRange.x);
            maxPos = new Vector3(transform.position.x, transform.position.y, minMaxRange.y);
        }

        transform.position = minPos;
    }

    private void Update()
    {
        Vector3 targetPos = movingToMax ? maxPos : minPos;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            movingToMax = !movingToMax;
        }
    }
}
