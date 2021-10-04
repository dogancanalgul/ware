using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
}
