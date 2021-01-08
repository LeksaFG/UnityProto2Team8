using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
   
   bool isFlickering = false;
   float timeDelay;
    public float FlickerRangeMin = 0.01f;
    public float FlickerRangeMax = 0.2f;
    public GameObject GlowingPart1;
    public GameObject GlowingPart2;


    // Update is called once per frame
    void Update()
    {


        if (isFlickering == false) {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight() {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        GlowingPart1.GetComponent<MeshRenderer>().enabled = false;
        GlowingPart2.GetComponent<MeshRenderer>().enabled = false;
        timeDelay = Random.Range(FlickerRangeMin, FlickerRangeMax);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        GlowingPart1.GetComponent<MeshRenderer>().enabled = true;
        GlowingPart2.GetComponent<MeshRenderer>().enabled = true;
        timeDelay = Random.Range(FlickerRangeMin, FlickerRangeMax);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
