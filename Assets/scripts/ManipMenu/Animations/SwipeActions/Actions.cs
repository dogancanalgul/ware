using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Actions : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Dropdown[] options; // up down left right
    private string[] actions = new string[] { "none", "destroy", "hide", "show", "move" };
    public Toggle global_gesture;

    void Start()
    {
        EntityBase.current.OnSelect += OnSelect;
        foreach (TMP_Dropdown d in options)
            d.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSelect()
    {
        if(null == EntityBase.current.selected)
        {// reset and disable
            foreach (TMP_Dropdown d in options)
            {
                d.SetValueWithoutNotify(0);
                d.enabled = false;
                d.SetValueWithoutNotify(0);
            }
        }
        else
        {
            //load the button information from the selected
            for (int i = 0; i < 4; ++i)
            {
                options[i].enabled = true;
                options[i].SetValueWithoutNotify(str2intAction(
                    EntityBase.current.selected.GetComponent<EntityComponents>().swipe.actions[i]));
            }
        }
    }
    public void OnValueChange()// up down left right from 0
    {
        //0 up
        if (null == EntityBase.current.selected)
            return;
        EntityComponents ec = EntityBase.current.selected.GetComponent<EntityComponents>();
        for(int i = 0; i < 4; ++i)
            ec.swipe.actions[i] = actions[options[i].value];
    }
    public int str2intAction(string str)
    {
        for (int i = 0; i < actions.Length; ++i)
            if (str == actions[i])
                return i;
        return -1;
    }
    public void globalGesture()
    {
        EntityBase.current.global_gesture = global_gesture.isOn;
    }
}
