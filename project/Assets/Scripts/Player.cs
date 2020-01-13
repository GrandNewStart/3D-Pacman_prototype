using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int score = 0;
    public GameManagerLogic manager = null;
    public AudioSource itemCollected = null;

    void Start()
    {
        score = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            itemCollected.Play();
            other.gameObject.SetActive(false);
            score++;
            manager.getItem(score);

            if (score == manager.total)
            {
                manager.StageClear();
            }
        }
    }
}
