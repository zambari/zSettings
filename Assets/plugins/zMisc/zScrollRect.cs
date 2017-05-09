
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class zScrollRect : ScrollRect, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
public bool enableDragging;
    protected override void OnValidate()
    {
        base.OnValidate();
		 checkReferecnes();
		// if (enableScroll)

    }
    [HideInInspector]
    public bool reversedScroll; //not implemented
	public bool reverseMouseScroll;
    public override void OnBeginDrag(PointerEventData eventData) {if (enableDragging) base.OnBeginDrag(eventData); }
    public override void OnDrag(PointerEventData eventData) {if (enableDragging) base.OnDrag(eventData); }
    public override void OnEndDrag(PointerEventData eventData) {if (enableDragging) base.OnEndDrag(eventData); }
	bool enableScroll;

	protected override void OnEnable()
	{
		base.OnEnable();
		checkReferecnes();
	}
    void checkReferecnes()
    {
        if (verticalScrollbar == null) 
        {
            verticalScrollbar = GetComponentInChildren<Scrollbar>();
        
        }
         if (content == null)
        {
            var t = transform.FindChild("CONTENT");
            if (t != null)
       
                content = t.gameObject.GetComponent<RectTransform>();
   
        }
        if (viewport == null)
        {
            var m = GetComponentInChildren<Mask>();
            if (m != null) viewport = m.GetComponent<RectTransform>();
       
        }
        if (viewport != null && content == null)
            if (viewport.transform.childCount > 0)
                content = viewport.transform.GetChild(0).GetComponent<RectTransform>();

    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        enableScroll = true;
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        enableScroll = false;
    }

	
    protected virtual void Update()
    {
        if (enableScroll)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
			   { 
				   if (reverseMouseScroll) scroll*=-1;
					verticalScrollbar.value+=scroll;
			    }
               
        }
    }
}

