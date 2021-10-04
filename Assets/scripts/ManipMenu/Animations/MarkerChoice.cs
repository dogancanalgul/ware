using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarkerChoice : MonoBehaviour
{
    // Start is called before the first frame update
    private TMP_Dropdown dropdown;
    void Start()
    {
        EntityBase.current.OnSelect += OnSelect;
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSelect()
    {
        if (null == EntityBase.current.selected)
        {// reset and disable
            dropdown.SetValueWithoutNotify(0);
            dropdown.enabled = false;
        }
        else
        {//TODO MAKE A BETTER MARKER CHOOSE SYSTEM
            dropdown.enabled = true;
            if ("d" == EntityBase.current.selected.
                GetComponent<EntityComponents>().marker)
                dropdown.SetValueWithoutNotify(0);
            else
                dropdown.SetValueWithoutNotify(1);
        }
    }
    public void onChange()
    {
        if (dropdown.value == 0)
            EntityBase.current.selected.GetComponent<EntityComponents>().marker = "d";
        else if(dropdown.value == 1)
            EntityBase.current.selected.GetComponent<EntityComponents>().marker = "hiro";

    }
}
