using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    #region Singelton Decleration
    private static ScoreManager _instance;

    public static ScoreManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
           Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public GameObject timeBar;
    public GameObject scoreBar;
    public GameObject timeBarBackgroud;
    public GameObject scoreBarBackground;
    public int scoreTime = 480;
    public int scoreRecycle;
    public int timerTime = 480;
    public TextMeshPro scoreTimeText;
    public TextMeshPro scoreRecycleText;
    public TextMeshPro gameOverText;
    private Vector3 a = new Vector3(0, 0.1F, 0);
    private Vector3 b = new Vector3(1, 0.1F, 0);
    public GameObject firstGroup;
    public GameObject secondGroup;
    public GameObject thirdGroup;
    public GameObject bins;
    public bool second = false;
    public bool third = false;
    public int totalNumOfJunk;
    public int CurrNumOfJunks;
    public int NumOfJunksInFirstGroup;
    public int NumOfJunksInSecondGroup;
    public int NumOfJunksInThirdGroup;

    private void Start()
    {
        totalNumOfJunk = GameObject.FindGameObjectsWithTag("Junk").Length;
        CurrNumOfJunks = totalNumOfJunk;
        NumOfJunksInFirstGroup = firstGroup.transform.childCount;
        NumOfJunksInSecondGroup = secondGroup.transform.childCount;
        NumOfJunksInThirdGroup = thirdGroup.transform.childCount;
        NumOfJunksInThirdGroup = thirdGroup.transform.childCount;
        SetTime();
        SetScoreRecycle();
        secondGroup.SetActive(false);
        thirdGroup.SetActive(false);
    }

    void Update()
    {
        if (scoreTime >= 0 && CurrNumOfJunks > 0)
        {
            scoreTime = (int)(timerTime - Time.time);
            SetTime();
        }
        else
        {
            finishGame();
        }

        if ((totalNumOfJunk - NumOfJunksInFirstGroup) >= CurrNumOfJunks && !second)
        {
            secondGroup.SetActive(true);
            second = true;
        }

        else if ((totalNumOfJunk - (NumOfJunksInFirstGroup + NumOfJunksInSecondGroup)) >= CurrNumOfJunks && !third)
        {
            thirdGroup.SetActive(true);
            third = true;
        }

    }

    public void SetTime()
    {
        if (timeBar!= null)
        {
            timeBar.transform.localScale = Vector3.Lerp(a, b, (480.0F - Time.time) / 480.0F);
        }
    }

    public void SetScoreRecycle()
    {
        if (scoreBar!= null)
        {
            scoreBar.transform.localScale = new Vector3(((float)scoreRecycle / (float)totalNumOfJunk), 0.1F, 0);
        }
        
    }

    public void AddScoreRecycle(int score)
    {
        scoreRecycle += score;
    }

    public void TrashEnteredToBin()
    {
        CurrNumOfJunks--;
    }


    public void finishGame()
    {
        firstGroup.SetActive(false);
        secondGroup.SetActive(false);
        thirdGroup.SetActive(false);
        bins.SetActive(false);
        Destroy(scoreRecycleText);
        Destroy(scoreTimeText);
        Destroy(timeBar);
        Destroy(scoreBar);
        Destroy(timeBarBackgroud);
        Destroy(scoreBarBackground);
        gameOverText.text = "Game Over! Your score is: " + scoreRecycle.ToString();
    }
}
