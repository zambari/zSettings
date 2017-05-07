//z2k17

using UnityEngine;
using UnityEngine.UI;


public class SettingsLabel : SettingsElement {

public Color labelBGColor=new Color(0.7f,0.7f,1,0.4f);
// inherited setLabel(string s)
public string value 
{
    set { setLabel(value); }
}

	
    protected override void OnValidate()
    {

        setColor();

    }
 
    public override void setColor()
    {

        
        if (image == null) image = GetComponentInChildren<Image>();
        if (image != null )

                image.color = labelBGColor;
        
    }
       public override void setLabel(string s)
    {
        base.setLabel(s);
        gameObject.name = "Label " + s;


    }

}
