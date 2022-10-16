using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class Glacier : MonoBehaviour
{
    public GameObject glacier;
    public Vector3 initialPosition;
    public int index;
    public void Awake()
    {
        initialPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
    public void Set(GameObject g, int i)
    {
        glacier = g;
        index = i;
    }
}
