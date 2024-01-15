using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerAction
{
    USER_LOGIN,
    USER_LOGOUT,
    USER_PURCHASE,
    USER_CHAT
}

[Serializable]
public class PlayerDataManager
{
    private string _id;
    private string _timestamp;
    public string itemPurchased;
    //public string _itemPurchased;

    public string ID => _id;
    public string Timestamp => _timestamp;

    //public string ItemPurchased => _itemPurchased;

    public PlayerDataManager(string id, string playerAction)
    {
        _id = id;
        //_itemPurchased = itemPurchased;
        itemPurchased = playerAction.ToString();
        _timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
