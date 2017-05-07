///zambari codes unity

using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.UI.Extensions;
//using System.Collections;
//using System.Collections.Generic;

public class ExampleScaleSetting : MonoBehaviour
{
    [Range(0, 3)]
    public float minScale = 0.5f;
    [Range(0, 3)]
    public float maxScale = 1.5f;
    void OnValidate()
    {
        if (maxScale < minScale) maxScale = minScale;

    }
    void OnEnable()
    {
        zSettings.ShowTab("Scales");
    }

    void OnDisable()
    {
        zSettings.HideTab("Scales");
    }
    void Start()
    {
        SettingsSlider thisSlider = zSettings.addSlider(gameObject.name, "Scales");
        thisSlider.setRange(minScale, maxScale);
        thisSlider.valueChanged += setScale;
        thisSlider.defValue = transform.localScale.x;

    }

    void setScale(float f)
    {
        transform.localScale = new Vector3(f, f, f);

    }
}
