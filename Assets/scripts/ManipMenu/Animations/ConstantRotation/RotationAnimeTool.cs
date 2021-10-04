using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RotationAnimeTool : MonoBehaviour
{
    // Start is called before the first frame update
    private Toggle toggle;
    public TMP_InputField[] axis;
    void Awake()
    {
        EntityBase.current.OnSelect += OnSelect;
        toggle = GetComponentInChildren<Toggle>();
        toggle.SetIsOnWithoutNotify(false);
        toggle.enabled = false;
        disableInputs();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleAnimation()
    {
        if (null == EntityBase.current || null == EntityBase.current.selected)
            return;
        EntityBase.current.selected.GetComponent<EntityComponents>().rotation = toggle.isOn;
        if (toggle.isOn)
            enableInputs();
        else
            disableInputs();
    }
    private void disableInputs()
    {
        foreach (TMP_InputField t in axis)
        {
            t.enabled = false;
            t.SetTextWithoutNotify("");
        }
    }
    private void enableInputs()
    {
        foreach (TMP_InputField t in axis)
            t.enabled = true;
    }
    public void OnSelect()
    {
        if (null == EntityBase.current.selected)
        {
            toggle.SetIsOnWithoutNotify(false);
            toggle.enabled = false;
            disableInputs();
            return;
        }
        toggle.enabled = true;
        toggle.isOn = EntityBase.current.selected.GetComponent<EntityComponents>().rotation;
        Vector3 rot = EntityBase.current.selected.GetComponent<EntityComponents>().rotationAxis;
        if (toggle.isOn)
        {
            axis[0].SetTextWithoutNotify(rot.x.ToString());
            axis[1].SetTextWithoutNotify(rot.y.ToString());
            axis[2].SetTextWithoutNotify(rot.z.ToString());
        }
        //update fields Accordingly if off clear and disable
    }
    public void ChangeAxis()
    {
        Vector3 rot = Vector3.zero;
        rot.x = Statics.convertFloat(axis[0].text);
        rot.y = Statics.convertFloat(axis[1].text);
        rot.z = Statics.convertFloat(axis[2].text);
        EntityBase.current.selected.GetComponent<EntityComponents>().rotationAxis = rot;
    }
}
