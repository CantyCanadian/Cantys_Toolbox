﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class ExternalDataManager : Singleton<ExternalDataManager>
{
    public static string ExternalDataPath { get { return "Assets/Assets/External/"; } }

    private Dictionary<string, int[]> m_PreparedFiles = null;
    private Dictionary<string, string> m_Data = null;

    public bool HasData(string key)
    {
        return m_Data.ContainsKey(key);
    }

    public string GetData(string key)
    {
        return m_Data[key];
    }

    public T GetData<T>(string key) where T : IConvertible
    {
        return m_Data[key].ConvertTo<T>();
    }

    public void PrepareFile(string fileName)
    {
        PrepareFile(fileName, new int[] { -1 });
    }

    public void PrepareFile(string fileName, int column)
    {
        if (column <= 0)
        {
            Debug.Log("ExternalDataManager : Column provided to prepare file " + fileName + " is invalid.");
            return;
        }

        PrepareFile(fileName, new int[] { column });
    }

    public void PrepareFile(string fileName, int[] column)
    {
        if (m_PreparedFiles == null)
        {
            m_PreparedFiles = new Dictionary<string, int[]>();
        }

        m_PreparedFiles.Add(fileName, column);
    }

    public void LoadPreparedFiles()
    {
        m_Data = new Dictionary<string, string>();

        foreach (KeyValuePair<string, int[]> file in files)
        {
            if (file.Value.Length == 0)
            {
                Debug.Log("ExternalDataManager : Trying to load file " + file.Key + " but it has no specified column.");
            }
            else if (file.Value.Length == 1)
            {
                if (file.Value[0] == -1)
                {
                    m_Data.AddRange(CSVUtil.LoadAllColumns(ExternalDataPath, file.Key));
                }
                else
                {
                    m_Data.AddRange(CSVUtil.LoadSingleColumn(ExternalDataPath, file.Key, file.Value[0]));
                }
            }
            else
            {
                m_Data.AddRange(CSVUtil.LoadMultipleColumns(ExternalDataPath, file.Key, file.Value));
            }
        }
    }
}