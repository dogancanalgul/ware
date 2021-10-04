using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XAxis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        GetComponentInParent<Gizmo>().hold("x");
    }
    private void OnMouseUp()
    {
        GetComponentInParent<Gizmo>().release("x");
    }

}
