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
        if (other.tag.Equals("PlayerHand"))
        {
            transform.SetParent(other.transform);
        }
    }

    private void PutInTrashCan(GameObject can)
    {

        Destroy(can);
    }
}
