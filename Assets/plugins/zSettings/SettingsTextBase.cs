//z2k17

using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UI.Extensions;
//using System.Collections;
//using System.Collections.Generic;

public class SettingsTextBase : SettingsElement
{


    public InputField inputField;



    public System.Action<string> valueChanged;

    string _default;
    public override void resetToDefault()
    {
        OnEndEdit(_default);
        
    }

    public bool numberOnly
    {
        get { return inputField.characterValidation == InputField.CharacterValidation.Decimal; }
        set
        {
            if (value)
                inputField.characterValidation = InputField.CharacterValidation.Decimal;
            else
                inputField.characterValidation = InputField.CharacterValidation.None;
        }

    }

    public bool intOnly
    {
        get { return inputField.characterValidation == InputField.CharacterValidation.Integer; }
        set
        { if (inputField==null) 
            Debug.Log(gameObject.name+" text stting has no inputfield ",gameObject);
        else 
            if (value)
                inputField.characterValidation = InputField.CharacterValidation.Integer;
            else
                inputField.characterValidation = InputField.CharacterValidation.None;
        }

    }

  protected  string lastValue = "0";

   public virtual string defVal
    {
        set
        {
            _default=value;
            if (!PlayerPrefs.HasKey(getID()))
          
            {
                PlayerPrefs.SetString(getID(),value);
                this.value=value;
                OnEndEdit(value);

            }
          
        }

    }
    public virtual string value
    {
        set
        {
            //   text.text
            inputField.text = value;
            lastValue = value;
            OnEndEdit(value);

        }
        get { return inputField.text; }


    }
    public string valueSilent
    {
        set
        {

            inputField.text = value;
            lastValue = value;
        }
        get { return inputField.text; }


    }

    public override void setLabel(string s)
    {
        base.setLabel(s);
        gameObject.name = "Text " + s;

    }
    public void OnEndEdit(string s)
    { 
          if (intOnly)
        {
            try
            {
                System.Int32.Parse(lastValue);
                try
                {
                    System.Int32.Parse(s);
                }
                catch
                {
                    inputField.text = "invalid number";
                }


              
            }
            catch
            {

            }
             
        }
         if (valueChanged != null) 
                
                {
                    valueChanged(s);
                       elementValueChanged();

                }
      
    }


    public override void loadSaved()
    {
    if (PlayerPrefs.HasKey(getID()))
    {
             value = PlayerPrefs.GetString(getID());
            //  OnEndEdit(value); // no needed, assigning value does that
    }

    }

    public override void saveCurrent()
    {
        PlayerPrefs.SetString(getID(),value);
                
    }
    protected override void Awake()
    {
        base.Awake();
    }
}
