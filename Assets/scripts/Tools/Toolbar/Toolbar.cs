using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    private SpriteRenderer oldRenderer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void newActive()
    {
        foreach(SpriteRenderer r in GetComponentsInChildren<SpriteRenderer>())
        {
            r.color = Color.grey;
        }
    }
    public void newActive(bool click)
    {
        if (click) { 
            foreach (SpriteRenderer r in GetComponentsInChildren<SpriteRenderer>())
            {
                if (r.color != Color.grey)
                    oldRenderer = r;
                r.color = Color.grey;
            }
        }
        else if(oldRenderer != null)
            oldRenderer.color = Color.green;
    }
}
