using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrashCanType
{
    Plastic,
    Paper,
    Glass,
    Metal,
    Regular
}

public class TrashCan : MonoBehaviour
{
    public TrashCanType type;

    public void TrashEntered(Grabbable trash)
    {
        ScoreManager.Instance.TrashEnteredToBin();
        if (type.ToString().Equals(trash.type.ToString()))
        {
            ScoreManager.Instance.AddScoreRecycle(1);
            ScoreManager.Instance.SetScoreRecycle();
        }
    }
}
