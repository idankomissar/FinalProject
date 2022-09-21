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
    public int scoreTime = 480;
    public int scoreRecycle;
    public int timerTime = 480;
    public TextMeshPro scoreTimeText;
    public TextMeshPro scoreRecycleText;
    public TextMeshPro gameOverText;
    public int numberOfJunks;
    private Vector3 a = new Vector3(0, 0.1F, 0);
    private Vector3 b = new Vector3(1, 0.1F, 0);
    private int totalObjects = 5;

    private void Start()
    {
        SetTime();
        SetScoreRecycle();  
    }

    void Update()
    {
        numberOfJunks = (GameObject.FindGameObjectsWithTag("Junk")).Length;
        Debug.Log("number of junks is " + numberOfJunks);
        if (scoreTime >= 0 && totalObjects- scoreRecycle > 0)
        {
            scoreTime = (int)(timerTime - Time.time);
            SetTime();
            SetScoreRecycle();
        }
        else
        {
            finishGame();
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
            Debug.Log("set score recycle");
            scoreBar.transform.localScale = new Vector3(((float)scoreRecycle / (float)totalObjects), 0.1F, 0);
        }
        
    }

    public void AddScoreRecycle(int score)
    {
        scoreRecycle += score;
    }

    public void TrashEnteredToBin()
    {
        numberOfJunks--;
    }


    public void finishGame()
    {
        Destroy(scoreRecycleText);
        Destroy(scoreTimeText);
        gameOverText.text = "Game Over! Your score is: " + scoreRecycle.ToString();
    }
}
