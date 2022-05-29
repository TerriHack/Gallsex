using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class triggerlight : MonoBehaviour
{
    public Light2D projecteur;
    private float graphValue,currentTime;
    private bool entered = false;
    public AnimationCurve curveIntensity;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            entered = true;
            currentTime = 0;

        }
        
    }
    void Update()
    {
        if (entered)
        {
            currentTime = currentTime + Time.deltaTime;
            graphValue = curveIntensity.Evaluate(currentTime);
            projecteur.intensity = graphValue;
            if (currentTime == 1)
            {
                entered = false;
            }
        }
    }
}
