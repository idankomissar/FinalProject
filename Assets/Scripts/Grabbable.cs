using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("TrashCan"))
        {
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
