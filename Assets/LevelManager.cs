using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	public int level;

	public GameObject star1;
	public GameObject star2;
	public GameObject star3;


	private void Start()
	{
		if (PlayerPrefs.HasKey("Level1Stars"))
		{
			int stars = PlayerPrefs.GetInt("Level1Stars");
			if (stars == 1) star1.GetComponent<Image>().color = new Color(255,255,255,1);
			if (stars == 2) star2.GetComponent<Image>().color = new Color(255, 255, 255, 1);
			if (stars == 3) star3.GetComponent<Image>().color = new Color(255, 255, 255, 1);
		}

		else
		{
			star1.GetComponent<Image>().color = new Color(128, 128, 128, 0.3f);
			star2.GetComponent<Image>().color = new Color(128, 128, 128, 0.3f);
			star3.GetComponent<Image>().color = new Color(128, 128, 128, 0.3f);
		}
	}
}
