using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class Glacier : MonoBehaviour
{
    public GameObject glacier;
    public Vector3 initialScale;
    public int index;
    public void Awake()
    {
        initialScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
    }
    public void Set(GameObject g, int i)
    {
        glacier = g;
        //initialScale = new Vector3(g.transform.localScale.x, g.transform.localScale.y, g.transform.localScale.z);
        index = i;
    }
}
