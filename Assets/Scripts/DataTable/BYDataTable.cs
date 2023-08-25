﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Text.RegularExpressions;
public class BYDataTableCreate : ScriptableObject
{
    public virtual void ImportData(TextAsset textData)
    {

    }
    public virtual string GetCSVData()
    {
        return string.Empty;
    }
    public virtual string GetJsonData()
    {
        return string.Empty;
    }
}
public abstract class RecordCompare<T> : IComparer<T>
{
    public abstract int OnRecordCompare(T x, T y);
    public int Compare(T x, T y)
    {
        return OnRecordCompare(x, y);
    }
    public abstract T GetKeySearch(object key);
}

public class BYDataTable<T> : BYDataTableCreate where T : class , new()
{
    [SerializeField]
    protected List<T> records= new List<T>();
    public RecordCompare<T> recordCompare;
    private void OnEnable()
    {
        SetCompareObject();
    }
    public virtual void SetCompareObject()
    {

    }
    public override void ImportData(TextAsset textData)
    {
        records.Clear();
        Type dataType = typeof(T);
        FieldInfo[] fieldInfos = dataType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        List<List<string>> grids = GetDataByCSV(textData);

        for (int i = 1; i < grids.Count; i++)
        {

            string jsonString = "{";
            for (int j = 0; j < grids[i].Count; j++)
            {
                if (j > 0)
                {
                    jsonString += ",";
                }
                if (fieldInfos[j].FieldType == typeof(string))
                {
                    jsonString += "\"" + fieldInfos[j].Name + "\":\"" + grids[i][j].ToString()+"\"";
                }
                else
                    jsonString += "\"" + fieldInfos[j].Name + "\":" + grids[i][j].ToString();
            }
            jsonString += "}";
         
            T recordData = JsonUtility.FromJson<T>(jsonString);
            records.Add(recordData);
        }

        records.Sort(recordCompare);
        base.ImportData(textData);
    }
    private List<List<string>> GetDataByCSV(TextAsset textData)
    {
        List<List<string>> grids = new List<List<string>>();
        string[] lines = textData.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string s = lines[i];
            if (s.CompareTo(string.Empty) != 0)
            {
              
                string[] lineData = s.Split(',');
                List<string> data = new List<string>();
                foreach (string e in lineData)
                {
                    string newChar = Regex.Replace(e, @"\t|\n|\r", "");
                    data.Add(newChar);
                }
                grids.Add(data);
            }
        }
        return grids;
    }

    public override string GetCSVData()
    {
        string s = string.Empty;
        Type mType = typeof(T);
        FieldInfo[] fieldInfos = mType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        for (int x = 0; x < fieldInfos.Length; x++)
        {
            if (x > 0)
                s += ",";
            s += fieldInfos[x].Name;

        }
        foreach (T e in records)
        {
            s += "\n";
            for (int x = 0; x < fieldInfos.Length; x++)
            {
                if (x > 0)
                    s += ",";
                s += fieldInfos[x].GetValue(e);

            }
        }
        return s;
    }
    public override string GetJsonData()
    {
        return JsonUtility.ToJson(this);
    }

    public T GetRecordByKeySearch(object key)
    {
        T item = recordCompare.GetKeySearch(key);
        int index= records.BinarySearch(item, recordCompare);

        if (index < 0)
            return null;

        return records[index];
    }

    private T CopyData(object data)
    {
        string s = JsonUtility.ToJson(data);

        return JsonUtility.FromJson<T>(s);
    }
    public List<T> GetAllRecord()
    {
        List<T> ls = new List<T>();
        foreach (T e in records)
        {
            ls.Add(CopyData(e));
        }

        return ls;
    }
}
