using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrashType
{
    Plastic,
    Paper,
    Glass,
    Metal
}

public class Grabbable : MonoBehaviour
{
    public TrashType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("TrashCan"))
        {
            other.GetComponent<TrashCan>().TrashEntered(this);
            PutInTrashCan(gameObject);
        }
        else if (other.tag.Equals("TrashCanIce"))
        {
            other.GetComponent<TrashCanIce>().TrashEntered(this);
            PutInTrashCan(gameObject);
        }
        else if (other.tag.Equals("PlayerHand"))
        {
            AudioManager.Instance.Play("PickUp");
            transform.SetParent(other.transform);
        }
    }

    private void PutInTrashCan(GameObject junk)
    {
        Destroy(junk);
    }
}
