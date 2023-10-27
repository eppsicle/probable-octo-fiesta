using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private float holdDuration = 3f;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject player;
    private float holdElapsed = 0;
    [SerializeField] private StartGameScript startGamePlayer;
    [SerializeField] private PlayAgainScript playAgainPlayer;
    private float bufferTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HoldStart();
    }

    private void HoldStart()
    {
        if (spawner.GetComponent<SpawnerScript>().startGame == false && player.GetComponent<PlayerScript>().hp == 6)
        {
            bufferTime += Time.deltaTime;

            if (bufferTime >= 2f)
            {
                if (Input.GetKey("space"))
                {
                    holdElapsed += Time.deltaTime;
                }
                else
                {
                    holdElapsed = 0;
                }

                if (holdElapsed >= holdDuration)
                {
                    startGamePlayer.StartGame();
                }
            }
        }

        else if (spawner.GetComponent<SpawnerScript>().startGame == false && player.GetComponent<PlayerScript>().hp == 0)
        {
            if (Input.GetKey("space"))
            {
                holdElapsed += Time.deltaTime;
            }
            else
            {
                holdElapsed = 0;
            }

            if (holdElapsed >= holdDuration)
            {
                playAgainPlayer.PlayAgain();
            }

            bufferTime = 0f;
        }
    }
}
