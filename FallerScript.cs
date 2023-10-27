using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallerScript : MonoBehaviour
{
    public float speed = 1f;

    public bool leftSide = true;

    [SerializeField] private float boundRight = 10f;
    [SerializeField] private float boundLeft = -10f;

    [SerializeField] private float yAxis = -1.93f;

    // Start is called before the first frame update
    void Start()
    {
        if (leftSide)
        {
            Flip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        OutOfBounds();
    }

    private void Move()
    {
        if (transform.position.y > yAxis)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }

    private void OutOfBounds()
    {
        if (transform.position.x > boundRight || transform.position.x < boundLeft)
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
