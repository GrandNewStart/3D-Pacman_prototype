using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerLogic : MonoBehaviour
{
    public int total;
    public int stage;
    public float restartDelay;
    public Text totalScore;
    public Text playerScore;
    public Text stageCount;
    public Text gameOver;
    public Image itemImage;

    public void Awake()
    {
        totalScore.text = "/ " + total.ToString();
        stageCount.text = "Stage : " + stage.ToString();
        gameOver.gameObject.SetActive(false);
    }

    public void getItem(int score)
    {
        playerScore.text = score.ToString();
    }

    public void stageClear()
    {
        SceneManager.LoadScene(++stage);
        Cursor.lockState = CursorLockMode.None;
    }

    public void stageLost()
    {
        totalScore.gameObject.SetActive(false);
        playerScore.gameObject.SetActive(false);
        stageCount.gameObject.SetActive(false);
        itemImage.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(true);
        Invoke("Restart", restartDelay);
        Restart();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
