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
        var isCompatible = false;
        ScoreManager.Instance.TrashEnteredToBin();
        switch(trash.type.ToString())
        {
            case "Plastic":
                AudioManager.Instance.Play("TrashDrop");
                break;
            case "Paper":
                AudioManager.Instance.Play("TrashDrop");
                break;
            case "Glass":
                AudioManager.Instance.Play("TrashDrop");
                break;
            case "Metal":
                AudioManager.Instance.Play("TrashDrop");
                break;
            default:
                break;
        }
        if (type.ToString().Equals(trash.type.ToString()))
        {
            ScoreManager.Instance.AddScoreRecycle(1);
            ScoreManager.Instance.SetScoreRecycle();
            isCompatible = true;
        }
        var pos = trash.transform.position;
        DataManager.Instance.WriteTrashEnteredEvent(ScoreManager.Instance.scoreRecycle, isCompatible, new Vector3(pos.x, pos.y, pos.z));
    }
}
