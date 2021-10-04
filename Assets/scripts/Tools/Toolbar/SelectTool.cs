using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTool : MonoBehaviour
{
    public EntityMouse entityMouse;
    public EntitySelectMouse entitySelectMouse;

    // Start is called before the first frame update
    void Start()
    {
        entityMouse.changeUser(entitySelectMouse);
        GetComponentInChildren<SpriteRenderer>().color = Color.green;
    }

    void Update()
    {
        
    }
    public void OnClick()
    {
        if (GetComponentInChildren<SpriteRenderer>().color == Color.grey)
        {
            entityMouse.changeUser(entitySelectMouse);
            GetComponentInParent<Toolbar>().newActive();
            GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }
    }
}
