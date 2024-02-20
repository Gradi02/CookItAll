using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MenuManager : MonoBehaviour
{
	public GameObject[] StartText;
	private Vector3[] TextPos;

	private void Start()
	{
		TextPos = new Vector3[11];
	}
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider != null)
				{
					if (hit.collider.CompareTag("start"))
					{
						StartCoroutine(Click(StartText));
						LeanTween.move(Camera.main.gameObject, new Vector3(25f,Camera.main.gameObject.transform.position.y,2f), 2f).setEase(LeanTweenType.easeInSine);
						LeanTween.rotateY(Camera.main.gameObject, 90f, 2f).setEase(LeanTweenType.easeInSine);
					} 
				}
			}
		}
	}


	private IEnumerator Click(GameObject[] TextToClick)
	{
		for (int i = 0; i < TextToClick.Length; i++)
		{
			TextPos[i] = TextToClick[i].transform.position;
		}

		for (int i = 0; i<TextToClick.Length; i++)
		{
			int randomPower = Random.Range(5, 7);
			Vector3 randomDirection = Random.insideUnitSphere.normalized;
			float randomAngle = Random.Range(180f, 360f);
			randomDirection = Quaternion.AngleAxis(randomAngle, Random.onUnitSphere) * randomDirection;

			TextToClick[i].GetComponent<Rigidbody>().AddForce(randomDirection * randomPower, ForceMode.Impulse);
		}

		yield return new WaitForSeconds(2.2f);
		for (int i = 0; i<TextToClick.Length; i++)
		{
			TextToClick[i].transform.position = TextPos[i];
		}
	}
}
