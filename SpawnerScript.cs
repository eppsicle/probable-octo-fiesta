using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject mosher;
    [SerializeField] private GameObject diver;
    [SerializeField] private GameObject faller;
    [SerializeField] private GameObject player;

    private Vector3 moshLeftPos;
    private Vector3 moshRightPos;
    private Quaternion moshLeftRot;
    private Quaternion moshRightRot;

    private Vector3 diveLeftPos;
    private Vector3 diveRightPos;
    private Quaternion diveLeftRot;
    private Quaternion diveRightRot;

    private Vector3 fallLeftPos;
    private Vector3 fallRightPos;
    private Quaternion fallLeftRot;
    private Quaternion fallRightRot;

    public float yAxis = -2f;

    public float introDuration = 24f;
    public float introElapsed = 0f;


    public int totalMoshers = 0;

    public int passedMiddleMosher = 0;
    public int passedMiddleDiver = 0;
    public int passedMiddleFaller = 0;
    public int mosherSpawnThresh = 5;
    public int diverSpawnThresh = 7;
    public int fallerSpawnThresh = 4;

    private int minMoshThresh = 3;
    private int minDiveThresh = 4;
    private int minFallThresh = 2;

    private bool leftDiver = true;

    public bool spawnedFaller = false;

    public bool startGame = false;

    public float gameTimer = 0f;

    private int mosherWaves = 0;
    private int diverWaves = 0;
    private int fallerWaves = 0;

    private int waveIncModulo = 5;


    [SerializeField] private TMP_Text tutorial;

    //blue is 1050FA, 3F6BEA
    //red is FF2E2E
    //yellow is E7DE17

    private string buttonInstruct = "Press \"space\" to duck.\n\nRelease \"space\" to mosh.";
    private string healthInstruct = "The teeth are your health.\n\nDon't lose your teeth.";
    private string mosherInstruct = "Moshers are <color=#FF2E2E>red</color>.\n\nMosh into them to\navoid losing a tooth.";
    private string diverInstruct = "Stage Divers are <color=#1050FA>blue</color>.\n\nDon't let them hit you.";
    private string fallerInstruct = "Fallers are <color=#E7DE17>yellow</color>.\n\nDuck to pick them up\n and recover a tooth.";
    private string surviveInstruct = "Survive as long as you can.\n\nGo!";


    // Start is called before the first frame update
    void Start()
    {
        SetMoshLocation();
        SetDiveLocation();
        SetFallLocation();
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
        {
            StartCoroutine(Tutorial());
            WaveManager();
        }
        
    }

    private void SpawnMosher()
    {
        GameObject mosherLeft = Instantiate(mosher, moshLeftPos, moshLeftRot);
        mosherLeft.GetComponent<MosherScript>().leftSide = true;
        GameObject mosherRight = Instantiate(mosher, moshRightPos, moshRightRot);
        mosherRight.GetComponent<MosherScript>().leftSide = false;
        totalMoshers += 2;
    }

    private void SetMoshLocation()
    {
        moshLeftPos = new Vector3(-7f, yAxis + 0.1f, 0f);
        moshRightPos = new Vector3(7f, yAxis + 0.1f, 0f);
        moshLeftRot = new Quaternion(0, 0, 0, 0);
        moshRightRot = new Quaternion(0, 0, 0, 0);
    }

    private void SpawnDiver(bool leftDiver)
    {
        if (leftDiver)
        {
            GameObject diverLeft = Instantiate(diver, diveLeftPos, diveLeftRot);
            diverLeft.GetComponent<DiverScript>().leftSide = true;
        }
        else
        {
            GameObject diverRight = Instantiate(diver, diveRightPos, diveRightRot);
            diverRight.GetComponent<DiverScript>().leftSide = false;
        }
    }

    private void SetDiveLocation()
    {
        diveLeftPos = new Vector3(-9f, 6f, 0f);
        diveRightPos = new Vector3(9f, 6f, 0f);
        diveLeftRot = new Quaternion(0,0,0,0);
        diveRightRot = new Quaternion(0, 0, 0, 0);
    }

    private void SpawnFaller()
    {
        if (leftDiver)
        {
            GameObject fallerLeft = Instantiate(faller, fallLeftPos, fallLeftRot);
            fallerLeft.GetComponent<FallerScript>().leftSide = true;
        }
        else
        {
            GameObject fallerRight = Instantiate(faller, fallRightPos, fallRightRot);
            fallerRight.GetComponent<FallerScript>().leftSide = false;
        }
    }


    private void SetFallLocation()
    {
        fallLeftPos = new Vector3(0f, 9f, 0f);
        fallRightPos = new Vector3(0f, 9f, 0f);
        fallLeftRot = new Quaternion(0, 0, 0, 0);
        fallRightRot = new Quaternion(0, 0, 0, 0);
    }

    private void WaveManager()
    {
        if (introElapsed < introDuration)
        {
            introElapsed += Time.deltaTime;
        }
        else if (introElapsed >= introDuration)
        {
            tutorial.gameObject.SetActive(false);

            if (player.GetComponent<PlayerScript>().hp > 0)
            {
                gameTimer += Time.deltaTime;
            }
            

            if (passedMiddleMosher >= mosherSpawnThresh && totalMoshers == 0)
            {
                SpawnMosher();
                passedMiddleMosher = 0;
                mosherWaves += 1;
            }

            if (passedMiddleDiver >= diverSpawnThresh)
            {
                SpawnDiver(leftDiver);
                leftDiver = !leftDiver;
                passedMiddleDiver = 0;
                diverWaves += 1;
            }


            if (passedMiddleFaller >= fallerSpawnThresh && player.GetComponent<PlayerScript>().hp < 6)
            {
                SpawnFaller();
                passedMiddleFaller = 0;
                SpawnDiver(leftDiver);

                fallerWaves += 1;
            }

            if (mosherWaves != 0 && mosherWaves % waveIncModulo == 0 && mosherSpawnThresh > minMoshThresh)
            {
                mosherSpawnThresh -= 1;
            }

            if (diverWaves != 0 && diverWaves % waveIncModulo == 0 && diverSpawnThresh > minDiveThresh)
            {
                diverSpawnThresh -= 2;
            }

            if (fallerWaves != 0 && fallerWaves % waveIncModulo == 0 && fallerSpawnThresh > minFallThresh)
            {
                fallerSpawnThresh -= 1;
            }



        }

    }

    IEnumerator Tutorial()
    {

        while (introElapsed < introDuration + 6)
        {
            if (introElapsed >= 5f * introDuration / 6f)
            {
                tutorial.SetText(surviveInstruct);
            }
            else if (introElapsed >= 2f * introDuration / 3f)
            {
                tutorial.SetText(fallerInstruct);
            }
            else if (introElapsed >= introDuration/ 2f)
            {
                tutorial.SetText(diverInstruct);
            }
            else if (introElapsed >= introDuration / 3f)
            {
                tutorial.SetText(mosherInstruct);
            }
            else if (introElapsed >= introDuration / 6f)
            {
                tutorial.SetText(healthInstruct);
            }
            else if (introElapsed == 0)
            {
                tutorial.SetText(buttonInstruct);
            }
            
            yield return null;
        }
    }
}
