using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    public float jumpPower;

    private Rigidbody rb;
    public LeftControllerAnim leftCtrl;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        rb.velocity = transform.up * jumpPower;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (leftCtrl is not null)
        {
            leftCtrl.IsJump = false;
        }
    }
}
