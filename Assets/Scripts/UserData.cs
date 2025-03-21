using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class UserData
{
    [SerializeField] private string userName;
    [SerializeField] private int userCash;
    [SerializeField] private int userBalance;

    public event Action onUserDataUpdate;

    public UserData(string userName1, int userCash1, int userBalance1)
    {
        userName = userName1;
        userCash = userCash1;
        userBalance = userBalance1;
    }

    public string UserName
    {
        get => userName;
        set
        {
            if(userName != value)
            {
                userName = value;
                onUserDataUpdate?.Invoke();
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
                onUserDataUpdate?.Invoke();
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
                onUserDataUpdate?.Invoke();
            }
        }
    }
}
