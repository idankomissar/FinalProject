using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    #region Singelton Decleration

    private static DataManager _instance;

    public static DataManager Instance { get { return _instance; } }


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

    public string participantName;

    string pathContinuous;
    string pathDiscrete;

    StreamWriter writerDiscrete;
    StreamWriter writerContinuous;

    public Transform Head;
    public Transform RightHand;
    public Transform LeftHand;

    void Start()
    {
        pathContinuous = @$"Assets\Data\ContinuousTest_{participantName}.txt";
        pathDiscrete = @$"Assets\Data\DiscreteTest_{participantName}.txt";

        writerDiscrete = new StreamWriter(pathDiscrete, false);
        writerContinuous = new StreamWriter(pathContinuous, false);

        // write files headers
        writerContinuous.WriteLine("Time," +
                                   "RightHand_Position_X,RightHand_Position_Y,RightHand_Position_Z," +
                                   "LeftHand_Position_X,LeftHand_Position_Y,LeftHand_Position_Z," +
                                   "Head_Position_X,Head_Position_Y,Head_Position_Z");

        writerDiscrete.WriteLine("Time," +
                                 "Score," +
                                  "Pickup_Position_X," +
                                  "Pickup_Position_Y," +
                                  "Pickup_Position_Z");
    }

    void FixedUpdate()
    {
        // write continuous data
        writerContinuous.Write($"{Time.time},");
        writerContinuous.Write($"{RightHand.position.x},{RightHand.position.y},{RightHand.position.z},");
        writerContinuous.Write($"{LeftHand.position.x},{LeftHand.position.y},{LeftHand.position.z},");
        writerContinuous.WriteLine($"{Head.position.x},{Head.position.y},{Head.position.z}");
    }

    // write discrete data
    public void WriteTrashEnteredEvent(int score, Vector3 pickupPosition)
    {
        writerDiscrete.Write($"{Time.time},");
        writerDiscrete.Write($"{score},");
        writerDiscrete.WriteLine($"{pickupPosition.x},{pickupPosition.y},{pickupPosition.z}");
    }


    private void OnDestroy()
    {
        writerDiscrete.Flush();
        writerDiscrete.Close();
        writerContinuous.Flush();
        writerContinuous.Close();
    }
}