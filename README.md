# zSettings
Settings module for Unity, with sliders for float values, text valeus with presets, toggles, buttons, and playerprefecnes

![alt text](https://media.githubusercontent.com/media/zambari/zSettings/master/Screenshots/zSettings.png "zSettings")


To use: call static methods of zSettings from your monobehaviours to create new settings element
keep a reference to the newly created element to add a callback function to your code

For example

>void Start() <br /> 
{ <br /> 
   SettingsSlider thisSlider = zSettings.addSlider("Parameter Name, "Tab Name"); <br /> 
   thisSlider.valueChanged += setScale; <br /> 
} <br /> 

This wil either create a new tab and new control if your script is the first one requesting that, or a reference to the existing one if such combination (tab name / parameter name) exsits already;
You can than add callback to your function that will get called if the user changes the parameter (or if preferences are loaded)
By default the script auto-saves the values to playerPrefs and loads them upon startup, so you can expect a callback with next 100ms (via Invoke).


>void iNeedThisFloatValue(float f) <br /> 
{  
//Use your value here<br /> 
} <br /> 


