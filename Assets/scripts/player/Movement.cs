using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

	private float moveSpeed = 7;    //PREDKOSC
	private float maxSpeed = 7.5f;  //MAKS bo przez Force predkosc sie zwieksza
	private float drag = 5;			//ograniczenie slizgania sie przez force
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
}
