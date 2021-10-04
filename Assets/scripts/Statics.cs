using System.Collections;
using UnityEngine;

namespace Assets.scripts
{
    public class Statics : MonoBehaviour
    {
        public static float CameraDistance = 6f;
        public static float CanvasScreenRatio = Statics.CameraDistance * 2 / 1920f;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public static Color select(GameObject select)
        {
            Color oldColor = select.GetComponent<Renderer>().material.GetColor("_Color");
            select.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
            return oldColor;
        }
        public static float convertFloat(string text)
        {
            float number;
            if (float.TryParse(text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.CurrentCulture, out number))
                return number;
            else
                return 0;
        }
    }
}