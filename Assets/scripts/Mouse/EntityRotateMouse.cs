using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRotateMouse : MonoBehaviour, MouseUser
{
    private Vector3 origin = Vector3.zero;
    public float speed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void disengage()
    {
        mouseUp();
    }

    public void keepDown()
    {
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - origin);
        pos = new Vector3(-pos.y, pos.x, 0); // rotation axis fix
        Camera.main.transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles  +  pos * speed);
        origin = Input.mousePosition;
    }

    public void mouseDown()
    {
        origin = Input.mousePosition;

    }

    public void mouseUp()
    {
    }
}
