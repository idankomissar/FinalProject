using UnityEngine;

public class TrashCanIce : MonoBehaviour
{
    public TrashCanType type;


    public void TrashEntered(Grabbable trash)
    {
        var isCompatible = false;
        ScoreManagerIce.Instance.TrashEnteredToBin();
        switch (trash.type.ToString())
        {
            case "Plastic":
                AudioManager.Instance.Play("PlasticDrop");
                break;
            case "Paper":
                AudioManager.Instance.Play("PaperDrop");
                break;
            case "Glass":
                AudioManager.Instance.Play("GlassDrop");
                break;
            case "Metal":
                AudioManager.Instance.Play("MetalDrop");
                break;
            default:
                break;
        }
        if (type.ToString().Equals(trash.type.ToString()))
        {
            ScoreManagerIce.Instance.AddScoreRecycle(1);
            ScoreManagerIce.Instance.Enlarge(ScoreManagerIce.Instance.icebergs[0]);
            isCompatible = true;
        }
        else
        {
            ScoreManagerIce.Instance.Shrink(ScoreManagerIce.Instance.icebergs[0]);
        }
        var pos = trash.transform.position;
        DataManager.Instance.WriteTrashEnteredEvent(ScoreManagerIce.Instance.scoreRecycle, isCompatible, new Vector3(pos.x, pos.y, pos.z));
    }
}
