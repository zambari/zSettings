///zambari codes unity


// this class is boilerplate code repository that evolved arout the concept of managing list of items (in the UI)
// 

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class zNodeController : MonoBehaviour

{
    [SerializeField]
    public List<zNode> nodeTemplatePool;
    public Color nonHoveredColor = new Color(1, 1, 1, 0.2f);
    public Color hoveredColor = new Color(1, 1, 1, 0.4f);
    public Color activeColor = new Color(1, 0, 0, 0.3f);
    public Color panelEditColor = new Color(.5f, 0, 0, 0.1f);
    protected List<zNode> nodes;
    protected Image image;
    protected Text text;
    protected RectTransform content;
    protected RectTransform contentMaskRect;
    protected GameObject templatePoolGO;
    [SerializeField]
    [HideInInspector]
    protected zScrollRect scrollRect;
    protected Canvas canvas;

    protected RectTransform rect;
    protected int activeNodeIndex;

    [Header(" make protected ")]

    //    protected Vector2 startDrag;
    //  protected Vector2 startSize;

    //  protected bool scrollStateDirty;
    [SerializeField]
    Dictionary<string, zNode> templateDict;
    public Action OnNodeAdded;

    //  bool _scrolldir;
    /*   get { return scrollBar.direction == Scrollbar.Direction.BottomToTop; }
       set
       {
           if (value) scrollBar.direction = Scrollbar.Direction.BottomToTop;
           else scrollBar.direction = Scrollbar.Direction.TopToBottom;
       }*/

    [HideInInspector]
    public bool isHidden;

    #region remoteNavigation
    public virtual void OnUp()
    {
        if (isHidden) return;
        if (activeNodeIndex < 0 || activeNodeIndex >= nodes.Count) activeNodeIndex = 0;
        nodes[activeNodeIndex].setAsActive(false);
        int startIndex = activeNodeIndex;

        do
        {
            activeNodeIndex--;
            if (activeNodeIndex < 0) activeNodeIndex = nodes.Count - 1;

        } while (activeNodeIndex != startIndex && !nodes[activeNodeIndex].gameObject.activeInHierarchy);

        nodes[activeNodeIndex].setAsActive(true);
        scrollToActive();


    }
    public virtual void OnDown()
    {
        if (isHidden) { Debug.Log("ishidden"); return; }
        if (activeNodeIndex < 0 || activeNodeIndex >= nodes.Count) activeNodeIndex = 0;
        nodes[activeNodeIndex].setAsActive(false);
        int startIndex = activeNodeIndex;
        do
        {
            activeNodeIndex++;
            if (activeNodeIndex >= nodes.Count) activeNodeIndex = 0;
        } while (activeNodeIndex != startIndex && !nodes[activeNodeIndex].gameObject.activeInHierarchy);
        nodes[activeNodeIndex].setAsActive(true);
        scrollToActive();


    }
    public virtual void GoParent()
    {
    }
    public virtual void OnLooseFocus()
    {
        canvas.enabled = false;
        isHidden = true;
    }
    public virtual void OnLeft()
    {
        if (isHidden) return;
        if (activeNodeIndex == 0)
            GoParent();
        else
        {
            nodes[activeNodeIndex].setAsActive(false);
            activeNodeIndex = 0;
            nodes[activeNodeIndex].setAsActive(true);
        }
        scrollToActive();
    }
    public virtual void OnRight()
    {
        if (isHidden) return;
        nodes[activeNodeIndex].setAsActive(false);
        activeNodeIndex = nodes.Count - 1;
        nodes[activeNodeIndex].setAsActive(true);
        scrollToActive();
    }
    public virtual void OnEnter()
    {
        if (isHidden) return;
        //        FileNode f = nodes[activeNodeIndex] as FileNode;
        //    bool isFile = f.type == FileNode.NodeTypes.file;
        //    nodes[activeNodeIndex].OnClick();
        //    if (isFile)
        //        OnLooseFocus();
    }

    public virtual void OnEscape()
    {
        canvas.enabled = !canvas.enabled;
        isHidden = !canvas.enabled;
    }

    public virtual void OnFocus()
    {
        canvas.enabled = true;
        isHidden = false;
    }
    #endregion

    public zNode AddNodeFromTemplate()
    {
        return AddNode("node", nodeTemplatePool[0]);
    }
    public zNode AddNode(string nodeName, string templateName)
    {
        return AddNode(nodeName, getTemplate(templateName));
    }

    public zNode AddNode(string nodeName, zNode templateNode)
    {
        zNode newNode = Instantiate(templateNode, content);
        newNode.setLabel(nodeName);
        newNode.gameObject.SetActive(true);
        if (OnNodeAdded != null)
            OnNodeAdded();
        nodes.Add(newNode);
        return newNode;

    }
    public zNode AddNode(string nodeName)
    {
        return AddNode(nodeName, nodeTemplatePool[0]);
    }

    /* 
        void _onNewNode()
        {
            scrollStateDirty = true;
        }*/

    public virtual void Clear()
    {
        for (int i = nodes.Count - 1; i >= 0; i--)
        {  //  Debug.Log("removing "+i);
            zNode thisNode = nodes[i];
            nodes.Remove(thisNode);
            Destroy(thisNode.gameObject);

        }

    }

    protected bool createTemplateDictionary()
    {
        if (nodeTemplatePool == null) Debug.Log(gameObject.name + " has no templates, trying to Add", gameObject);
        else
        if (nodeTemplatePool.Count == 0) Debug.Log(gameObject.name + " has  template Count==0, trying to Add", gameObject);
        else
        if (nodeTemplatePool[0] == null) Debug.Log(gameObject.name + " templateePool[0]==null, trying to Add", gameObject);
        if (nodeTemplatePool == null || (nodeTemplatePool.Count == 0) || (nodeTemplatePool[0] == null))
        {
            nodeTemplatePool = new List<zNode>();
            zNode[] nodeComponents = GetComponentsInChildren<zNode>();
            Debug.Log(gameObject.name + "template add started " + nodeComponents.Length + " components found");
            foreach (zNode thisNode in nodeComponents)
                if (!String.IsNullOrEmpty(thisNode.getTemplateName()))
                    nodeTemplatePool.Add(thisNode);
                else Debug.Log("non template node ? '" + thisNode.getTemplateName() + "'", thisNode.gameObject);
        }

        templateDict = new Dictionary<string, zNode>();
        for (int i = 0; i < nodeTemplatePool.Count; i++)
            if (nodeTemplatePool[i] != null &&
                nodeTemplatePool[i].getTemplateName() != null)
                if (!templateDict.ContainsKey(nodeTemplatePool[i].getTemplateName()))
                    templateDict.Add(nodeTemplatePool[i].getTemplateName(), nodeTemplatePool[i]);

        if (nodeTemplatePool == null || nodeTemplatePool.Count == 0 || nodeTemplatePool[0] == null)
        {
            Debug.Log(gameObject.name + " has templatebool or object 0 is null", gameObject);
            return false;

        }
        zNode t = nodeTemplatePool[nodeTemplatePool.Count-1];
        if (t.transform.parent == null) Debug.Log("no parent? wtf", gameObject);
        templatePoolGO = t.transform.parent.gameObject;
        if (templatePoolGO == null) Debug.Log(gameObject.name + " has no template pool", gameObject);
        else
            return true;
        return false;
    }

    public zNode getTemplate(string n)
    {
        if (templateDict == null)
            createTemplateDictionary();
        zNode t;
        if (templateDict.TryGetValue(n, out t))
            return t;
        else
        {
            Debug.Log(gameObject.name + " unknown template " + n + " dictionary has " + templateDict.Count + " entries", gameObject);
            foreach (string s in templateDict.Keys) Debug.Log(s);
        }
        return t;
    }
    protected virtual void scrollToActive()
    {
        /*  if (scrollAmount != 0 && activeNodeIndex > 0 && activeNodeIndex < nodes.Count)
          {
              if (nodes.Count > 1)
              {
                  float newScroll = activeNodeIndex * 1f / (nodes.Count - 1);
                  scrollBar.value = newScroll;
                  scrollContentSlider(newScroll);

              }

          }*/
    }
    public virtual void newNodeHovered(zNode hoveredNode)
    {
        /* 
         int nodeIndex = -1;
         for (int i = 0; i < nodes.Count; i++)
             if (nodes[i] == hoveredNode) nodeIndex = i;
         if (nodeIndex != -1)
         {
             if (activeNodeIndex >= 0 && activeNodeIndex < nodes.Count)
                 nodes[activeNodeIndex].setAsActive(false);
             activeNodeIndex = nodeIndex;
             nodes[activeNodeIndex].setAsActive(true);
         }*/
    }

    public virtual void activeNodeClicked()
    {
        if (activeNodeIndex >= 0 && activeNodeIndex < nodes.Count)
            nodes[activeNodeIndex].OnClick();
    }
    public virtual void nodeValueChanged()
    {

    }
    public bool activeNodePresent()
    {
        if (activeNodeIndex >= 0 && activeNodeIndex < nodes.Count && nodes[activeNodeIndex].gameObject.activeInHierarchy) return true;
        return false;
    }
    public virtual void setHeight(float f) // size
    {
        if (nodes == null) return;
        for (int i = 0; i < nodes.Count; i++)
            nodes[i].setHeight(f);

    }

    public virtual void NodeClicked(zNode node)
    {

    }


    Color savedColor = new Color(0, 0, 0, 0);
    bool disableImage;
    public virtual void highlightPanel(BaseEventData eventData)
    {
        if (image != null)
        {
            if (image.enabled == false) disableImage = true; ;
            image.enabled = true;
            savedColor = image.color;
            image.color = panelEditColor;
        }
    }
    public virtual void restoreColor(BaseEventData eventData)
    {
        if (image != null)
        {
            if (disableImage) image.enabled = false;
            else
                image.color = savedColor;
        }
    }

    protected virtual void OnValidate()
    {
         if (text == null) text = GetComponentInChildren<Text>();

        if (enabled && gameObject.activeInHierarchy)
            createTemplateDictionary();
        if (scrollRect == null) 
        {  scrollRect = GetComponent<zScrollRect>();
            scrollRect.verticalScrollbar.direction=Scrollbar.Direction.BottomToTop;
             scrollRect.verticalScrollbarVisibility=ScrollRect.ScrollbarVisibility.AutoHide;
        }
        if (scrollRect == null) 
        {
              gameObject.AddComponent<zScrollRect>();
             scrollRect.verticalScrollbar.direction=Scrollbar.Direction.BottomToTop;
             scrollRect.verticalScrollbarVisibility=ScrollRect.ScrollbarVisibility.AutoHide;
        }
    }

    protected virtual void Awake()
    {
        nodes = new List<zNode>();
        image = GetComponent<Image>();
        canvas = GetComponentInParent<Canvas>();

        rect = GetComponent<RectTransform>();
        createTemplateDictionary();

        if (templatePoolGO != null)
        {
            GameObject contentGO = Instantiate(templatePoolGO, templatePoolGO.transform.parent);
            content = contentGO.GetComponent<RectTransform>();
            scrollRect.content = content;
            for (int i = content.transform.childCount - 1; i >= 0; i--)
                DestroyImmediate(content.transform.GetChild(i).gameObject);
            content.name = "CONTENT";

            Mask m = content.GetComponentInParent<Mask>();
            if (m == null) Debug.Log("no mask");
            else
                contentMaskRect = m.GetComponent<RectTransform>();
            templatePoolGO.SetActive(false);
        }
        else Debug.Log("no template pool");

    }

    protected virtual void Start()
    {

    }


}
