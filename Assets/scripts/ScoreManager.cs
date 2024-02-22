using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject arrow;
    public GameObject clock;

    [Header("Stars")]
    public float starScore1;
    public float starScore2;
    public float starScore3;
    private float currentScore = 0;

    [Header("Time")]
    public float levelLengthInMinutes;
    public float levelLengthInSecond;
    private float levelLength = 0;

    [Header("Refs")]
    private Tasks tasks;
    public TextMeshProUGUI ScoreText;

    public GameObject Star1;
	public GameObject Star2;
	public GameObject Star3;

	private void Awake()
    {
        levelLength = levelLengthInSecond + levelLengthInMinutes * 60;
        tasks = GetComponent<Tasks>();
    }

	private void Start()
	{
        Star1.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
		Star2.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
		Star3.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);

		LeanTween.rotateAround(arrow, new Vector3(0,0,-1), 360, levelLength);
        StartCoroutine(RunningTime());
	}

    private IEnumerator RunningTime()
    {
        yield return new WaitForSeconds(levelLength);
			while (true && this.gameObject != null)
		    {
			    float tick = 0.025f;
			    LeanTween.rotateZ(clock, 4f, tick);
                yield return new WaitForSeconds(tick);
			    LeanTween.rotateZ(clock, 0f, tick);
			    yield return new WaitForSeconds(tick);
			    LeanTween.rotateZ(clock, -4f, tick);
			    yield return new WaitForSeconds(tick);
			    LeanTween.rotateZ(clock, 0, tick);
			    yield return new WaitForSeconds(tick);
		    }
    }

	private void Update()
    {


        if(Time.time > levelLength && levelLength > 0)
        {
            tasks.levelEnd = true;
            tasks.EndLevel();
        }

        ScoreText.text = currentScore.ToString();
        CheckStars();
    }

    public void AddScore(float scoreIn)
    {
        currentScore += scoreIn;

        if (currentScore < 0) currentScore = 0;

        Debug.Log(currentScore);
    }

    private void CheckStars()
    {
        if (currentScore >= starScore1) Star1.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        if (currentScore >= starScore2) Star2.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        if (currentScore >= starScore3) Star3.GetComponent<Image>().color = new Color(255, 255, 255, 1);
    }
}
