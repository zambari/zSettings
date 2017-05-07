//z2k17

using System;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UI.Extensions;
//using System.Collections;
//using System.Collections.Generic;

public class SettingsToggle : SettingsElement {

     public Slider slider;
    public Text valueDisplayText;
    public Action<bool> valueChanged;
    public Image bgImage;
    public Color onColor=new Color(0.8f,0.8f,1,0.4f);
    public Color offColor=new Color(0,0,0,0.5f);
   bool _defVal;
       public override void setLabel(string s)
    {
        base.setLabel(s);
        gameObject.name = "Toggle " + s;

    }

    protected override void OnValidate()
    {
        base.OnValidate();
        if (slider==null) slider=GetComponentInChildren<Slider>();


    }

public override void resetToDefault()
{
 value=_defVal;

}
    public bool defValue{
        set {
            
             _defVal=value;
             if (!PlayerPrefs.HasKey(getID()))
             {
                 value=_defVal;
                 saveCurrent();

             }
        
        }
        get {return _defVal;}


    }

    /// <summary>
    /// If you don't want to provide your scaler function, you can specify min and max
    /// </summary>
    /// <param name="newMin"></param>
    /// <param name="newMax"></param>


    //public float getValue { get { return (scaler==null ? slider.value: scaler(slider.value)); }
    public bool  value
    {
        get { return slider.value == 1; }
        set {
             slider.value=(value?1:0);
             sliderValueChanged(slider.value);
         }
    }
    public override void loadSaved()
    {
    if (PlayerPrefs.HasKey(getID()))
    {
                string s = PlayerPrefs.GetString(getID());
                if (s == "true" ) value = true;
                else value=false;
    }

    }

    public override void saveCurrent()
    {
        PlayerPrefs.SetString(getID(),(value?"true":"false"));
                
    }
    public void sliderValueChanged(float v)
    {
          if (valueChanged != null)
            valueChanged.Invoke(v==1);

        if (valueDisplayText != null)
        {
            valueDisplayText.text = (v==1?"ON":"OFF");
        }
        elementValueChanged();
        if (bgImage!=null) 
        {
            bgImage.color=(value?onColor:offColor);
        }
    }

}
