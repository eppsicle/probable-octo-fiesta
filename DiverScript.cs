using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverScript : MonoBehaviour
{
    public float horizontalSpeed = 4.5f;
    public float verticalSpeed = 4.5f;

    public bool leftSide = true;

    [SerializeField] private float boundRight = 10f;
    [SerializeField] private float boundLeft = -10f;
    [SerializeField] private float boundUp = 10f;
    [SerializeField] private float boundDown = -10f;



    

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
        if (leftSide)
        {
            transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);
            transform.Translate(Vector3.down * verticalSpeed * Time.deltaTime);
        }
        else if (!leftSide)
        {
            transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime);
            transform.Translate(Vector3.down * verticalSpeed * Time.deltaTime);
        }
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
