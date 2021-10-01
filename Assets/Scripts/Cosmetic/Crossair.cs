using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossair : MonoBehaviour
{
    //sorry dit script is wel monobehaviour maar het is puur cosmetisch
    //waarom voeg je ik het dan toe vraag je je af?
    //geen idee ik stel verkeerde prioriteiten

    public Canvas myCanvas;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);
    }
}
