using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public List<UserData> userS = new List<UserData>();
    public UserData currentUser;

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

    public void SaveAllUserData() // 유저 전체 데이터 저장
    {
        string path = Application.persistentDataPath + "/UserData.txt";
        UserDataList userDataList = new UserDataList(userS);

        string saveData = JsonUtility.ToJson(userDataList, true);
        File.WriteAllText(path, saveData);
        Debug.Log("신규 유저 데이터 저장 완료!");
        Debug.Log(path);
    }

    public void SaveUserData(UserData updateUser) // 접속한 유저 데이터 저장
    {
        // 기존 데이터 로드
        string path = Application.persistentDataPath + "/UserData.txt";
        if (!File.Exists(path)) return;

        string loadData = File.ReadAllText(path);
        UserDataList userDataList = JsonUtility.FromJson<UserDataList>(loadData);

        // 접속한 유저 데이터만 저장
        for (int i = 0; i < userDataList.users.Count; i++)
        {
            if (userDataList.users[i].UserId == updateUser.UserId)
            {
                userDataList.users[i] = updateUser;
                break;
            }
        }

        string saveData = JsonUtility.ToJson(userDataList, true);
        File.WriteAllText(path, saveData);

        Debug.Log("현재 로그인한 유저 데이터 저장 완료!");
    }

    public void LoadUserData()
    {
        if (File.Exists(Application.persistentDataPath + "/UserData.txt"))
        {
            string loadData = File.ReadAllText(Application.persistentDataPath + "/UserData.txt");
            UserDataList userDataList = JsonUtility.FromJson<UserDataList>(loadData);
            userS = userDataList.users;
        }
        else
        {
            Debug.Log("아이디가 없습니다.");
        }
    }
}


