using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManipMenu : MonoBehaviour
{
    public enum View{
        Properties, Animations
    };
    // Start is called before the first frame update
    public PropertiesView propertiesView;
    public AnimationsView animationsView;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void newActive(View type)
    {
        //TODO a better system that can be scaled to many views.
        switch (type)
        {
            case View.Properties:
                if (propertiesView.isActiveAndEnabled)
                    propertiesView.hide();
                else
                {
                    animationsView.hide();
                    propertiesView.show();
                }
                break;

            case View.Animations:
                if (animationsView.isActiveAndEnabled)
                    animationsView.hide();
                else
                {
                    animationsView.show();
                    propertiesView.hide();
                }
                break;
        }
    }
}
