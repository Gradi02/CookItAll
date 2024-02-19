using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeManager : MonoBehaviour
{
	private float maxDistance = 1f;
	private bool hitted = false;
	public void Update()
	{
		if (!hitted)
		{
			//Debug.Log(transform.name + " col: " + GetComponent<BoxCollider>().enabled + " vel: " + GetComponent<Rigidbody>().velocity);
			Ray ray = new Ray(gameObject.transform.position, -gameObject.transform.forward);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, maxDistance))
			{
				IknifeInteraction interaction = hit.collider.GetComponent<IknifeInteraction>();
				if (interaction == null && !hitted && !hit.transform.CompareTag("item") && !hit.transform.CompareTag("Player") && !hit.transform.CompareTag("knife"))
				{
					hitted = true;
					Debug.Log(hit.point + " co: " + hit.transform.gameObject);
					GetComponent<BoxCollider>().enabled = false;
					GetComponent<Rigidbody>().useGravity = false;
					GetComponent<Rigidbody>().velocity = Vector3.zero;
					GetComponent<Rigidbody>().freezeRotation = true;
					transform.position = hit.point + transform.forward * 0.2f;
					Destroy(gameObject, 10f);
				}
			}
		}
		else
        {
			GetComponent<BoxCollider>().enabled = false;
			GetComponent<Rigidbody>().useGravity = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}

	public void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
			IknifeInteraction interaction = other.GetComponent<IknifeInteraction>();

			if (interaction != null)
			{
				interaction.knifeInteract();
				Destroy(gameObject);
			}
		}
    }
}
