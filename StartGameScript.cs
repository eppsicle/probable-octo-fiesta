using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartGameScript : MonoBehaviour
{
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject middle;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text tutorial;

    public void StartGame()
    {
        spawner.GetComponent<SpawnerScript>().startGame = true;
        middle.GetComponent<PassMiddleScript>().startGame = true;
        title.gameObject.SetActive(false);
        gameObject.SetActive(false);
        tutorial.gameObject.SetActive(true);

        spawner.GetComponent<SpawnerScript>().gameTimer = 0;
    }

}
