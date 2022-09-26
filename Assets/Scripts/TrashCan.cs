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
        AudioManager.Instance.Play("TrashDrop");
        if (type.ToString().Equals(trash.type.ToString()))
        {
            ScoreManager.Instance.AddScoreRecycle(1);
            ScoreManager.Instance.SetScoreRecycle();
        }
        var pos = trash.transform.position;
        DataManager.Instance.WriteTrashEnteredEvent(ScoreManager.Instance.scoreRecycle, new Vector3(pos.x, pos.y, pos.z));

    }
}
