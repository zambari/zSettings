using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;


public class zSettings : zNodeController
{
    public static zSettings instance;
    [HideInInspector]
    public SettingsTab activeTab;
    public bool autoSave;
    List<SettingsElement> settingElements;
    Dictionary<string, SettingsTab> tabDict;
    Dictionary<string, SettingsElement> elementDict;
    List<SettingsElement> lastAdded;
    bool started;
    public LayoutElement headerLayoutElement;
    public Image editFrame;
    public static void HideTab(string tabName)
    {
        SettingsTab tab = null;
        if (instance.tabDict.TryGetValue(tabName, out tab))
        {
            tab.Hide();
            if (instance.activeTab == tab)
                Debug.Log("oops, hidden active tab");
        }

    }
    public static void ShowTab(string tabName)
    {
        SettingsTab tab = null;
        if (instance.tabDict.TryGetValue(tabName, out tab))
            tab.Show();

    }
    SettingsTab AddTab(string tabName)
    {
        zNode n = getTemplate("Tab");
        if (n == null) { Debug.Log(" no template tab", gameObject); }
        GameObject template = n.gameObject;
        GameObject newTabButton = Instantiate(template, template.transform.parent);
        newTabButton.SetActive(true);
        newTabButton.transform.localScale = template.transform.localScale;
        newTabButton.transform.SetAsFirstSibling();
        SettingsTab thisTab = newTabButton.GetComponent<SettingsTab>();
        thisTab.setLabel(tabName);
        GameObject newTabContent = Instantiate(content.gameObject, content.parent);
        for (int i = newTabContent.transform.childCount - 1; i >= 0; i--)
            Destroy(newTabContent.transform.GetChild(i).gameObject); // we need to make sure new tab is empty unfortunately
        newTabContent.SetActive(true);
        newTabContent.name = "CONTENT " + tabName;
        thisTab.linkToTab(newTabContent);
        if (activeTab == null)
        { thisTab.activatePanel(); }
        else newTabContent.SetActive(false);
        tabDict.Add(tabName, thisTab);
        return thisTab;
    }

    public void saveAll()
    {
        Debug.Log("saved all" + settingElements.Count);
        for (int i = 0; i < settingElements.Count; i++)
            settingElements[i].saveCurrent();
    }


    public void reset()
    {
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < settingElements.Count; i++)
            settingElements[i].resetToDefault();
    }
    public void _toggleSettings()
    {
        if (instance.canvas == null) instance.canvas = instance.GetComponent<Canvas>();
        instance.canvas.enabled = !instance.canvas.enabled;

    }
    public static void toggleSettings()
    {
        if (instance.canvas == null) instance.canvas = instance.GetComponent<Canvas>();
        instance.canvas.enabled = !instance.canvas.enabled;

    }
    public void loadSaved()
    {
        for (int i = 0; i < settingElements.Count; i++)
            settingElements[i].loadSaved();
    }
    public void savePreferencesToggle(bool b)
    {
        if (!autoSave && b)
            saveAll();
        autoSave = b;
        Debug.Log("preferences will be saved =" + b);

    }

    float tabHeights;
    [Range(30, 100)]
    public float headerHeight = 70;
    void setHeaderHeight(float h)
    {
        if (headerLayoutElement != null)
        {
            headerLayoutElement.minHeight = h;
            headerLayoutElement.preferredHeight = h;
            RectTransform r = headerLayoutElement.GetComponent<RectTransform>();
            r.sizeDelta = new Vector2(r.sizeDelta.x, h);
        }
    }
    protected override void OnValidate()
    {
        base.OnValidate();
        //setHeight(headerHeight);

    }
    public override void setHeight(float f)
    {
        base.setHeight(f);
        zNode tab = getTemplate("Tab");
        if (tab == null) return;
        RectTransform tabRecttab = tab.transform.parent.GetComponent<RectTransform>();

        if (tabHeights == 0) tabHeights = tabRecttab.sizeDelta.y;
        tabRecttab.sizeDelta = new Vector2(tabRecttab.sizeDelta.x, f * 1.6f); //tabHeights
        setHeaderHeight(2 * f);
    }


    public override void highlightPanel(BaseEventData eventData)
    {
        if (editFrame != null) editFrame.enabled = true;

    }
    public override void restoreColor(BaseEventData eventData)
    {
        if (editFrame != null) editFrame.enabled = false;

    }



    public bool IsThereASettingCalled(string paramName, string tabName)
    {
        paramName += tabName;

        if (elementDict == null) return false;
        return (elementDict.ContainsKey(paramName));
    }


    SettingsTab getTab(string tabName)
    {
        if (string.IsNullOrEmpty(tabName))
            Debug.LogWarning("tab name too short");


        if (tabDict == null) // do we have the dictionary?
            tabDict = new Dictionary<string, SettingsTab>();
        SettingsTab thisTab;
        if (tabDict.ContainsKey(tabName))
        {
            tabDict.TryGetValue(tabName, out thisTab);
        }
        else
            thisTab = AddTab(tabName);

        return thisTab;
    }
    public SettingsElement addSettingsElement(string nodeType, string paramName, string tabName, bool loadPref = true)
    {
        if (!started) Debug.Log(" settings not ready (how) ");

        SettingsElement prefab = getTemplate(nodeType) as SettingsElement;
        if (prefab == null)
            Debug.Log("unknown element " + nodeType, gameObject);

        SettingsElement thisSetting;
        if (elementDict.TryGetValue(tabName + paramName, out thisSetting))
        {
            //            Debug.Log("slider exists");
            return thisSetting;
        }
        else
        {
            SettingsTab thisTab = getTab(tabName);

            //   try {
            thisSetting = Instantiate(prefab, prefab.transform.parent);
            //  } catch { Debug.LogError("error instantiating prefab "+prefab+" of type "+nodeType); return null;}
            thisSetting.gameObject.SetActive(true);
            thisSetting.Init();
            thisSetting.setLabel(paramName);
            thisSetting.setTab(thisTab);
            elementDict.Add(tabName + paramName, thisSetting);
            settingElements.Add(thisSetting);
            lastAdded.Add(thisSetting);
            nodes.Add(thisSetting);
            if (loadPref) Invoke("loadLast", 0.3f);
            return thisSetting; // there you go
        }
    }
    public static SettingsSlider addSlider(string paramName, string tabName, bool loadPref = true)
    {
        return ((SettingsSlider)instance.addSettingsElement("Slider", paramName, tabName, loadPref));
    }
    public static SettingsToggle addToggle(string paramName, string tabName, bool loadPref = true)
    {
        return ((SettingsToggle)instance.addSettingsElement("Toggle", paramName, tabName, loadPref));
    }
    public static SettingsText addTextSetting(string paramName, string tabName, bool loadPref = true)
    {
        return ((SettingsText)instance.addSettingsElement("Text", paramName, tabName, loadPref));
    }
    public static SettingsLabel addLabel(string paramName, string tabName, bool loadPref = true)
    {
        return ((SettingsLabel)instance.addSettingsElement("Label", paramName, tabName, loadPref));
    }
    public static SettingsButton addButton(string paramName, string tabName, bool loadPref = true)
    {

        return ((SettingsButton)instance.addSettingsElement("Button", paramName, tabName, loadPref));
    }


    protected override void Awake()
    {
        base.Awake();

        if (instance == null) { instance = this; }
        else { Debug.LogError("Two settings panels !", gameObject); }
        settingElements = new List<SettingsElement>();
        elementDict = new Dictionary<string, SettingsElement>();
        lastAdded = new List<SettingsElement>();
        started = true;
        zNode t = getTemplate("Tab");
        if (t != null) t.gameObject.SetActive(false);


        if (nodeTemplatePool[0].name.Equals("{Tab}")) Debug.LogWarning("settings should not have TABS at the first entry of the nodetemplate list!", nodeTemplatePool[0].gameObject);

    }
    public void newActiveContent(GameObject t)
    {
        content = t.GetComponent<RectTransform>();
        setScrollStateDirty();
    }


    void loadLast()
    {
        for (int i = 0; i < lastAdded.Count; i++)
        {
            lastAdded[i].loadSaved();
            //      Debug.Log("loading "+i);
        }

        lastAdded = new List<SettingsElement>();
    }


}
