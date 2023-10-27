using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassMiddleScript : MonoBehaviour
{
    [SerializeField] private GameObject spawner;

    public int passed = 0;

    public bool startGame = false;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player" )
        {
            if (startGame && spawner.GetComponent<SpawnerScript>().introElapsed >= spawner.GetComponent<SpawnerScript>().introDuration)
            {
                spawner.GetComponent<SpawnerScript>().passedMiddleMosher += 1;
                spawner.GetComponent<SpawnerScript>().passedMiddleDiver += 1;
                spawner.GetComponent<SpawnerScript>().passedMiddleFaller += 1;
            }
            
        }

    }
}
