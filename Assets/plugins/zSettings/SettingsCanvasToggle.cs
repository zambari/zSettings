//z2k17

using UnityEngine;

public class SettingsCanvasToggle : MonoBehaviour
{
    Canvas settingsCanvas;
    public GameObject settingsPanel;
       public GameObject settingsOpenButton;
       public bool hideButtonWhenOpen;
    public bool startOpen;
 public bool openWithEscape;
    void Start()
    {
        settingsCanvas = GetComponent<Canvas>();
        if (!startOpen) HideSettings();
    }

    public void HideSettings()
    {

            settingsCanvas.enabled=false;
         if (settingsOpenButton!=null)
            settingsOpenButton.SetActive(true);

    }

    public void OpenSettings()
    {
             settingsCanvas.enabled=true;
             if (hideButtonWhenOpen)
             if (settingsOpenButton!=null)
             settingsOpenButton.SetActive(false);
        
    }
    public void ToggleSettingspanelActive()
    {
        if (settingsCanvas.enabled)
        {
            HideSettings();

        }
        else
        {
           OpenSettings();
        }

    }

    void Update()
    {
        if (openWithEscape)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) ToggleSettingspanelActive();
        }
    }

}
