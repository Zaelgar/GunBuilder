using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    bool spinActive = true;
    public float spinSpeed = 35.0f;

    void Update()
    {
        if(spinActive)
        {
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        }
    }

    public void SetSpin(bool isActive)
    {
        spinActive = isActive;
    }
}