using UnityEngine;
using System;
using UnityEngine.UI;

//public delegate float SettingScaler(float f);

public class SettingsSlider : SettingsElement
{
    // public SetText valueDisplay;

    public Slider slider;
    //public Text valueDisplayText;
    public InputField inputField;

    public RectTransform valuedDisplayTransform;
    public PosWithinRect showValueDefault;
    public PosWithinRect showValueSaved;

    public Action<float> valueChanged;
    float _defVal = -1000;

    [Range(0, 1.5f)]
    public float InputFieldOffset = 0.7f;
    public override void resetToDefault()
    {
        if (_defVal != -1000)
        {
            sliderValueChanged(_defVal);
        }
        //sho.clear();
        showValueSaved.clear();

    }
    public void InputValueChanged(string s)
    {
        float f = Single.Parse(s);
        slider.value = f;


    }
    protected override void OnValidate()
    {
        base.OnValidate();

        if (slider == null) GetComponentInChildren<Slider>();
        if (inputField == null) inputField = GetComponentInChildren<InputField>();
        valuedDisplayTransform = inputField.GetComponent<RectTransform>();
        if (valuedDisplayTransform != null)
            valuedDisplayTransform.pivot = new Vector2(-InputFieldOffset, 0.5f);
        if (showValueSaved == null)
        {
            Transform t = transform.Find("SavedVal");
            if (t != null)
                showValueSaved = t.GetComponent<PosWithinRect>();
        }
        if (showValueDefault == null)

        {
            Transform t = transform.Find("DefValue");
            if (t != null)
                showValueDefault = t.GetComponent<PosWithinRect>();
        }

    }
    public bool disabled
    {
        get
        {
            return !slider.interactable;
        }
        set
        {
            if (value == true)
            {
                if (slider.interactable)
                {
                    slider.interactable = false;
                    Image i = slider.GetComponentInChildren<Image>();
                    if (i != null)
                        i.color = i.color * 0.5f;
                }

            }
            else

                if (!slider.interactable)
            {
                slider.interactable = true;

                Image i = slider.GetComponentInChildren<Image>();
                if (i != null)
                    i.color = i.color * 2f;
            }

        }
    }


    float halfOfRange = 0.5f;
    public float defValue
    {
        get { return _defVal; }
        set
        {
            _defVal = value;
            {
                if (showValueDefault != null)
                    showValueDefault.setValue(value);
                this.value = value;
                if (!PlayerPrefs.HasKey(getID())) saveCurrent();
            }

        }

    }
    public override void setLabel(string s)
    {
        base.setLabel(s);
        gameObject.name = "Slider " + s;

    }

    float halfOfSliderRange()
    {
        return (slider.maxValue + slider.minValue) / 2;

    }
    public float max
    {

        get { return slider.maxValue; }
        set
        {
            slider.maxValue = max;
            halfOfRange = halfOfSliderRange();
        }
    }
    public float min
    {

        get { return slider.minValue; }
        set
        {
            slider.minValue = min;
            halfOfRange = halfOfSliderRange();
        }
    }

    /// <summary>
    /// If you don't want to provide your scaler function, you can specify min and max
    /// </summary>
    /// <param name="newMin"></param>
    /// <param name="newMax"></param>

    public void setRange(float newMin, float newMax)
    {
        slider.minValue = newMin;
        slider.maxValue = newMax;
        halfOfRange = halfOfSliderRange();

    }
    //public float getValue { get { return (scaler==null ? slider.value: scaler(slider.value)); }
    public float value
    {
        get
        {
            return slider.value;
        }
        set
        {
            slider.value = value;
            sliderValueChanged(value);
        }
    }


    public void sliderValueChanged(float v)
    {

        if (inputField != null) inputField.text = (Mathf.Round(v * 100) / 100).ToString();

        if (valueChanged != null)
            valueChanged.Invoke(v);


        if (valuedDisplayTransform != null)

            if (v < halfOfRange)
            {
                valuedDisplayTransform.pivot = new Vector2(-InputFieldOffset, 0.5f);
            }
            else
                valuedDisplayTransform.pivot = new Vector2(1 + InputFieldOffset, 0.5f);

        elementValueChanged();
    }

    public override void loadSaved()
    {
        if (PlayerPrefs.HasKey(getID()))
        {
            value = PlayerPrefs.GetFloat(getID());
            if (showValueSaved != null)
                showValueSaved.setValue(value);
            slider.value = value;
            sliderValueChanged(value);
        }

    }
    public override void saveCurrent()
    {

        PlayerPrefs.SetFloat(getID(), value);
        if (showValueSaved != null)
            showValueSaved.setValue(value);
    }


}
