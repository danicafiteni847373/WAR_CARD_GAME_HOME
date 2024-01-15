using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System;


public class DataMiningManager : MonoBehaviour
{
    private static DatabaseReference _dbReference;
    [SerializeField] private string _userId;
    private void Start()
    {
        _dbReference = FirebaseDatabase.DefaultInstance.RootReference;

        //try to load userId from playerprefs
        string _savedUserId = PlayerPrefs.GetString("CurrencyAmount");
        //if a userId is saved in PlayerPrefs, we read it
        if (!String.IsNullOrEmpty(_savedUserId))
        {
            _userId = _savedUserId;
        }
        else
        {
            //generates a new one
            _userId = GenerateUserId();
            PlayerPrefs.SetString("CurrencyAmount", _userId);
        }
        //if not availablle generate one

    }

    public void SaveNewAction(string action)
    {
        PlayerDataManager playerData = new PlayerDataManager(_userId, action);
        string json = JsonUtility.ToJson(playerData);
        _dbReference.Child("players").Child(_userId).Child(playerData.Timestamp).SetRawJsonValueAsync(json);
        Debug.Log($"Action {action} saved successfully!");
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Start();
    }

    public void AppendNewAction(string action)
    {
        string key = _dbReference.Child(_userId).Push().Key;

        PlayerDataManager playerData = new PlayerDataManager(_userId, action);
        string json = JsonUtility.ToJson(playerData);
        _dbReference.Child("players").Child(_userId).Child(key).SetRawJsonValueAsync(json);
        //Dictionary<string, System.Object> playerDict = playerData.ToDictionary();
        Debug.Log($"Action {action} saved successfully!");
    }

    public void OverwriteNewAction(string action)
    {
        PlayerDataManager playerData = new PlayerDataManager(_userId, action);
        string json = JsonUtility.ToJson(playerData);
        _dbReference.Child("players").Child(_userId).SetRawJsonValueAsync(json);
        Debug.Log($"Action {action} saved successfully!");
    }

    public static string GenerateUserId()
    {
        return Guid.NewGuid().ToString();
    }


}
