using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseToothScript : MonoBehaviour
{

    [SerializeField] private float boundRight = 10f;
    [SerializeField] private float boundLeft = -10f;
    [SerializeField] private float boundUp = 10f;
    [SerializeField] private float boundDown = -10f;

    private float toothForce = 100f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * toothForce);
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
}
