//z2k17

using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.UI.Extensions;
using System.Collections;
using System.Collections.Generic;
using System;

public class zKeyMap : MonoBehaviour
{
    static List<KeyCode> k;
    static List<MonoBehaviour> m;
    static List<Action> a;
    public static bool map(MonoBehaviour mono,  Action action, KeyCode key )
    {  if (k == null)
        {
            k = new List<KeyCode>();
            m =new  List<MonoBehaviour>();
            a =new  List<Action>();
        }
        if (k.Contains(key))
        {
            Debug.Log("key " + key.ToString() + "is  mapped already, sorry");
            return false;
        }
        k.Add(key);
        m.Add(mono);
        a.Add(action);
        return true;
    }

    void init()
    {
         if (k == null)
        {
            k = new List<KeyCode>();
            m =new  List<MonoBehaviour>();
            a =new  List<Action>();
        }
    }
    void Awake()
    {
       init();
    }

    void Update()
    {
        for (int i = 0; i < k.Count; i++)
            if (Input.GetKeyDown(k[i]))
                if (m[i].enabled) a[i]();

    }


}
