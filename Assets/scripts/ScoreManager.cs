using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("Result")]
    public GameObject OrderWindow;
    public GameObject LevelResult;
    public TextMeshProUGUI FinishedTasks;
	public TextMeshProUGUI BadTasks;
	public TextMeshProUGUI KilledMonsters;
	public TextMeshProUGUI TotalScore;
	public TextMeshProUGUI OneStarTime;
	public TextMeshProUGUI TwoStarTime;
	public TextMeshProUGUI ThreeStarTime;


	[Header("Clock")]
	public GameObject arrow;
    public GameObject clock;
    public GameObject Star1;
	public GameObject Star2;
	public GameObject Star3;


    [Header("Stars")]
    public float starScore1;
    public float starScore2;
    public float starScore3;

    private bool checkStar1 = true;
	private bool checkStar2 = true;
	private bool checkStar3 = true;

	private int StarScore1Time = 0;
	private int StarScore2Time = 0;
	private int StarScore3Time = 0;

	private float currentScore = 0;


    [Header("Time")]
    public float levelLengthInMinutes;
    public float levelLengthInSecond;
    private float levelLength = 0;


    [Header("Refs")]
    private Tasks tasks;
    public TextMeshProUGUI ScoreText;




    private int Seconds = 0;

	private void Awake()
    {
        levelLength = Time.time + levelLengthInSecond + levelLengthInMinutes * 60;
        tasks = GetComponent<Tasks>();
    }

	private void Start()
	{
		LevelResult.SetActive(false);
		Star1.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
		Star2.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
		Star3.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);

		LeanTween.rotateAround(arrow, new Vector3(0,0,-1), 360, levelLength);
        StartCoroutine(RunningTime());
		StartCoroutine(Timer());
	}

    private IEnumerator Timer()
    {
        while(true){
            yield return new WaitForSeconds(1f);
            Seconds++;
        } 
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


            LevelResult.SetActive(true);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;

            FinishedTasks.text = OrderWindow.GetComponent<FinishTask>().FinishedTasks.ToString();
            BadTasks.text = OrderWindow.GetComponent<FinishTask>().BadTasks.ToString();
            TotalScore.text = currentScore.ToString();  

            if (StarScore1Time > 0)
            {
                OneStarTime.text = StarScore1Time.ToString();
            }
            else
            {
                OneStarTime.text = "Unreached";
			}


            if (StarScore2Time > 0)
            {
                TwoStarTime.text = StarScore2Time.ToString();
            }
			else
			{
				TwoStarTime.text = "Unreached";
			}


			if (StarScore3Time > 0)
            {
                ThreeStarTime.text = StarScore3Time.ToString();
			}
			else
			{
				ThreeStarTime.text = "Unreached";
			}


            Summary();
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
        if (currentScore >= starScore1 && checkStar1) 
        {
            checkStar1= false;
            Star1.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            StarScore1Time = Seconds;
        }

        if (currentScore >= starScore2 && checkStar2) 
        { 
            checkStar2= false;
            Star2.GetComponent<Image>().color = new Color(255, 255, 255, 1);
			StarScore2Time = Seconds;
		}

        if (currentScore >= starScore3 && checkStar3)
        {
            checkStar3= false;
            Star3.GetComponent<Image>().color = new Color(255, 255, 255, 1);
			StarScore3Time = Seconds;
		}

    }


    private void Summary()
    {
        if(currentScore > PlayerPrefs.GetFloat("HS_1"))
        {
            PlayerPrefs.SetFloat("HS_1", currentScore);
        }
    }
}
