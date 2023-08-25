using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using Newtonsoft.Json;
using UnityEngine.Events;

public class DataEventTrigger : UnityEvent<object>
{

}
public static class DataTrigger
{
    public static Dictionary<string, DataEventTrigger> dicOnValueChange = new Dictionary<string, DataEventTrigger>();
    public static void RegisterValueChange(string s, UnityAction<object> delegateDataChange)
    {
        if (dicOnValueChange.ContainsKey(s))
        {
            dicOnValueChange[s].AddListener(delegateDataChange);
        }
        else
        {
            dicOnValueChange.Add(s, new DataEventTrigger());
            dicOnValueChange[s].AddListener(delegateDataChange);
        }

    }
    //extention method 
    public static void TriggerEventData(this object data, string path)
    {
        if (dicOnValueChange.ContainsKey(path))
            dicOnValueChange[path].Invoke(data);
    }
}

public class DataBaseLocal : MonoBehaviour
{
    // playerInfo/username
    private PlayerData dataPlayer;
    public bool LoadData()
    {
        //PlayerPrefs.DeleteAll();
        //return;
        if (PlayerPrefs.HasKey("DATA"))
        {
          
            GetData();
            return true;
        }
        else
        {
            return false;
          
        }
    }
    public void CreateNewData (PlayerData dataPlayer,Action callback)
    {
        this.dataPlayer = dataPlayer;
        SaveData();
        callback?.Invoke();
    }
    public T Read<T>(string path)
    {
        GetData();
        object data=null;

        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);

        ReadDataBypath(paths, dataPlayer, out data);

        return (T)data;
    }
    public T Read<T>(string path, object key)
    {
        GetData();
        object data = null;

        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);
       
        ReadDataBypath(paths, dataPlayer, out data);
        Dictionary<string, T> newDic = (Dictionary<string, T>)data;

        T outData;
        newDic.TryGetValue(key.ToKey(), out outData);
        return outData;
    }
    private void ReadDataBypath(List<string> paths,object data, out object dataOut)
    {
        string p = paths[0];

        Type t = data.GetType();

        FieldInfo field = t.GetField(p);
        if(paths.Count==1)
        {
            dataOut = field.GetValue(data);
        }
        else
        {
            paths.RemoveAt(0);
            ReadDataBypath(paths, field.GetValue(data), out dataOut);
        }

    }
    public void UpdateData(string path,object dataNew,Action callback)
    {

        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);
        UpdateDataBypath(paths, dataPlayer, dataNew, callback);
        SaveData();

        dataNew.TriggerEventData(path);
    }
    private void UpdateDataBypath(List<string> paths, object data, object datanew, Action callback)
    {
        string p = paths[0];

        Type t = data.GetType();

        FieldInfo field = t.GetField(p);
        if (paths.Count == 1)
        {
            field.SetValue(data, datanew);
            if(callback!=null)
            {
                callback();
            }
           
        }
        else
        {
            paths.RemoveAt(0);
            UpdateDataBypath(paths, field.GetValue(data), datanew, callback);
        }

    }
    public void UpdateData<TValue>(string path, object key, TValue dataNew, Action callback)
    {
        GetData();
        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);
        UpdateDataDicBypath(paths, dataPlayer, key,dataNew, callback);
        SaveData();
        dataNew.TriggerEventData(path);
    }
    private void UpdateDataDicBypath<TValue>(List<string> paths, object data, object key, TValue dataNew, Action callback)
    {
        string p = paths[0];

        Type t = data.GetType();

        FieldInfo field = t.GetField(p);


        if (paths.Count == 1)
        {
            object dic = field.GetValue(data);

            Dictionary<string, TValue> newDic = dic as Dictionary<string, TValue>;
        
            if (newDic.ContainsKey(key.ToKey()))
            {
         
                newDic[key.ToKey()] = dataNew;
            }
            else
            {
            
                newDic.Add(key.ToKey(), dataNew);
            }
            field.SetValue(data, newDic);
            if (callback != null)
            {
                callback();
            }
           
        }
        else
        {
            paths.RemoveAt(0);
            UpdateDataDicBypath(paths, field.GetValue(data),key, dataNew, callback);
        }

    }
    public void Delete()
    {

    }
    private void SaveData()
    {
        string s = JsonConvert.SerializeObject(dataPlayer, Formatting.None);
        PlayerPrefs.SetString("DATA", s);
    }
    private void GetData()
    {
        string s = PlayerPrefs.GetString("DATA");
        dataPlayer = JsonConvert.DeserializeObject<PlayerData>(s);
    }
}
