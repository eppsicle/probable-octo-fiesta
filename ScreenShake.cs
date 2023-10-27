using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public bool start = false;
    public AnimationCurve curve;
    public float shakeTime = 1f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {

            start = false;
            StartCoroutine(ShakeAnimation());

            //Begin the shake coroutine

        }
    }

    IEnumerator ShakeAnimation()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeTime)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / shakeTime);
            Vector2 randOffset = Random.insideUnitCircle;
            transform.position = startPosition + new Vector3(randOffset.x, randOffset.y, 0f) * strength;
            yield return null;
        }

        transform.position = startPosition;
    }
}
