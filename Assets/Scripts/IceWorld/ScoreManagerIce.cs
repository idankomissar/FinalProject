using UnityEngine;
using TMPro;

public class ScoreManagerIce : MonoBehaviour
{
    #region Singelton Decleration
    private static ScoreManagerIce _instance;

    public static ScoreManagerIce Instance { get { return _instance; } }


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
    public int scoreTime = 480;
    public int scoreRecycle;
    public int timerTime = 480;
    public TextMeshPro scoreTimeText;
    public TextMeshPro gameOverText;
    public int numberOfJunks;
    private Vector3 a = new Vector3(0, 0.1F, 0);
    private Vector3 b = new Vector3(1, 0.1F, 0);
    private int totalObjects = 5;
    public Glacier[] icebergs;
   

    private void Start()
    {
        SetTime();
        var objects = GameObject.FindGameObjectsWithTag("iceberg");
        icebergs = new Glacier[objects.Length];
        for (int i=0; i<objects.Length; i++)
        {
            icebergs[i] = new Glacier(objects[i],i);
        }
    }

    void Update()
    {
        numberOfJunks = (GameObject.FindGameObjectsWithTag("Junk")).Length;
        if (scoreTime >= 0 && numberOfJunks > 0)
        {
            scoreTime = (int)(timerTime - Time.time);
            SetTime();
        }
        else
        {
            finishGame();
        }
    }

    public void SetTime()
    {
        if (timeBar != null)
        {
            timeBar.transform.localScale = Vector3.Lerp(a, b, (480.0F - Time.time) / 480.0F);
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

    public void Enlarge(Glacier obj)
    {
        Debug.Log("entered enlarge");

        if (obj.glacier.activeSelf)
        {
            Debug.Log("entered if");

            obj.glacier.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            Debug.Log("entered else");

            obj.glacier.SetActive(true);
        }
    }

    public void Shrink(Glacier obj)
    {
        Debug.Log("entered shrinks");

        if ((obj.glacier.transform.localScale.x -0.5f) >= obj.initialScale.x)
        {
            Debug.Log("entered if");

            obj.glacier.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            Debug.Log("entered else");

            obj.glacier.SetActive(false);
        }
    }

    public void finishGame()
    {
        Destroy(scoreTimeText);
        gameOverText.text = "Game Over! Your score is: " + scoreRecycle.ToString();
    }
}
