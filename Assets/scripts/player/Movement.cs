using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{

	public float moveSpeed = 5f;
	public float jumpForce = 4f;
	private bool left = false;
	private Rigidbody rb;

	private bool Grounded = true;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		Vector3 camForward = Camera.main.transform.forward;
		Vector3 camRight = Camera.main.transform.right;

		camForward.y = 0;
		camRight.y = 0;

		camForward.Normalize();
		camRight.Normalize();

		Vector3 movement = (camForward * verticalInput + camRight * horizontalInput) * moveSpeed * Time.deltaTime;

		rb.MovePosition(transform.position + movement);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && Grounded)
		{
			Grounded = false;
			rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
			StartCoroutine(Cooldown());
		}
	}
	private IEnumerator Cooldown()
	{
		yield return new WaitForSeconds(0.7f);
		Grounded = true;
	}
}
