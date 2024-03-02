using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
	public int level;

	public GameObject star1;
	public GameObject star2;
	public GameObject star3;

	public TextMeshProUGUI hs;

	private void Start()
	{
		if (!PlayerPrefs.HasKey("HS_1"))
		{
			PlayerPrefs.SetFloat("HS_1", 0);
		}
		//LEVEL 1
		star1.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
		star2.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
		star3.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
			float HS = PlayerPrefs.GetFloat("HS_1");
			if (HS >= 150) star1.GetComponent<Image>().color = new Color(1,1,1,1);
			if (HS >= 300) star2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
			if (HS >= 450) star3.GetComponent<Image>().color = new Color(1, 1, 1, 1);

		hs.text = "HIGHSCORE: " + PlayerPrefs.GetFloat("HS_1");
	}
}
