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
    public int chosenAnimalIndex = -1;
    public int numOfIceBergs;

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
        for (int i = 0; i < animals.Length; i++)
        {
            animals[i].SetActive(false);
        }

        numOfIceBergs = GameObject.FindGameObjectsWithTag("iceberg").Length;
        var objects = GameObject.FindGameObjectsWithTag("iceberg");
        icebergs = new Glacier[numOfIceBergs];
        for (int i = 0; i < numOfIceBergs; i++)
        {
            icebergs[i] = objects[i].GetComponent<Glacier>();
            icebergs[i].Set(objects[i], i);
            objects[i].SetActive(false);
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
            FinishGame();
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

    public void RaiseGlacier(Glacier obj)
    {
        Debug.Log("entered RaiseGlacier: Glacier " + obj.glacier.name);
        if (obj.glacier.activeSelf)
        {
            //Debug.Log("curr y position = " + obj.glacier.transform.position.y + " intitial pos = " + obj.initialPosition.y);
            //Debug.Log("curr x scale = " + obj.glacier.transform.localScale.x + " intitial scale = " + obj.initialScale.x);
            obj.glacier.transform.position += new Vector3(0f, 0.04f, 0f);
            obj.glacier.transform.localScale += new Vector3(0.008f, 0f, 0.008f);
        }
        else
        {
            //Debug.Log("setActive = true for " + obj.glacier.name);
            obj.glacier.SetActive(true);
        }
    }

    public bool LowerGlacier(Glacier obj)
    {
        Debug.Log("entered lowerGlacier: Glacier "+obj.glacier.name);
        if (obj.glacier.activeSelf)
        {
            //Debug.Log("curr y position = " + obj.glacier.transform.position.y+ " intitial pos = " + obj.initialPosition.y);
            //Debug.Log("curr x scale = " + obj.glacier.transform.localScale.x + " intitial scale = " + obj.initialScale.x);
            if ((obj.glacier.transform.localScale.x - 0.008f) >= obj.initialScale.x)
            {
                obj.glacier.transform.position -= new Vector3(0f, 0.04f, 0f);
                obj.glacier.transform.localScale -= new Vector3(0.008f, 0.008f, 0.008f);
            }
            else
            {
                //Debug.Log("setActive = false for " + obj.glacier.name);
                obj.glacier.SetActive(false);
            }
            return true;
        }
        return false;

    }

    public void AddAnimal()
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
        else if (chosenAnimalIndex < animals.Length)
        {
            if (chosenAnimalIndex < 0)
            {
                chosenAnimalIndex = -1;
            }
            chosenAnimalIndex++;
            animals[chosenAnimalIndex].SetActive(true);
        }

    }

    public bool DiscardAnimal()
    {
        if (chosenAnimalIndex >= 0)
        {
            animals[chosenAnimalIndex].SetActive(false);
            chosenAnimalIndex -= 1;
            return true;
        }
        return false;
    }

    public void FinishGame()
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
