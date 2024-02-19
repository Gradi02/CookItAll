using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

	private float moveSpeed = 5f;		
	//private float maxSpeed = 7.5f;	
	private float drag = 5f;			 
	private float jumpforce = 0.5f;		 	
	public Transform orientation;
	Vector3 moveDIrection;

	float HorizontalInput;
	float VerticalInput;
	Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
	}
	private void Update()
    {
		KeyInput();

		if (Input.GetKey(KeyCode.Space) && IsGrounded())
		{
			Jump();
		}

		if (!IsGrounded())
		{
			moveSpeed = 3;
			rb.AddForce(Vector3.down * -9.81f * Time.deltaTime);
		}
		else
		{
			moveSpeed = 5;
		}
	}
	
	private void KeyInput()
	{
		//sprawdzamy co klikamy itd.
		HorizontalInput = Input.GetAxisRaw("Horizontal");
		VerticalInput = Input.GetAxisRaw("Vertical");
	}

	private void FixedUpdate()
	{
		//wykonujemy poruszanie i nadajemy DRAG aby zapobiec slizganiu sie postaci
		MovePlayer();
		rb.drag = drag;
	}

	private void MovePlayer()
	{
		moveDIrection = orientation.forward * VerticalInput + orientation.right * HorizontalInput;
		rb.AddForce(moveDIrection.normalized * moveSpeed * 10f, ForceMode.Force);
	}
	private void Jump()
	{
		if (IsGrounded())
		{
			rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
		}
	}
	private bool IsGrounded()
	{
		// Sprawd�, czy posta� jest na ziemi (na podstawie niewielkiego przesuni�cia od dolnej kraw�dzi collidera)
		float distanceToGround = GetComponent<Collider>().bounds.extents.y;
		return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
	}
}
