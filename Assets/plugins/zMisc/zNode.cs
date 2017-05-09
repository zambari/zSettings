//z2k17

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.UI.Extensions;
//using System.Collections;
//using System.Collections.Generic;

public class zNode : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    protected zNodeController controller;
    public string nodeName;
    public Text text;
    protected LayoutElement layoutElement;
    protected Image image;
    protected bool isActive;
    protected bool isHovered;
    protected RectTransform myRect;
    public bool isDisabled;

    public bool activateOnHover=false;


    public string getTemplateName()
    {
    if (name.Contains("{") && name.Contains("}"))
        {
            string templateName=(name.Substring(1, name.Length - 2));
              char[] a = templateName.ToCharArray();
              a[0] = char.ToUpper(a[0]);
              templateName=new string(a);
              gameObject.name="{"+templateName+"}";
            return templateName;
        }

        return null;

    }

    protected virtual void OnValidate()
    {

        setColor();
        if (image == null)
            image = GetComponentInChildren<Image>();

        if (text==null)
            text=GetComponentInChildren<Text>();

     /*   if (name.Contains("{"))
        {

            if (string.IsNullOrEmpty(templateName))
            {
                templateName = name.Substring(1, name.Length - 2);
            }
            Debug.Log("template found " + templateName, gameObject);
            zNodeController controller=GetComponentInParent<zNodeController>();
            if (controller!=null) 
            { 
                if controller.nodeTemplatePool)
            }


        }*/
        
    }

    public virtual void setColor()
    {
        //if (customColor) return;
        if (controller == null) controller = GetComponentInParent<zNodeController>();
        if (image == null) image = GetComponentInChildren<Image>();
        if (image != null && controller != null)
        {
            if (isActive)
                image.color = controller.activeColor;
            else
           if (isHovered) image.color = controller.hoveredColor;
            else image.color = controller.nonHoveredColor;
        }
    }
    public virtual void setLabel(string s)
    {
        
        if (text != null) text.text = s;
        nodeName = s;
        name="<"+nodeName+">";

    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button != 0) return;
        OnClick();
    }


    public virtual void setAsActive(bool a)
    {
        isActive = a;
        setColor();
    }
    public virtual void OnClick()
    {
        if (controller!=null) controller.NodeClicked(this);
    }
    public void setHeight(float f)
    {
        if (layoutElement == null) layoutElement = GetComponent<LayoutElement>();
        layoutElement.preferredHeight = f;

    }
    protected virtual void Start()
    {
        setColor();
    }
    protected virtual void Awake()
    {
        if (image == null) image = GetComponentInChildren<Image>();
        myRect = GetComponent<RectTransform>();
      //  controller = GetComponentInParent<zNodeController>();
        if (text == null) text = GetComponentInChildren<Text>();

    
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (isDisabled) return;
        isHovered = true;
        setColor();

        eventData.Use();
        if (controller!=null) controller.newNodeHovered(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDisabled) return;
        isHovered = false;
        setColor();

        eventData.Use();
    }


    public virtual void setLabelToNodeName()
    {
        setLabel(nodeName);
        
    }
}
