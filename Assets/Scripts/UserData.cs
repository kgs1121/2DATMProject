using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class UserData
{
    [SerializeField] private string useriD;
    [SerializeField] private string userPs;
    [SerializeField] private string userName;
    [SerializeField] private int userCash;
    [SerializeField] private int userBalance;

    public event Action<UserData> onUserDataUpdate;

    public UserData(string id, string ps, string userName1, int userCash1, int userBalance1)
    {
        useriD = id;
        userPs = ps;
        userName = userName1;
        userCash = userCash1;
        userBalance = userBalance1;
    }

    public string UserId
    {
        get => useriD;
        set => useriD = value;
    }

    public string UserPs
    {
        get => userPs;
        set => userPs = value;
    }

    public string UserName
    {
        get => userName;
        set
        {
            if(userName != value)
            {
                userName = value;
                onUserDataUpdate?.Invoke(this);
            }
        }
    }

    public int UserCash
    {
        get => userCash;
        set
        {
            if (userCash != value)
            {
                userCash = value;
                onUserDataUpdate?.Invoke(this);
            }
        }
    }

    public int UserBalance
    {
        get => userBalance;
        set
        {
            if (userBalance != value)
            {
                userBalance = value;
                onUserDataUpdate?.Invoke(this);
            }
        }
    }
}

[System.Serializable]
public class UserDataList
{
    public List<UserData> users;

    public UserDataList(List<UserData> users)
    {
        this.users = users;
    }
}
