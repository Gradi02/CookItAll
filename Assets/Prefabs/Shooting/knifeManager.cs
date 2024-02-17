using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeManager : MonoBehaviour
{
	private float maxDistance = 1f;
	private bool hitted = false;
	public void Update()
	{
		Ray ray = new Ray(gameObject.transform.position, -gameObject.transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, maxDistance))
		{
			IknifeInteraction interaction = hit.collider.GetComponent<IknifeInteraction>();
			if (interaction == null && !hitted && !hit.transform.CompareTag("item"))
			{
				hitted = true;
				gameObject.GetComponent<BoxCollider>().enabled = false;
				gameObject.GetComponent<Rigidbody>().useGravity = false;
				gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
				gameObject.GetComponent<Rigidbody>().freezeRotation = true;
				gameObject.transform.position = hit.point + transform.forward * 0.2f;
				Destroy(gameObject, 10f);
			}
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
