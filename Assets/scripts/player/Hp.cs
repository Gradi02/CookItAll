using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Hp : MonoBehaviour
{
    public float health_val = 10;
    public Slider health;
    public Volume vol;
    private Vignette vn;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        health.maxValue = health_val;
        health.value = health_val;
        StartCoroutine(Regen());
        
        Vignette temp;
        if(vol.profile.TryGet<Vignette>(out temp))
        {
            vn = temp;
        }
    }

	private void Update()
	{
        health.value = health_val;
        if(health_val > health.maxValue) health_val = health.maxValue;
        if(health_val < 0) health_val = 0;

        if (vn != null)
        {
            vn.intensity.value = 0.4f - (health_val / health.maxValue / 2.5f);
        }

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

    public void DamagePlayer(float val)
    {
        health_val -= val;
        StartCoroutine(camShake());
    }

    private IEnumerator camShake()
    {
        LeanTween.rotateZ(cam.gameObject, 10, 0.1f);
        yield return new WaitForSeconds(0.1f);
        LeanTween.rotateZ(cam.gameObject, 0, 1f).setEase(LeanTweenType.easeOutCubic);
        yield return new WaitForSeconds(1f);
    }
}
