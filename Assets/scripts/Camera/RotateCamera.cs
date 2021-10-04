using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public GameObject upAxes;
    public GameObject downAxes;
    public GameObject leftAxes;
    public GameObject rightAxes;
    private float rotateAmount = 0;
    private float rotationSpeed = 180f;
    private Vector3 axis = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        upAxes.GetComponent<Renderer>().material.color = Color.green;
        rightAxes.GetComponent<Renderer>().material.color = Color.red;
        Camera.main.transform.LookAt(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {

        if (0 != rotateAmount)
        {
            if(rotateAmount - rotationSpeed * Time.deltaTime > 0)
            {
                Camera.main.transform.RotateAround(Vector3.zero, axis, rotationSpeed * Time.deltaTime);
                rotateAmount -= rotationSpeed * Time.deltaTime;
            }
            else
            {
                Camera.main.transform.RotateAround(Vector3.zero, axis, rotateAmount);
                rotateAmount = 0;
                clampCamera();
                
            }
        }
    }
    private void clampCamera()
    {
        int sign = -1;
        Vector3 fwd = Camera.main.transform.forward;

        if (fwd.x + fwd.y + fwd.z < 0)
            sign = 1;

        if (Mathf.Abs(fwd.x) > Vector3.kEpsilon)
            Camera.main.transform.position = new Vector3(sign*Statics.CameraDistance, 0, 0);
        else if(Mathf.Abs(fwd.y) > Vector3.kEpsilon)
            Camera.main.transform.position = new Vector3(0, sign*Statics.CameraDistance, 0);
        else
            Camera.main.transform.position = new Vector3(0, 0, sign * Statics.CameraDistance);
        Camera.main.transform.LookAt(Vector3.zero, Camera.main.transform.up);
    }
    public void clickUpAxes()
    {
        if (0 != rotateAmount)
            return;
        axis = Camera.main.transform.right;
        rotateAmount =  90;
    }

    public void clickDownAxes()
    {
        if (0 != rotateAmount)
            return;
        axis = -Camera.main.transform.right;
        rotateAmount = 90;
    }
    public void clickLeftAxes()
    {
        if (0 != rotateAmount)
            return; 
        axis = Camera.main.transform.up;
        rotateAmount = 90;
    }
    public void clickRightAxes()
    {
        if (0 != rotateAmount)
            return; 
        axis = -Camera.main.transform.up;
        rotateAmount = 90;
    }


};