//z2k17

using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UI.Extensions;
//using System.Collections;
using System.Collections.Generic;

public class SettingsText : SettingsTextBase
{
    public GameObject expandPresetsButton;
    public Button templateButton;
    LayoutElement layoutEl;
    float myHeight;
    public float buttonSize;

    int numpresets;
    bool expanded;
    public bool autoHidePresets = true;
    List<GameObject> presetButtons;
    public override void Init()
    {
         presetButtons = new List<GameObject>();
       
    }

    protected override void OnValidate()
    {
        base.OnValidate();
             if (inputField==null) inputField=GetComponentInChildren<InputField>();
    }
    protected override void Awake()

    {
        base.Awake();
     //   presetButtons = new List<GameObject>();

        layoutEl = GetComponentInChildren<LayoutElement>();
        myHeight = layoutEl.minHeight;

    }
    public bool presetOnly
    {
        set
        {
            inputField.interactable = !value;
        }
    }

    List<string> presets;
    public void AddPreset(string s)
    {
        if (presets == null) 
        
        {
            presets = new List<string>();
            defVal=s;
        }
        if (presets.Contains(s)) return;
        presets.Add(s);
        expandPresetsButton.SetActive(true);

        Button presetbutton = Instantiate(templateButton, templateButton.transform.parent);
        presetbutton.transform.localPosition = templateButton.transform.localPosition + new Vector3(0, -(numpresets) * buttonSize, 0);
        Text presettext = presetbutton.GetComponentInChildren<Text>();
        presetbutton.gameObject.SetActive(false);
        presettext.text = s;
        presetbutton.onClick.AddListener(() => setPresetText(s));
        if (presetButtons == null) Debug.Log("no presetbutton list", gameObject);
        presetButtons.Add(presetbutton.gameObject);

        numpresets++;
    }
    public override void setLabel(string s)
    {
        base.setLabel(s);
        gameObject.name = "Text " + s;


    }
    public void SetText(string s)
    {
        Debug.Log(s);
    }
    public void setPresetText(string s)
    {
        value = s;
        if (valueChanged != null) valueChanged(s);
        if (autoHidePresets)
            ToggleExpandPresets();
    }

    public void hidePresets()
    {
        if (expanded)
        {
            layoutEl.minHeight = myHeight;
            expanded = false;
            for (int i = 0; i < presetButtons.Count; i++)
                presetButtons[i].SetActive(false);
        }
    }
    public void showPresets()
    {
        layoutEl.minHeight = myHeight + (numpresets + 1.5f) * buttonSize;
        expanded = true;
        for (int i = 0; i < presetButtons.Count; i++)
            presetButtons[i].SetActive(true);
    }

    public void ToggleExpandPresets()
    {
        if (expanded)
            hidePresets();
        else showPresets();
        tab.refreshContentSize();
    }
    public override string value
    {
        set
        {
            if (inputField==null) 
            {
                Debug.Log(gameObject.name+" has no inputfield ",gameObject);
            }
            inputField.text = value;
            lastValue = value;
            OnEndEdit(value);

        }
        get { return inputField.text; }


    }

    public override string defVal
    {
        set
        {
            this.value = value;
            if (presets == null) presets = new List<string>();

            if (!PlayerPrefs.HasKey(getID()))

            {
                PlayerPrefs.SetString(getID(), value);

            }
            if (!presets.Contains(value)) return;
            presets.Add(value);

            OnEndEdit(value);
        }

    }



}
