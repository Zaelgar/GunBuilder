using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    public float pulseDuration = 0.2f;
    public float scaleMin = 0.08f;
    public float scaleMax = 0.15f;
    float nextScale = 0f;

    private void Start()
    {
        StartCoroutine(PulseVisual());
        nextScale = scaleMax;
    }

    IEnumerator PulseVisual()
    {
        LeanTween.scale(gameObject, new Vector3(nextScale, nextScale, nextScale), pulseDuration).setEase(LeanTweenType.easeInBounce);
        yield return new WaitForSeconds(pulseDuration);

        if (nextScale == scaleMax)
            nextScale = scaleMin;
        else
            nextScale = scaleMax;

        StartCoroutine(PulseVisual());
    }
}