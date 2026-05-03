using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    [SerializeField]
    Material bulbMaterial;
    
    [SerializeField]
    Transform bulb;
    Transform lightSource;
    bool lightOn;
    Color emissionColor;

    private void Start()
    {
        lightOn = true;
        lightSource = transform.GetComponentInChildren<Light>().transform;
        bulbMaterial = bulb.GetComponent<Renderer>().material;
        emissionColor = bulbMaterial.GetColor("_EmissionColor");
    }
    public void ToggleLight()
    {
        


        if (lightOn == true)
        {
            StartCoroutine(FadeLight(5.5f,0f,1f,0f));
            
            lightOn = false;
        }
        else {
            StartCoroutine(FadeLight(0f,5.5f,0f,1f));
            //bulbMaterial.SetColor("_EmissionColor", emissionColor * 10f);
            lightOn = true;
        }
        
    }

    IEnumerator FadeLight(float currentIntensity,float targetIntensity,float currentEmission, float targetEmission)
    {
        float duration = 1f;
        float time = 0f;
        float emission;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            lightSource.GetComponent<Light>().intensity = Mathf.Lerp(currentIntensity, targetIntensity, t);
            emission = Mathf.Lerp(currentEmission, targetEmission, t);
            bulbMaterial.SetColor("_EmissionColor", emissionColor * emission);
            yield return null;
        }
        
    }

}
