using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Glacier : MonoBehaviour
{
    public GameObject glacier;
    public Vector3 initialScale;
    public int index;
    public Glacier(GameObject g, int i)
    {
        glacier = g;
        initialScale = new Vector3(g.transform.localScale.x, g.transform.localScale.y, g.transform.localScale.z);
        index = i;
    }
}
