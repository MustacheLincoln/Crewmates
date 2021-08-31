using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigatorNode : MonoBehaviour
{
    public Image background;
    public Image icon;
    public Image radialProgress;

    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        background.transform.position = pos;
        icon.transform.position = pos;
        radialProgress.transform.position = pos;
        bool isPos = (pos.z >= 0);
        background.enabled = isPos;
        icon.enabled = isPos;
        radialProgress.enabled = isPos;

    }
}
