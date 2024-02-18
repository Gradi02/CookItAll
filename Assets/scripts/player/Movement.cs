using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

	private float moveSpeed = 5f;		 //PREDKOSC
	//private float maxSpeed = 7.5f;		 //MAKS bo przez Force predkosc sie zwieksza
	private float drag = 5f;			 //ograniczenie slizgania sie przez force
	private float jumpforce = 5f;		 //moc skoku
	private bool jumped = false;		
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

		if (Input.GetKey(KeyCode.Space) && jumped == false)
		{
			jumped = true;
			StartCoroutine(Jump());

		}

		if (jumped)
		{
			Debug.Log("SKOK");
			moveSpeed = 3;
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
		//ruszamy sie, nadajemy moc!
		moveDIrection = orientation.forward * VerticalInput + orientation.right * HorizontalInput;
		rb.AddForce(moveDIrection.normalized * moveSpeed * 10f, ForceMode.Force);
	}
	/*
	private void Jump()
	{
		// SprawdŸ, czy postaæ znajduje siê na ziemi, aby unikn¹æ podwójnego skoku
		if (IsGrounded())
		{
			rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
		}
	}
	*/

	private IEnumerator Jump()
	{
		jumped = true;
		rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
		yield return new WaitForSeconds(0.3f);
		rb.AddForce(-Vector3.up * jumpforce/7, ForceMode.Impulse);
		yield return new WaitForSeconds(0.05f);
		rb.AddForce(-Vector3.up * jumpforce/5, ForceMode.Impulse);
		yield return new WaitForSeconds(0.05f);
		rb.AddForce(-Vector3.up * jumpforce / 3, ForceMode.Impulse);
		yield return new WaitForSeconds(0.2f);
		jumped = false;
	}

	private bool IsGrounded()
	{
		// SprawdŸ, czy postaæ jest na ziemi (na podstawie niewielkiego przesuniêcia od dolnej krawêdzi collidera)
		float distanceToGround = GetComponent<Collider>().bounds.extents.y;
		return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
	}
}
