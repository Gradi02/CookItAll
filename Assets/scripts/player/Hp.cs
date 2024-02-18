using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Hp : MonoBehaviour
{
    public float health_val = 10;
    public UnityEngine.UI.Slider health;

    void Start()
    {
        health.value = health_val;
        StartCoroutine(Regen());
    }

	private void Update()
	{
        health.value = health_val;
        if(health_val>10) health_val = 10;
	}


    private IEnumerator Regen()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if  (health_val<10)
            {
                health_val += 0.005f;
            }
        }
    }
}
