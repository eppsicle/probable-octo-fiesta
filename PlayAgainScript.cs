using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayAgainScript : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject player;

    public void PlayAgain()
    {
        GameObject[] allBlood = player.GetComponent<PlayerScript>().bloodGroup;
        startButton.SetActive(true);
        gameObject.SetActive(false);
        title.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        player.GetComponent<PlayerScript>().hp = 6;
        for (int i = 0; i < allBlood.Length; i++)
        {
            allBlood[i].GetComponent<ParticleSystem>().Stop();
        }
        
    }
}
