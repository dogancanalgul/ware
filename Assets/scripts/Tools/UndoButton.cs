using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoButton : MonoBehaviour
{
    public static UndoButton current;
    private Undoable lastAction = null;
    // Start is called before the first frame update
    private void Awake()
    {
        current = this;
    }
    void Start()
    {
    }

    void Update()
    {
        
    }
    public void Register(Undoable undoable)
    {
        KillButton killButton = lastAction as KillButton;
        if (killButton != null)
            killButton.noRessurection();
        lastAction = undoable;
    }
    public void Undo()
    {
        Debug.Log("Undo Call" + lastAction);
        if (null == lastAction)
            return;
        else
        {
            lastAction.Undo();
            lastAction = null;
        }
    }
}
