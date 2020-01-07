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
    public GameObject gameOver;
    public Image itemImage;
    public static bool pause = false;
    public GameObject pauseMenu = null;

    public void Awake()
    {
        totalScore.text = "/ " + total.ToString();
        stageCount.text = "Stage : " + stage.ToString();
        gameOver.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
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
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pause = true;
    }
}
