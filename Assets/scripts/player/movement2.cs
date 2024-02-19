using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement2 : MonoBehaviour
{
    private float Hmove, Vmove;
    private Rigidbody rb;
    private Vector3 move;
    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Hmove = Input.GetAxis("Horizontal");
        Vmove = Input.GetAxis("Vertical");

        move = new Vector3(Hmove, 0, Vmove);
    }

    private void FixedUpdate()
    {
        rb.velocity += move * Time.deltaTime * speed;
    }
}
