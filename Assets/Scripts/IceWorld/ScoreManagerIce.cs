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
    public Glacier[] icebergs;
    public GameObject secondgroup;
    //public GameObject olaf;

    public bool first = false;
    public bool second = false;
    public bool third = false;
    public bool fourth = false;
    public bool fifth = false;


    private void Start()
    {
        SetTime();
        var objects = GameObject.FindGameObjectsWithTag("iceberg");
        icebergs = new Glacier[objects.Length];
        for (int i=0; i<objects.Length; i++)
        {
            icebergs[i] = objects[i].GetComponent<Glacier>();
            icebergs[i].Set(objects[i], i);
        }
        //olaf = GameObject.FindWithTag("olaf");
        ////olaf.SetActive(false);
    }

    void Update()
    {
        if (fifth)
        {
            numberOfJunks = (GameObject.FindGameObjectsWithTag("Junk")).Length;
        }
        else
        {
            numberOfJunks = (GameObject.FindGameObjectsWithTag("Junk")).Length + 5;
        }
        if (scoreTime >= 0 && numberOfJunks > 0)
        {
            scoreTime = (int)(timerTime - Time.time);
            SetTime();
        }
        if (numberOfJunks <= 14 && !second)
        {
            second = true;
            secondgroup.SetActive(true);
        }

        if (scoreTime == 0 || numberOfJunks == 0)
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
        if (obj.glacier.activeSelf)
        {
            obj.glacier.transform.localScale += new Vector3(5f, 5f, 5f);
        }
        else
        {
            obj.glacier.SetActive(true);
        }
    }

    public void Shrink(Glacier obj)
    {
        if ((obj.glacier.transform.localScale.x -5f) >= obj.initialScale.x)
        {
            obj.glacier.transform.localScale -= new Vector3(5f, 5f, 5f);
        }
        else
        {
            obj.glacier.SetActive(false);
        }
    }

    public void finishGame()
    {
        Destroy(scoreTimeText);
        gameOverText.text = "Game Over!";
        //olaf.SetActive(true);
    }
}
