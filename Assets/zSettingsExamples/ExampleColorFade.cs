///zambari codes unity

using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.UI.Extensions;
//using System.Collections;
//using System.Collections.Generic;

public class ExampleColorFade : MonoBehaviour {
public Color a=Color.red;
public Color b=Color.blue;
	public string tabName="Colors";
      void OnEnable()
    {
        zSettings.ShowTab(tabName);
    }

    void OnDisable()
    {
        zSettings.HideTab(tabName);  //this won't check if other instances using this setting are still active, use with cautioun
    }
Material m;
   	void Start()
    {
         SettingsSlider thisSlider =   zSettings.addSlider(gameObject.name,tabName);
           thisSlider.setRange(0f, 1);
           thisSlider.defValue=0;
           thisSlider.valueChanged+=(setColor);
           MeshRenderer mr=GetComponent<MeshRenderer>();
           m=mr.material;
    }

    void setColor(float f)
    {
        m.SetColor("_Color",Color.Lerp(a,b,f));
     

    }
}
