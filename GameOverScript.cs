using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScript : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject middle;
    [SerializeField] private GameObject playAgainButton;
    [SerializeField] private TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if (player.GetComponent<PlayerScript>().hp <= 0)
        {
            spawner.GetComponent<SpawnerScript>().startGame = false;
            middle.GetComponent<PassMiddleScript>().startGame = false;
            playAgainButton.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(true);
            scoreText.SetText("Your Time: " + spawner.GetComponent<SpawnerScript>().gameTimer);
        }
    }
}
