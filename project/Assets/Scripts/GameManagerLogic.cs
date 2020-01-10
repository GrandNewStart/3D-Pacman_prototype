using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerLogic : MonoBehaviour
{
    public int total = 0;
    public int stage = 0;
    public float restartDelay = 0f;
    public GameObject gameOver = null;
    [Header("UI")]
    public GameObject HUD = null;
    public Text totalScore = null;
    public Text playerScore = null;
    public Text stageCount = null;
    public Image itemImage = null;
    [Header("Pause Screen")]
    public GameObject pauseMenu = null;
    public PlayerMovement playerMovement = null;
    public MouseLook mouseLook = null;
    public static bool pause = false;
    public bool over = false;
    

    public void Awake()
    {
        totalScore.text = "/ " + total.ToString();
        stageCount.text = "Stage : " + stage.ToString();
        gameOver.gameObject.SetActive(false);
        over = false;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !over)
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
        HUD.SetActive(false);
        gameOver.gameObject.SetActive(true);
        over = true;
        playerMovement.speed = 0f;
        mouseLook.mouseSensitivity = 0f;
        Invoke("Restart", restartDelay);
    }

    public void BeginStage()
    {
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(stage);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pause = true;
    }
}
