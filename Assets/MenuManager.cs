using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

	private float TransSpeed = 1f;
	private Vignette vn;
	private DepthOfField df;
	public Volume vol;
	public ParticleSystem MenuParticle;

	public GameObject[] StartText;
	public GameObject[] Options;
	public GameObject Options_menu;
	public GameObject[] Authors;
	public GameObject Authors_menu;
	public GameObject[] Exit;

	public GameObject[] Level1Text;


	private Vector3[] StartTextPos;
	private Vector3[] OptionsTextPos;
	private Vector3[] AuthorsTextPos;
	private Vector3[] ExitTextPos;

	private Vector3[] Level1TextPos;

	private bool clicked = false;

	private void Start()
	{
		StartTextPos = new Vector3[StartText.Length];
		OptionsTextPos = new Vector3[Options.Length];
		AuthorsTextPos = new Vector3[Authors.Length];
		ExitTextPos = new Vector3[Exit.Length];

		Level1TextPos = new Vector3[Level1Text.Length];

		SavePos(StartText, StartTextPos);
		SavePos(Options, OptionsTextPos);
		SavePos(Authors, AuthorsTextPos);
		SavePos(Exit, ExitTextPos);
		SavePos(Level1Text, Level1TextPos);


		Vignette temp;
		if (vol.profile.TryGet<Vignette>(out temp))
		{
			vn = temp;
		}		
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
					if (hit.collider.CompareTag("start") && clicked == false)
					{
						StartCoroutine(Click(StartText, StartTextPos));
						LeanTween.move(Camera.main.gameObject, new Vector3(25f,Camera.main.gameObject.transform.position.y), TransSpeed).setEase(LeanTweenType.easeInOutBack);
						LeanTween.rotateY(Camera.main.gameObject, 90f, TransSpeed).setEase(LeanTweenType.easeInOutBack);
						clicked = false;
					}

					if (hit.collider.CompareTag("level") && clicked == false)
					{
						StartCoroutine(Click(Level1Text, Level1TextPos));
						int which_lvl = hit.collider.gameObject.GetComponent<LevelManager>().level;
						StartCoroutine(SceneLoading(which_lvl));
					}


					if (hit.collider.CompareTag("options") && clicked == false)
					{
						StartCoroutine(Click(Options, OptionsTextPos));
						LeanTween.move(Options_menu, new Vector3(0, 2, 3), TransSpeed).setEase(LeanTweenType.easeOutBack);
						LeanTween.rotateY(Options_menu, 340f, TransSpeed).setEase(LeanTweenType.easeOutBack);
					}

					if (hit.collider.CompareTag("authors") && clicked == false)
					{
						StartCoroutine(Click(Authors, AuthorsTextPos));
						LeanTween.move(Authors_menu, new Vector3(0, 2, 3), TransSpeed).setEase(LeanTweenType.easeOutBack);
						LeanTween.rotateY(Authors_menu, 340f, TransSpeed).setEase(LeanTweenType.easeOutBack);
					}

					if (hit.collider.CompareTag("exit") && clicked == false)
					{
						Application.Quit();
					}
				}
			}
		}
	}
	public void FOptionsBack()
	{
		LeanTween.moveLocal(Options_menu, new Vector3(-8, 2, 1), TransSpeed).setEase(LeanTweenType.easeOutBack);
		LeanTween.rotateY(Options_menu, 90f, 2f).setEase(LeanTweenType.easeOutBack);
		clicked = false;
	}

	public void FAuthorsBack()
	{
		LeanTween.moveLocal(Authors_menu, new Vector3(-4, 2, 6), TransSpeed).setEase(LeanTweenType.easeOutBack);
		LeanTween.rotateY(Authors_menu, 90f, 2f).setEase(LeanTweenType.easeOutBack);
		clicked = false;
	}


	private void SavePos(GameObject[] TextToClick, Vector3[] Position)
	{
		for (int i = 0; i < TextToClick.Length; i++)
		{
			Position[i] = TextToClick[i].transform.position;
		}
	}

	private IEnumerator SceneLoading(int lvl)
	{
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(lvl, LoadSceneMode.Single);
	}

	private IEnumerator Click(GameObject[] TextToClick, Vector3[] Position)
	{
		clicked = true;
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
			TextToClick[i].transform.SetPositionAndRotation(Position[i], Quaternion.Euler(90, 0, 0));
			float x1 = TextToClick[i].gameObject.transform.position.x;
			float y1 = TextToClick[i].gameObject.transform.position.y;
			float z1 = TextToClick[i].gameObject.transform.position.z;
			ParticleSystem part1 = Instantiate(MenuParticle, new Vector3(x1,y1,z1), Quaternion.identity, TextToClick[i].gameObject.transform);

			float x2 = Position[i].x;
			float y2 = Position[i].y;
			float z2 = Position[i].z;
			ParticleSystem part2 =  Instantiate(MenuParticle, new Vector3(x2,y2,z2), Quaternion.identity);

			Destroy(part1.gameObject, 3f);
			Destroy(part2.gameObject, 3f);
		}
	}
}
