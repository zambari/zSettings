//z2k17

using UnityEngine;
using UnityEngine.UI;

using System;

public class SettingsButton : SettingsElement {

public Action onTrigger;
public Action valueChanged;
public Button button;

public override void OnClick() {

    if (onTrigger!=null) onTrigger();
    if (valueChanged!=null) valueChanged();
 
} 
  public override void setLabel(string s)
    {
        base.setLabel(s);
        gameObject.name = "Button " + s;


    }

	
}
