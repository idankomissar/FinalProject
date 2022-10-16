using UnityEngine;
using TMPro;
using System.Linq;
using System;
using System.Collections.Generic;

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
    public GameObject timeBarBackgroud;
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
    public GameObject bins;
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
    //public List<GameObject> animalsList;
    public int chosenAnimalIndex = 0;
    public int numOfIceBergs;
    public int numOfAnimals;



    private void Start()
    {   
        totalNumOfJunk = GameObject.FindGameObjectsWithTag("Junk").Length;
        CurrNumOfJunks = totalNumOfJunk;
        NumOfJunksInFirstGroup = firstGroup.transform.childCount;
        NumOfJunksInSecondGroup = secondGroup.transform.childCount;
        NumOfJunksInThirdGroup = thirdGroup.transform.childCount;
        NumOfJunksInThirdGroup = thirdGroup.transform.childCount;
        secondGroup.SetActive(false);
        thirdGroup.SetActive(false);

        animals = GameObject.FindGameObjectsWithTag("Animal");
        System.Random random = new System.Random();
        animals = animals.OrderBy(x => random.Next()).ToArray();
        //animalsList = animals.OrderBy(x => UnityEngine.Random.value).ToList();
        numOfAnimals = animals.Length;
        for (int i=0; i< numOfAnimals; i++)
        {
            animals[i].SetActive(false);
        }
        
        numOfIceBergs = GameObject.FindGameObjectsWithTag("iceberg").Length;
        var objects = GameObject.FindGameObjectsWithTag("iceberg");
        icebergs = new Glacier[numOfIceBergs];
        for (int i=0; i < numOfIceBergs; i++)
        {
            icebergs[i] = objects[i].GetComponent<Glacier>();
            icebergs[i].Set(objects[i], i);
        }
        olaf.SetActive(false);
        
        SetTime();
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

    public void AddGlacier(Glacier[] glaciers)
    {
        if (scoreRecycle >= 7)
        {
            glaciers[0].gameObject.SetActive(true);
        }
        else if (scoreRecycle >= 5)
        {
            glaciers[1].gameObject.SetActive(true);
        }
        else if (scoreRecycle >= 3)
        {
            glaciers[2].gameObject.SetActive(true);
        }
    }    
    
    public void RaiseGlacier(Glacier obj)
    {
        if (obj.glacier.activeSelf)
        {
            obj.glacier.transform.position += new Vector3(0f, 0.04f, 0f);
        }
        else
        {
            obj.glacier.SetActive(true);
        }
    }

    public void LowerGlacier(Glacier obj)
    {
        if ((obj.glacier.transform.position.y - 0.04f) >= obj.initialPosition.y)
        {
            obj.glacier.transform.position -= new Vector3(0f, 0.04f, 0f);
        }
        else
        {
            obj.glacier.SetActive(false);
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

        else if (CurrNumOfJunks > 0 && chosenAnimalIndex < numOfAnimals)
        {
            animals[chosenAnimalIndex].SetActive(true);
            chosenAnimalIndex++;
        }

    }

    public void DiscardAnimalFromEnvironment()
    {
        if (scoreRecycle > 2 && CurrNumOfJunks > 0 && chosenAnimalIndex > 0)
        {
            chosenAnimalIndex--;
            animals[chosenAnimalIndex].SetActive(false);
        }
    }

    public void finishGame()
    {
        firstGroup.SetActive(false);
        secondGroup.SetActive(false);
        thirdGroup.SetActive(false);
        bins.SetActive(false);
        Destroy(timeBar);
        Destroy(timeBarBackgroud);
        Destroy(scoreTimeText);
        gameOverText.text = "Game Over!";
        olaf.SetActive(true);
    }
}
