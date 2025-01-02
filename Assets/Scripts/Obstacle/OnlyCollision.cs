using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyCollision : MonoBehaviour
{
    public float magnitude;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody rb = collision.collider.attachedRigidbody;
            Vector3 forceDir = rb.position - transform.position;
            forceDir.Normalize();
            forceDir.y = 0.5f;
            rb.AddForce(forceDir * magnitude, ForceMode.Impulse);
        }
    }
}
