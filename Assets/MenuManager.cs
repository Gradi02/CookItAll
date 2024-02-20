using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public GameObject[] StartText;
	public GameObject[] Level1Text;
	private Vector3[] StartTextPos;
	private Vector3[] Level1TextPos;
	private void Start()
	{
		StartTextPos = new Vector3[StartText.Length];
		Level1TextPos = new Vector3[Level1Text.Length];
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
						StartCoroutine(Click(StartText, StartTextPos));
						LeanTween.move(Camera.main.gameObject, new Vector3(25f,Camera.main.gameObject.transform.position.y,2f), 2f).setEase(LeanTweenType.easeInOutBack);
						LeanTween.rotateY(Camera.main.gameObject, 90f, 2f).setEase(LeanTweenType.easeInOutBack);
					}

					if (hit.collider.CompareTag("level"))
					{
						int which_lvl = hit.collider.gameObject.GetComponent<LevelManager>().level;
						StartCoroutine(Click(Level1Text, Level1TextPos));
						StartCoroutine(SceneLoading(which_lvl));
					}
				}
			}
		}
	}

	private IEnumerator SceneLoading(int lvl)
	{
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(lvl, LoadSceneMode.Single);
	}

	private IEnumerator Click(GameObject[] TextToClick, Vector3[] Position)
	{
		for (int i = 0; i < TextToClick.Length; i++)
		{
			Position[i] = TextToClick[i].transform.position;
		}

		for (int i = 0; i<TextToClick.Length; i++)
		{
			int randomPower = Random.Range(3, 5);
			Vector3 randomDirection = Random.insideUnitSphere.normalized;
			float randomAngle = Random.Range(180f, 360f);
			randomDirection = Quaternion.AngleAxis(randomAngle, Random.onUnitSphere) * randomDirection;

			TextToClick[i].GetComponent<Rigidbody>().AddForce(randomDirection * randomPower, ForceMode.Impulse);
		}

		yield return new WaitForSeconds(2.2f);
		for (int i = 0; i<TextToClick.Length; i++)
		{
			TextToClick[i].transform.position = Position[i];
		}
	}
}
