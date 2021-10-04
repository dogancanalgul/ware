using Assets.scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesView : MonoBehaviour, Undoable
{
    public TMP_InputField[] pos;
    public TMP_InputField[] rot;
    public TMP_InputField[] scale;
    public Vector3 oldVector;
    int whichVector = 0;// updated
    // Start is called before the first frame update
    void Start()
    {
        EntityBase.current.OnSelect += OnNewSelect;
        hide();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void show()
    {
        gameObject.SetActive(true);
    }
    public void hide()
    {
        gameObject.SetActive(false);
    }
    public void OnNewSelect()
    {
        if (EntityBase.current.selected == null)
        {
            UpdateVectorInput3(pos);
            UpdateVectorInput3(rot);
            UpdateVectorInput3(scale);
        }
        else
        {
            Transform t = EntityBase.current.selected.transform;
            UpdateVectorInput3(t.position, pos);
            UpdateVectorInput3(t.rotation.eulerAngles, rot);
            UpdateVectorInput3(t.localScale, scale);
        }
    }
    private void UpdateVectorInput3(Vector3 vec, TMP_InputField[] input)
    {
        
        input[0].SetTextWithoutNotify(vec.x.ToString());
        input[1].SetTextWithoutNotify(vec.y.ToString());
        input[2].SetTextWithoutNotify(vec.z.ToString());
    }
    private void UpdateVectorInput3(TMP_InputField[] input)
    {
            foreach (TMP_InputField t in input)
                t.SetTextWithoutNotify("");
    }
    public void changePos()
    {
        if (EntityBase.current.selected == null)
            return;
        oldVector = EntityBase.current.selected.transform.position;
        whichVector = 0;
        EntityBase.current.selected.transform.position = new Vector3(Statics.convertFloat(pos[0].text),
            Statics.convertFloat(pos[1].text), Statics.convertFloat(pos[2].text));
        UndoButton.current.Register(this);
    }
    public void changeRot()
    {
        if (EntityBase.current.selected == null)
            return;
        oldVector = EntityBase.current.selected.transform.rotation.eulerAngles;
        whichVector = 1;
        EntityBase.current.selected.transform.rotation = Quaternion.Euler(new Vector3(Statics.convertFloat(rot[0].text),
            Statics.convertFloat(rot[1].text), Statics.convertFloat(rot[2].text)));
        UndoButton.current.Register(this);
    }
    public void changeScale()
    {
        if (EntityBase.current.selected == null)
            return;
        oldVector = EntityBase.current.selected.transform.localScale;
        whichVector = 2;
        EntityBase.current.selected.transform.localScale = new Vector3(Statics.convertFloat(scale[0].text),
            Statics.convertFloat(scale[1].text), Statics.convertFloat(scale[2].text));
        UndoButton.current.Register(this);
    }


    public void Undo()
    {
        if (EntityBase.current.selected == null)
            return;
        switch (whichVector)
        {
            case 0:
                EntityBase.current.selected.transform.position = oldVector;
                break;
            case 1:
                EntityBase.current.selected.transform.rotation = Quaternion.Euler(oldVector);
                break;
            case 2:
                EntityBase.current.selected.transform.localScale = oldVector;
                break;
        }
    }
}
