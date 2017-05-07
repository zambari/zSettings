
 // THIS IS A PART OF zOSC LIBRARY, NOT INCLUDED IN THIS DEMO, CHECK OUT MY GITHUB
using UnityEngine;

public class ExampleSettingsMisc : MonoBehaviour
{
   
    void Start()
    {
    
        string tab = "Sample";
         zSettings.addLabel("OSC Settins (Here lobotomized)", tab);
        zSettings.addLabel("Listener (IP:" + Network.player.ipAddress + ")", tab);

        SettingsText listenPort = zSettings.addTextSetting("Listen port", tab);
        listenPort.intOnly = true;
        listenPort.AddPreset("9000");
        listenPort.AddPreset("8000");
        listenPort.AddPreset("9988");
        listenPort.AddPreset("10101");

        zSettings.addButton("▲ ▼ Swap In / Out Ports ▲ ▼ ", tab);

     zSettings.addLabel("destination", tab);
        SettingsText destIP = zSettings.addTextSetting("dest IP", tab);
        SettingsText destPort = zSettings.addTextSetting("dest port", tab);
        SettingsToggle local = zSettings.addToggle("Local Echo", tab);
        zSettings.addLabel("Test Send", tab);
        zSettings.addTextSetting("Test send addr", tab);
        zSettings.addSlider("TestFloat", tab);
        SettingsText recieveTestSliderAddress = zSettings.addTextSetting("Test recieve addr", tab);
        SettingsText sendTestSliderAddress = zSettings.addTextSetting("Test recieve addr", tab);
        zSettings.addSlider("RECIEVER", tab);
        zSettings.addToggle("Show Log", tab);
        zSettings.addToggle("Log Sends", tab);
        zSettings.addToggle("Log recieves", tab);
        destIP.AddPreset("127.0.0.1");
        destIP.AddPreset("192.168.0.101");
        destIP.AddPreset("192.168.0.102");
        destIP.AddPreset("192.168.0.2");
        destIP.AddPreset("192.168.1.2");
        destIP.AddPreset("192.168.1.20");
        destIP.AddPreset("192.168.2.20");
        destPort.intOnly = true;
        destPort.AddPreset("9000");
        destPort.AddPreset("8000");
        destPort.AddPreset("9988");
        destPort.AddPreset("10101");
        local.defValue = true;
        destIP.defVal = "127.0.0.1";
        sendTestSliderAddress.AddPreset("/testslider2");
        sendTestSliderAddress.AddPreset("/testslider3");
        recieveTestSliderAddress.AddPreset("/testslider2");
        recieveTestSliderAddress.AddPreset("/testslider3");


    }
    void OnEnable()
    {
        zSettings.ShowTab("Scales");
    }

    void OnDisable()
    {
        zSettings.HideTab("Scales");
    }
}
