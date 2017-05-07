using UnityEngine;
using UnityEngine.UI;


public class PosWithinRect : MonoBehaviour
{
    RectTransform rect;
    // public RectTransform parentRect;
    // public RectTransform handleRect;
    public Slider slider;
    public Image image;
    //public uiToolTip toolTip;
    [Range(0, 1)]
    public float value;
    RectTransform sliderRect;
    RectTransform sliderHandleRect;
    float startPos;

    void checkObjectReferences()
    {
        rect = GetComponent<RectTransform>();

        if (slider == null) slider = GetComponentInParent<Slider>();
        if (image == null)
        {
            image = GetComponent<Image>();
            image.enabled = false;
        }

        if (sliderRect == null)
            sliderRect = slider.GetComponent<RectTransform>();
        if (sliderHandleRect == null)
            sliderHandleRect = slider.handleRect;



    }
    void OnValidate()
    {
        checkObjectReferences();
        image.enabled = true;
        setValue(value);

    }


    public void clear()
    {

        image.enabled = false;
    }
    void Awake()
    {
        checkObjectReferences();
        startPos = sliderHandleRect.rect.width / 2-1;

    }

    public void setValue(float f)
    {
        value = f;

        //if (image==null) image=GetComponent<Image>();
        image.enabled = true;
        //if (slider != null)
        f = (f - slider.minValue) / (slider.maxValue - slider.minValue);
if (sliderRect==null || sliderHandleRect ==null) return;
        float travelDistance = sliderRect.rect.width - sliderHandleRect.rect.width + 2;

        if (rect == null) rect = GetComponent<RectTransform>();
//        Debug.Log(travelDistance + "  " + f);
        //rect.anchoredPosition = new Vector3(startPos + width * f, 0, 0);
        rect.anchoredPosition = new Vector3(startPos + travelDistance * f, 0, 0);

    }

}
