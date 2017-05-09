using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsTab : SettingsElement
{
    [Header("set to avoid componentgets")]
    public Button button;



    //   public Scrollbar scrollbar;
    //  Canvas canvas;
    //public RectTransform maskRect;
    [Header("Filled at runtime")]
    public GameObject content;
    public RectTransform contentRect;
    //  public float scrollAmount;
    public void linkToTab(GameObject tab)
    {
        if (tab == null) Debug.Log("null tab");
        content = tab;
        contentRect = tab.GetComponent<RectTransform>();

        //contentMask=tab
    }
    void OnApplicationQuit()
    {
        quit=true;
    }
    bool quit;
  
    public void Hide()
    {
if (!quit)
        gameObject.SetActive(false);
    }
    public void Show()
    {// if (gameObject!=null)
        gameObject.SetActive(true);
    }
    
    public GameObject getContent()
    {
        return content;
        //contentMask=tab
    }
    public string tabName { get { return gameObject.name; } set { gameObject.name = value; } }
    public override void setLabel(string n)
    {
        gameObject.name = "TAB " + n;
            if (text==null) Debug.Log("no text",gameObject); else
        text.text = n;
    }
    protected override void Awake()
    {
        base.Awake();
        if (settingsPanel == null) settingsPanel = GetComponentInParent<zSettings>();
        if (text == null) text = GetComponentInChildren<Text>();
        if (button == null) button = GetComponent<Button>();

    }
    public override void OnClick()
    {
        activatePanel();
    }
    public void activatePanel()
    {

        if (content == null) return;
        if (settingsPanel.activeTab != null) settingsPanel.activeTab.deactivateTab();
        content.SetActive(true);
        settingsPanel.newActiveContent(content);
        refreshContentSize();
        settingsPanel.activeTab = this;
        button.interactable = false;
        

    }

    public void refreshContentSize()
    {
        Canvas.ForceUpdateCanvases();
  
    }
    public void deactivateTab()
    {
        content.SetActive(false);
        button.interactable = true;


    }

}
