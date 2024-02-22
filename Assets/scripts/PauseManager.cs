using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
	bool pause = false;
	public GameObject PauseObject;

	private void Start()
	{
		PauseObject.SetActive(false);
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q) && pause == false){
			Debug.Log("pauza");
			PauseOn();
		}

		else if (Input.GetKeyDown(KeyCode.Q) && pause == true)
		{
			Debug.Log("giereczka");
			PauseOff();
		}
	}


	public void PauseOn()
	{
		Time.timeScale = 0;
		PauseObject.SetActive(true);
		pause = true;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void PauseOff()	{
		Time.timeScale = 1;
		PauseObject.SetActive(false);
		pause = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void MainMenu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}
}
