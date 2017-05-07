//z2k17

using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.UI.Extensions;
//using System.Collections;
//using System.Collections.Generic;

public class ExampleSettingsRotat : MonoBehaviour
{
    public enum XYZ { x, y, z }
    public XYZ xyz=XYZ.z;
    [Range(-360, 360)]
    public float min = -180;
    [Range(-360, 360)]
    public float max = +180;
    [Range(0, 1)]
    public float setAngleTest;
    public string tabName="Robot";
      void OnEnable()
    {
        zSettings.ShowTab(tabName);
    }

    void OnDisable()
    {
        zSettings.HideTab(tabName);  //this won't check if other instances using this setting are still active, use with cautioun
    }
    void Start()
    {
        SettingsSlider mySlider=zSettings.addSlider(name,tabName);
        mySlider.valueChanged+=setAngle;
        mySlider.defValue=setAngleTest;
    }

    void OnValidate()
    {
       setAngle(setAngleTest);
    }

    void setAngle(float t)
    {
        float angle = t * (max - min) - min;
        if (xyz == XYZ.x)
            transform.localRotation = Quaternion.Euler(angle, 0, 0);
        else
        if (xyz == XYZ.y)
            transform.localRotation = Quaternion.Euler(0, angle, 0);
        else
        if (xyz == XYZ.z)
            transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
 
}
