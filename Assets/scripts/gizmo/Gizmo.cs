using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    public EntityMouse mouse;
    public GameObject canvas;
    private bool[] pressingAxis = new bool[] { false, false, false };
    private Vector3 offsetCenter = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        EntityBase.current.OnSelect += OnSelect;
        gameObject.SetActive(false);
        mouse.OnChange += OnMouseChange;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        if (null == EntityBase.current.selected)
            return;
        updateAxisMoves();
        updatePosition();
    }
    private void updatePosition()
    {
        Plane plane = new Plane(Camera.main.transform.forward, transform.position); // ground plane

        Ray ray = new Ray(EntityBase.current.selected.transform.position, Camera.main.transform.position
            - EntityBase.current.selected.transform.position);
        
        float distance; // the distance from the ray origin to the ray intersection of the plane
        plane.Raycast(ray, out distance);
        transform.position = ray.GetPoint(distance); // distance along the ray
    }
    private void updateAxisMoves()
    {
        // Dot product of right axis and then move along the right axis that much!
        if (pressingAxis[0])
        {
            EntityBase.current.selected.transform.position = new Vector3( axisPlanePosition().x,
                EntityBase.current.selected.transform.position.y, EntityBase.current.selected.transform.position.z);
        }
        else if (pressingAxis[1])
        {
            EntityBase.current.selected.transform.position = new Vector3( 
                EntityBase.current.selected.transform.position.x,
                axisPlanePosition().y,EntityBase.current.selected.transform.position.z);
        }
        else if (pressingAxis[2]) 
        {
            EntityBase.current.selected.transform.position = new Vector3( 
                EntityBase.current.selected.transform.position.x, 
                EntityBase.current.selected.transform.position.y, axisPlanePosition().z);
        }
    }
    private Vector3 axisPlanePosition()
    { 
        Plane plane = new Plane(Camera.main.transform.forward,
        EntityBase.current.selected.transform.position); //plane

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance; // the distance from the ray origin to the ray intersection of the plane
        plane.Raycast(ray, out distance);
        Vector3 pos = ray.GetPoint(distance);
        if (offsetCenter.magnitude == 0)
            offsetCenter = pos - EntityBase.current.selected.transform.position;
        pos = pos - offsetCenter;
        return pos;
    }
    public void hold(string type)
    {
        change(type, true);
    }
    public void release(string type)
    {
        change(type, false);
        offsetCenter = Vector3.zero;
    }

    public void change(string type, bool value)
    {
        switch (type)
        {
            case "x":
                pressingAxis[0] = value;
                break;
            case "y":
                pressingAxis[1] = value;
                break;
            case "z":
                pressingAxis[2] = value;
                break;
        }
    }
    public void OnSelect()
    {
        if(null == EntityBase.current.selected)
        {
            gameObject.transform.
            gameObject.SetActive(false);
            return;
        }
        //
        gameObject.SetActive(true);

    }
    public void OnMouseChange()
    {
        if (!(mouse.mouseUser is EntitySelectMouse))
        {
            gameObject.SetActive(false);
            offsetCenter = Vector3.zero;
        }
        else if(null != EntityBase.current.selected)
            gameObject.SetActive(true);
    }
}
