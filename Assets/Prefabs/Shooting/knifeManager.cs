using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeManager : MonoBehaviour
{
	private float maxDistance = 1f;
	public void Update()
	{
		Ray ray = new Ray(gameObject.transform.position, -gameObject.transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, maxDistance))
		{
			Debug.Log(hit.point);
			Freeze(hit.collider, hit.point);
		}

	}

	public void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
			Freeze(other, Vector3.zero);
		}
    }

	private void Freeze(Collider other, Vector3 pos)
	{
		IknifeInteraction interaction = other.GetComponent<IknifeInteraction>();

		if (interaction != null)
		{
			interaction.knifeInteract();
			Destroy(gameObject);
		}
		gameObject.GetComponent<BoxCollider>().enabled = false;
		gameObject.GetComponent<Rigidbody>().useGravity = false;
		gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		gameObject.GetComponent<Rigidbody>().freezeRotation = true;
		gameObject.transform.position = pos + transform.forward * 0.2f;
		Destroy(gameObject, 10f);
	}

}
