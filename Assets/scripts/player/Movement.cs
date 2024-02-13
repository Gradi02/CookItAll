using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

	private float moveSpeed = 7f;		 //PREDKOSC
	private float maxSpeed = 7.5f;		 //MAKS bo przez Force predkosc sie zwieksza
	private float drag = 5f;			 //ograniczenie slizgania sie przez force
	private float jumpforce = 5f;		 //moc skoku
	public Transform orientation;
	Vector3 moveDIrection;

	float HorizontalInput;
	float VerticalInput;
	Rigidbody rb;

	private void Start()
	{
		rb =GetComponent<Rigidbody>();
		rb.freezeRotation= true;
	}
	private void Update()
    {
		KeyInput();

		if (Input.GetKey(KeyCode.Space))
		{
			Jump();
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
		//ruszamy sie, nadajemy moc!
		moveDIrection = orientation.forward * VerticalInput + orientation.right * HorizontalInput;
		if (moveSpeed < maxSpeed)
		{
			rb.AddForce(moveDIrection.normalized * moveSpeed * 10f, ForceMode.Force);
		}
	}

	private void Jump()
	{
		// SprawdŸ, czy postaæ znajduje siê na ziemi, aby unikn¹æ podwójnego skoku
		if (IsGrounded())
		{
			rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
		}
	}

	private bool IsGrounded()
	{
		// SprawdŸ, czy postaæ jest na ziemi (na podstawie niewielkiego przesuniêcia od dolnej krawêdzi collidera)
		float distanceToGround = GetComponent<Collider>().bounds.extents.y;
		return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
	}
}
