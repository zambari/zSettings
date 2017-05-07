//z2k17

using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.UI.Extensions;
//using System.Collections;
//using System.Collections.Generic;

public class ToggleGameObjects : MonoBehaviour
{
    GameObject[] children;
    void Start()
    {
        children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
            SettingsToggle thisToggle = zSettings.addToggle(children[i].name, "toggles");
            thisToggle.defValue = false;
            thisToggle.value = false;
            int k = i;
            thisToggle.valueChanged += ((v) => toggleObject(k, v));

        }

    }

    void toggleObject(int i, bool active)
    {
        children[i].SetActive(active);
    }
}
