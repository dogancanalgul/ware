using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillButton : MonoBehaviour, Undoable
{
    GameObject killObject = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void kill()
    {
        if(EntityBase.current.selected != null)
        {
            killObject = EntityBase.current.selected;
            EntityBase.current.select(null);
            //TODO REDO UNDO
            noRessurection();
            //killObject.SetActive(false);
            //UndoButton.current.Register(this);
        }
    }
    public void Undo()
    {
        killObject.SetActive(true);
    }
    internal void noRessurection()
    {
        EntityBase.current.entities.Remove(killObject);
        GameObject.Destroy(killObject);
        killObject = null;
    }
}
