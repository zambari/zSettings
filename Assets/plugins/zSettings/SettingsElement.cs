//z2k17

using UnityEngine;
using UnityEngine.UI;

public class SettingsElement : zNode {
    [HideInInspector]
    public SettingsTab tab;
    public zSettings settingsPanel;
    public int orderingHint;


    public virtual void Init()
    {

    }

    public void setTab(SettingsTab t)
    {
        tab = t;
        if (tab == null) Debug.Log("invalid tab");
        GameObject c = tab.getContent();
        if (c == null) Debug.Log("no content");
        transform.SetParent(c.transform); 
    }
    public string getID()
    {   
        return tab.tabName + "_" + nodeName;
    }

 
    protected virtual void elementValueChanged()
    {
        if (settingsPanel==null)
         settingsPanel=GetComponentInParent<zSettings>();
                
        if (settingsPanel!=null && settingsPanel.autoSave)
        {
            saveCurrent();
        }
    }
    public virtual void resetToDefault()
    {

    }
    public virtual void loadSaved()
    {
        
    }
    public virtual void saveCurrent()
    {
        
    }
     public virtual void clearSaved()
    {
       if (PlayerPrefs.HasKey(getID()))
       {
           PlayerPrefs.DeleteKey(getID());
       }
             
    }


}
