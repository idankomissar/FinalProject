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
            isCompatible = true;
            var score = ScoreManagerIce.Instance.scoreRecycle;
            if (score <= 2 || score % 2 == 1 || score % 10 == 0)
            {
                ScoreManagerIce.Instance.AddToEnvironment();
  
            }
            else
            {
                for (int i = 0; i < ScoreManagerIce.Instance.numOfIceBergs; i++)
                {
                    ScoreManagerIce.Instance.RaiseGlacier(ScoreManagerIce.Instance.icebergs[i]);
                }
            }
        }

        else
        {
            var score = ScoreManagerIce.Instance.scoreRecycle;
            if (score > 2 && (score % 2 == 1 || score % 10 == 0))
            {
                ScoreManagerIce.Instance.DiscardAnimalFromEnvironment();
            }
            else
            {
                for (int i = 0; i < ScoreManagerIce.Instance.numOfIceBergs; i++)
                {
                    ScoreManagerIce.Instance.LowerGlacier(ScoreManagerIce.Instance.icebergs[i]);
                }
            }
        }
        var pos = trash.transform.position;
        DataManager.Instance.WriteTrashEnteredEvent(ScoreManagerIce.Instance.scoreRecycle, isCompatible, new Vector3(pos.x, pos.y, pos.z));
    }
}
