using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityComponents : MonoBehaviour
{
    public string type = "box";
    public bool rotation = false;
    public Vector3 rotationAxis = Vector3.zero;
    public Swipe swipe = new Swipe();
    public string marker = "d";
}
public class Swipe
{
    public string[] actions = new string[] { "none", "none", "none", "none" };
}
