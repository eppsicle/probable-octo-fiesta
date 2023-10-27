using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private float speed;
    [SerializeField] private float moshSpeed = 4f;
    [SerializeField] private float duckSpeed = 1f;
    public int hp = 6;

    private float boundLeft = -4f;
    private float boundRight = 5f;
    private bool goingRight = true;

    private bool moshing;

    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private GameObject cameraControl;

    [SerializeField] private GameObject spawner;

    public int numOfTeeth = 6;
 
    public Image[] teeth;
    public Sprite fullTooth;
    public Sprite emptyTooth;

    [SerializeField] private GameObject looseTeeth;

    public static int bloodNum = 6;
    public GameObject[] bloodGroup = new GameObject[bloodNum];
    [SerializeField] private GameObject blood;

    private bool flipped = false;

    public Animator animator;

    public GameObject mosherBody;
    private Vector3 mosherBodyLocation;

    public GameObject fallerBody;
    private Vector3 fallerBodyLocation;


    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        SpawnBlood();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        TakeInput();
        HealthManager();
        
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        Debug.Log("entered");

        switch (other.tag)
        {
            case ("Mosher"):
                if (moshing)
                {
                    other.GetComponent<MosherScript>().speed = -1f;
                    mosherBodyLocation = new Vector3 (other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
                    Quaternion bodyRotation = new Quaternion(0,0,0,0);
                    bool bodyLeftSide = other.gameObject.GetComponent<MosherScript>().leftSide;
                    Destroy(other.gameObject);
                    GameObject mosherDead = Instantiate(mosherBody, mosherBodyLocation, bodyRotation);
                    mosherDead.GetComponent<MosherBodyScript>().leftSide = bodyLeftSide;
                    spawner.GetComponent<SpawnerScript>().totalMoshers--;
                }
                else
                {
                    hp -= 1;
                    other.GetComponent<MosherScript>().speed = -1f;
                    mosherBodyLocation = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
                    Quaternion bodyRotation = new Quaternion(0, 0, 0, 0);
                    bool bodyLeftSide = other.gameObject.GetComponent<MosherScript>().leftSide;
                    Destroy(other.gameObject);
                    GameObject mosherDead = Instantiate(mosherBody, mosherBodyLocation, bodyRotation);
                    mosherDead.GetComponent<MosherBodyScript>().leftSide = bodyLeftSide;
                    goingRight = !goingRight;
                    cameraControl.GetComponent<ScreenShake>().start = true;
                    spawner.GetComponent<SpawnerScript>().totalMoshers--;

                    if (hp > 0)
                    {
                        GameObject looseTooth = Instantiate(looseTeeth, teeth[hp - 1].transform.position, teeth[hp - 1].transform.rotation);
                        bloodGroup[hp].GetComponent<ParticleSystem>().Play();
                    }
                    else if (hp == 0)
                    {
                        bloodGroup[hp].GetComponent<ParticleSystem>().Play();
                    }
                }
                break;

            case ("Diver"):
                hp -= 1;
                other.GetComponent<DiverScript>().verticalSpeed = -1f;
                Destroy(other.gameObject, 0.8f);
                goingRight = !goingRight;
                cameraControl.GetComponent<ScreenShake>().start = true;
                if (hp > 0)
                {
                    GameObject looseTooth = Instantiate(looseTeeth, teeth[hp - 1].transform.position, teeth[hp - 1].transform.rotation);
                    bloodGroup[hp].GetComponent<ParticleSystem>().Play();
                }
                else if (hp == 0)
                {
                    bloodGroup[hp].GetComponent<ParticleSystem>().Play();
                }
                break;
            case ("Faller"):
                if (moshing)
                {
                    hp -= 1;
                    cameraControl.GetComponent<ScreenShake>().start = true;
                    fallerBodyLocation = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
                    Quaternion bodyRotation = new Quaternion(0, 0, 0, 0);
                    bool bodyLeftSide = other.gameObject.GetComponent<FallerScript>().leftSide;
                    Destroy(other.gameObject);
                    GameObject fallerDead = Instantiate(fallerBody, fallerBodyLocation, bodyRotation);
                    fallerDead.GetComponent<FallerBodyScript>().leftSide = bodyLeftSide;
                    spawner.GetComponent<SpawnerScript>().spawnedFaller = false;
                    if (hp > 0)
                    {
                        GameObject looseTooth = Instantiate(looseTeeth, teeth[hp - 1].transform.position, teeth[hp - 1].transform.rotation);
                        bloodGroup[hp].GetComponent<ParticleSystem>().Play();
                    }
                    else if (hp == 0)
                    {
                        bloodGroup[hp].GetComponent<ParticleSystem>().Play();
                    }
                }
                else
                {
                    spawner.GetComponent<SpawnerScript>().spawnedFaller = false;
                    fallerBodyLocation = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
                    Quaternion bodyRotation = new Quaternion(0, 0, 0, 0);
                    bool bodyLeftSide = other.gameObject.GetComponent<FallerScript>().leftSide;
                    Destroy(other.gameObject);
                    GameObject fallerDead = Instantiate(fallerBody, fallerBodyLocation, bodyRotation);
                    fallerDead.GetComponent<FallerBodyScript>().leftSide = bodyLeftSide;
                    hp += 1;
                    bloodGroup[hp - 1].GetComponent<ParticleSystem>().Stop();
                }
                break;
        }

    }
    
    private void Move()
    {
        if (goingRight)
        {
            if (!flipped)
            {
                Vector3 currentScale = gameObject.transform.localScale;
                currentScale.x *= -1;
                gameObject.transform.localScale = currentScale;
                flipped = true;
            }
            

            if (transform.position.x < boundRight)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else
            {
                goingRight = false;
            }
        }
        else if (!goingRight)
        {
            
            if (flipped)
            {
                Vector3 currentScale = gameObject.transform.localScale;
                currentScale.x *= -1;
                gameObject.transform.localScale = currentScale;
                flipped = false;
            }

            if (transform.position.x > boundLeft)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else
            {
                goingRight = true;
            }
        }
       
    }

    private void TakeInput()
    {
        if (Input.GetKey("space"))
        {
            animator.SetBool("Mosh", false);
            Duck();
            
        }
        else
        {
            animator.SetBool("Mosh", true);
            Mosh();
        }

    }

    private void Mosh()
    { 
        //sprite.color = new Color(0,1,0,1);
        speed = moshSpeed;
        moshing = true;
    }

    private void Duck()
    {  
        //sprite.color = new Color(1, 0, 0, 1);
        speed = duckSpeed;
        moshing = false;
        

    }

    private void HealthManager()
    {
        for (int i = 0; i < teeth.Length; i++)
        {
            if (hp > numOfTeeth)
            {
                hp = numOfTeeth;
            }

            if (i < hp)
            {
                teeth[i].sprite = fullTooth;
            }
            else
            {
                teeth[i].sprite = emptyTooth;

            }

            if (i < numOfTeeth)
            {
                teeth[i].enabled = true;
            }
            else
            {
                teeth[i].enabled = false;
            }
        }

        if (hp == 6)
        {
            spawner.GetComponent<SpawnerScript>().passedMiddleFaller = 0;
        }
    }

    private void SpawnBlood()
    {
        for (int i = 0; i < bloodNum; i++)
        {
            bloodGroup[i] = GameObject.Instantiate(blood, teeth[i].transform.position, teeth[i].transform.rotation);
        }

    }


}
