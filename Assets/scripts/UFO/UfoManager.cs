using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoManager : MonoBehaviour
{
    public GameObject LevelManager;
    public GameObject UfoPrefab;
    public Vector3 position;
    void Start()
    {
        float min = LevelManager.GetComponent<ScoreManager>().levelLengthInMinutes;
		float sec = LevelManager.GetComponent<ScoreManager>().levelLengthInSecond;

        float totalTime = min * 60 + sec;

        int RandomTimeEvent = (int)Random.Range(0, totalTime - 10f);
        StartCoroutine(UfoMovement(RandomTimeEvent));
	}

    void FixedUpdate()
    {
        if(UfoPrefab != null)
        {
            UfoPrefab.transform.position += new Vector3(0.1f, 0, 0.1f);
        }
    }


    IEnumerator UfoMovement(int TimeToSpawn)
    {
        yield return new WaitForSeconds(TimeToSpawn);
        Instantiate(UfoPrefab, position, Quaternion.identity);
    }
}
