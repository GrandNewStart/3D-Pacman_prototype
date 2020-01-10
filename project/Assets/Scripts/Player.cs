using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int score;
    public GameManagerLogic manager;
    private AudioSource itemCollected;

    void Start()
    {
        score = 0;
        itemCollected = GetComponent<AudioSource>();
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
                manager.stageClear();
            }
        }
    }
}
