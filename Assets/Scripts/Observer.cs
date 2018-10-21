using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{

    public static Dictionary<string, List<Action>> listeners = new Dictionary<string, List<Action>>();

    public static void Attach(string key, Action value)
    {
        bool containsKey = listeners.ContainsKey(key);
        if (containsKey)
        {
            var list = listeners[key];
            bool containValue = !list.Contains(value);
            if (containValue)
            {
                listeners.Add(key, list); 
            }
            else
            {
                Debug.LogError("Listener contains this method");
            }    
        }
        else
        {
            
            List<Action> list = new List<Action>();
            list.Add(value);
            listeners.Add(key, list); 
        }
    
    }

    public static void Detatch(string key, Action value)
    {
        bool containsKey = listeners.ContainsKey(key);
        if (containsKey)
        {
            var list = listeners[key];
            bool containValue = !list.Contains(value);
            if (containValue)
            {
                list.Remove(value);
                if (list.Count == 0)
                {
                    listeners.Remove(key);
                }
      
            }
            else
            {
                Debug.LogError("Cannot found value");
            }
        }
        else
        {
            
            Debug.LogError("Cannot found key");
        }
    }

    public static void Execute(string key)
    {
        foreach (var action in listeners[key])
        {
            action();
        }
    }

}
