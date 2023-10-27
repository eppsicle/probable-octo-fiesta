using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallerBodyScript : MonoBehaviour
{
    [SerializeField] private float boundRight = 10f;
    [SerializeField] private float boundLeft = -10f;
    [SerializeField] private float boundUp = 10f;
    [SerializeField] private float boundDown = -10f;

    public bool leftSide = false;


    // Start is called before the first frame update
    void Start()
    {
        if (leftSide)
        {
            Flip();
        }
    }
    void Update()
    {
        OutOfBounds();

    }

    private void OutOfBounds()
    {
        if (transform.position.x > boundRight || transform.position.x < boundLeft || transform.position.y > boundUp || transform.position.x < boundDown)
        {
            Destroy(gameObject);
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }
}
