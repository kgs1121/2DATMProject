using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PopupBank : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI cash;
    public TextMeshProUGUI balance;

    public TMP_InputField customDepositInput;
    public TMP_InputField customWithdrawalInput;

    public TMP_InputField sendTarget;
    public TMP_InputField sendMoeny;

    public GameObject atmUI;
    public GameObject depositUI;
    public GameObject withdrawalUI;
    public GameObject failUI;
    public GameObject failNotMoney;
    public GameObject failNotUser;
    public GameObject failNotInput;
    public GameObject sendUI;
    
    

    private void OnDisable()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentUser != null)
        {
            GameManager.Instance.currentUser.onUserDataUpdate -= Refresh;
            GameManager.Instance.currentUser.onUserDataUpdate -= GameManager.Instance.SaveUserData;
        }
    }

    private void Start()
    {
        customDepositInput.contentType = TMP_InputField.ContentType.IntegerNumber;
        customWithdrawalInput.contentType = TMP_InputField.ContentType.IntegerNumber;

        GameManager.Instance.LoadUserData();
        GameManager.Instance.currentUser.onUserDataUpdate += Refresh;
        GameManager.Instance.currentUser.onUserDataUpdate += GameManager.Instance.SaveUserData;


        Refresh(GameManager.Instance.currentUser);
    }

    public void OnClickDeposit()
    {
        atmUI.SetActive(false);
        depositUI.SetActive(true);
    }

    public void OnClickWithdrawal()
    {
        atmUI.SetActive(false);
        withdrawalUI.SetActive(true);
    }

    public void OnClickSend()
    {
        atmUI.SetActive(false);
        sendUI.SetActive(true);
    }

    public void OnClickCancle()
    {
        atmUI.SetActive(true);
        depositUI.SetActive(false);
        withdrawalUI.SetActive(false);
        sendUI.SetActive(false);
        customDepositInput.text = "";
        customWithdrawalInput.text = "";
    }

    public void OnClickFailCancle()
    {
        customDepositInput.text = "";
        customWithdrawalInput.text = "";
        failUI.SetActive(false);
        failNotInput.SetActive(false);
        failNotMoney.SetActive(false);
        failNotUser.SetActive(false);
    }


    public void Refresh(UserData updateUser)
    {
        name.text = GameManager.Instance.currentUser.UserName;
        cash.text = string.Format("{0:N0}", GameManager.Instance.currentUser.UserCash);
        balance.text = string.Format("{0:N0}", GameManager.Instance.currentUser.UserBalance);
    }

    public void DoDeposit(int amount)
    {
        if (int.TryParse(customDepositInput.text, out int amount2))
        {
            amount = amount2;
        }

        if (amount <= GameManager.Instance.currentUser.UserCash)
        {

            GameManager.Instance.currentUser.UserCash -= amount;
            GameManager.Instance.currentUser.UserBalance += amount;
        }
        else
        {
            failUI.SetActive(true);
            failNotMoney.SetActive(true);
        }
    }

    public void DoWithdrawal(int amount)
    {
        if(int.TryParse(customWithdrawalInput.text, out int amount2))
        {
            amount = amount2;
        }

        if (amount <= GameManager.Instance.currentUser.UserBalance)
        {
            GameManager.Instance.currentUser.UserCash += amount;
            GameManager.Instance.currentUser.UserBalance -= amount;
        }
        else
        {
            failUI.SetActive(true);
            failNotMoney.SetActive(true);
        }
    }

    public void DoSendMoney(int amount)
    {
        bool isUser = false;

        if (int.TryParse(sendMoeny.text, out int amount2))
        {
            amount = amount2;
        }

        GameManager.Instance.LoadUserData();

        if (string.IsNullOrEmpty(sendTarget.text) || string.IsNullOrEmpty(sendMoeny.text))
        {
            failUI.SetActive(true);
            failNotInput.SetActive(true);
            return;
        }
        
        if (GameManager.Instance.currentUser.UserBalance < amount)
        {
            failUI.SetActive(true);
            failNotMoney.SetActive(true);
            return;
        }

        foreach(var user in GameManager.Instance.userS)
        {
            if(user.UserId == sendTarget.text) isUser = true;

            if (isUser && GameManager.Instance.currentUser.UserBalance >= amount)
            {
                user.UserBalance += amount;
                GameManager.Instance.currentUser.UserBalance -= amount;
                GameManager.Instance.SaveAllUserData();
                return;
            }
        }

        if (!isUser)
        {
            failUI.SetActive(true);
            failNotUser.SetActive(true);
        }

    }
}
