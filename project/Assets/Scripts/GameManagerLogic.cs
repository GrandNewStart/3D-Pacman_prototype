using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour
{
    public int total;
    public int stage;
    public Text totalScore;
    public Text playerScore;
    public Text stageCount;

    public void Awake()
    {
        totalScore.text = "/ " + total.ToString();
        stageCount.text = "stage " + stage.ToString();
    }

    public void getItem(int score)
    {
        playerScore.text = score.ToString();
    }
}
