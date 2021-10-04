using Assets.scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour
{
    public static EntityBase current = null;

    public List<GameObject> entities;
    public GameObject selected = null;
    public Color oldColor = Color.gray;

    public event Action OnSelect;
    public event Action Gizmo;


    public bool selectedColorChanged;
    public bool global_gesture = false;

    void Awake()
    {
        current = this;
        entities = new List<GameObject>();
    }

    void Update()
    {
    }
    public void select(GameObject select)
    {
        if(selected != null && !selectedColorChanged)
            selected.GetComponent<Renderer>().material.color = oldColor;
        selectedColorChanged = false;

        if (select != null)
            oldColor = Statics.select(select);
        
        selected = select;

        if (OnSelect != null)
            OnSelect();
    }
}
