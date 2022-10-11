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
    private Vector3 a = new Vector3(0, 0.1F, 0);
    private Vector3 b = new Vector3(1, 0.1F, 0);
    public Glacier[] icebergs;
    public GameObject firstGroup;
    public GameObject secondGroup;
    public GameObject thirdGroup;
    public GameObject olaf;
    public bool second = false;
    public bool third = false;
    public GameObject LeftIceField;
    public GameObject RightIceField;
    public bool LeftIceFieldAppear = false;
    public bool RightIceFieldAppear = false;
    public int totalNumOfJunk;
    public int CurrNumOfJunks;
    public int NumOfJunksInFirstGroup;
    public int NumOfJunksInSecondGroup;
    public int NumOfJunksInThirdGroup;
    public GameObject[] animals;
    public int chosenAnimalIndex = 0;


    private void Start()
    {   
        totalNumOfJunk = GameObject.FindGameObjectsWithTag("Junk").Length;
        CurrNumOfJunks = totalNumOfJunk;
        NumOfJunksInFirstGroup = firstGroup.transform.childCount;
        NumOfJunksInSecondGroup = secondGroup.transform.childCount;
        NumOfJunksInThirdGroup = thirdGroup.transform.childCount;
        NumOfJunksInThirdGroup = thirdGroup.transform.childCount;
        animals = GameObject.FindGameObjectsWithTag("Animal");
        for (int i=0; i<animals.Length; i++)
        {
            animals[i].SetActive(false);
        }

        SetTime();
        var objects = GameObject.FindGameObjectsWithTag("iceberg");
        icebergs = new Glacier[objects.Length];
        for (int i=0; i<objects.Length; i++)
        {
            icebergs[i] = objects[i].GetComponent<Glacier>();
            icebergs[i].Set(objects[i], i);
        }
        secondGroup.SetActive(false);
        thirdGroup.SetActive(false);
        olaf.SetActive(false);
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
        CurrNumOfJunks--;
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

    public void AddToEnvironment()
    {
        if (scoreRecycle >= 1 && !LeftIceFieldAppear)
        {
            LeftIceField.SetActive(true);
            LeftIceFieldAppear = true;
        }
        else if (scoreRecycle >= 2 && !RightIceFieldAppear)
        {
            RightIceField.SetActive(true);
            RightIceFieldAppear = true;
        }
        else if (CurrNumOfJunks > 0)
        {
            animals[chosenAnimalIndex].SetActive(true);
            chosenAnimalIndex++;
        }

    }

    public void DiscardFromEnvironment()
    {
        if (scoreRecycle > 2 && CurrNumOfJunks > 0 && chosenAnimalIndex > 0)
        {
            chosenAnimalIndex--;
            animals[chosenAnimalIndex].SetActive(false);
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
        olaf.SetActive(true);
    }
}
