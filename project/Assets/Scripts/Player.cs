using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int score;
    private AudioSource itemCollected;
    public GameManagerLogic manager;

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

            if (score == manager.total)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
