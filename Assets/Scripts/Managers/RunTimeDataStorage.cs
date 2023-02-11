using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RunTimeDataStorage : Singleton<RunTimeDataStorage>
{

    public virtual void SaveData(string key, string data)
    {
        PlayerPrefs.SetString(key, data);
        PlayerPrefs.Save();
    }

    public virtual bool HasData(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public virtual string GetData(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public virtual void RemoveData(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
}