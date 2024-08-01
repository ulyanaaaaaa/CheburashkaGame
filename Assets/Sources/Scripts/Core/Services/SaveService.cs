using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveService : MonoBehaviour
{
    public Action OnSave;
    
    private string _filePath;
    public FileSaveData CurrentSaveData { get; private set; } 
    private void Awake()
    {
        _filePath = Application.persistentDataPath + "/SaveData.data";
        
        if (IsFileExist())
            CurrentSaveData = Load();
        else
            CurrentSaveData = new FileSaveData();
    }

    public void Save()
    {
        OnSave?.Invoke();
        using (FileStream file = File.Create(_filePath))
        {
            new BinaryFormatter().Serialize(file, CurrentSaveData);
        }
    }
    
    
    public FileSaveData Load()
    {
        FileSaveData returnObj = new FileSaveData();
        if (IsFileExist())
        {
            using (FileStream file = File.Open(_filePath, FileMode.Open))
            {
                object loadedData = new BinaryFormatter().Deserialize(file);
                returnObj = (FileSaveData) loadedData;
            }
        }
        else
        {
            Save();
        }

        return returnObj;
    }

    
    public bool IsFileExist()
    {
        if (File.Exists(_filePath))
            return true;
        return false;
    }
}

[Serializable]
public class FileSaveData : SaveData
{

    public Language Language { get; private set; }
    public Dictionary<string, SaveData> Datas = new Dictionary<string, SaveData>();
    public FileSaveData()
    {

    }

    public void UpdateLanguage(Language language)
    {
        Language = language;
    }
    
    public bool TryGetData<T>(string id, out T data) where T : SaveData
    {
        foreach (SaveData saveData in Datas.Values)
        {
            if (saveData.Id == id)
            {
                data = (T)saveData;
                return true;
            }
        }
        data = null;
        return false;
    }

    public void AddData(string id, SaveData saveData)
    {
        Dictionary<string, SaveData> datasBuffer = new Dictionary<string, SaveData>(Datas);
        foreach (string datasKey in datasBuffer.Keys)
        {
            if (datasKey == id)
            {
                Datas.Remove(id);
            }
        }
        Datas.Add(id, saveData);
    }
    
    public void RemoveData(string id)
    {
        Dictionary<string, SaveData> datasBuffer = new Dictionary<string, SaveData>(Datas);
        foreach (string datasKey in datasBuffer.Keys)
        {
            if (datasKey == id)
            {
                Datas.Remove(id);
            }
        }
    }
} 

[Serializable]
public class SaveData
{
    public string Id;
    public Type Type;

    public SaveData()
    {
        
    }

    public SaveData(string id, Type type)
    {
        Id = id;
        Type = type;
    }
}
