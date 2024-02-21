using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float SensX;
    public float SensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;
	}
	private void Update()
	{
		float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensX;
		float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensY;

		yRotation += mouseX;
		xRotation -= mouseY;

		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.rotation = Quaternion.Euler(xRotation, yRotation, transform.rotation.z);
		orientation.rotation = Quaternion.Euler(0, yRotation, 0);
	}
}
