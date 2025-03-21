using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public UserData userData;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject(nameof(GameManager));
                instance = obj.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveUserData()
    {
        if (userData == null) return;

        string saveData = JsonUtility.ToJson(userData);
        File.WriteAllText(Application.persistentDataPath + "/UserData.txt", saveData);
        Debug.Log(saveData);
    }

    public void LoadUserData()
    {
        if (File.Exists(Application.persistentDataPath + "/UserData.txt"))
        {
            string loadData = File.ReadAllText(Application.persistentDataPath + "/UserData.txt");
            userData = JsonUtility.FromJson<UserData>(loadData);
            Debug.Log(loadData);
        }
        else
        {
            GameManager.Instance.userData = new UserData("°­±â¼ö", 100000, 50000);
            GameManager.Instance.SaveUserData();
            Debug.Log(GameManager.Instance.userData.UserCash);
        }
    }
}


