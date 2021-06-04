using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMove : MonoBehaviour
{
    [SerializeField]
    Transform[] positions;

    public float moveTime = 3.0f;
    public float delayTime = 1.0f;
    public LeanTweenType easingType = LeanTweenType.notUsed;
    int index = 0;
    Transform nextPoint = null;

    private void Start()
    {
        if(positions.Length < 2)
        {
            Debug.LogError("Not enough positions defined for patrolling target!!");
        }
        else
        {
            nextPoint = positions[index];
        }

        StartCoroutine(MoveTarget());
    }

    IEnumerator MoveTarget()
    {
        LeanTween.move(gameObject, nextPoint, moveTime).setEase(easingType);
        yield return new WaitForSeconds(moveTime + delayTime);

        index++;
        index %= positions.Length;
        nextPoint = positions[index];

        StartCoroutine(MoveTarget());
    }
}