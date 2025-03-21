using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public GameObject atmUI;
    public GameObject depositUI;
    public GameObject withdrawalUI;
    public GameObject failUI;

    

    private void OnDisable()
    {
        if (GameManager.Instance.userData != null)
        {
            GameManager.Instance.userData.onUserDataUpdate -= Refresh;
            GameManager.Instance.userData.onUserDataUpdate -= GameManager.Instance.SaveUserData;
        }
    }

    private void Start()
    {
        customDepositInput.contentType = TMP_InputField.ContentType.IntegerNumber;
        customWithdrawalInput.contentType = TMP_InputField.ContentType.IntegerNumber;

        GameManager.Instance.LoadUserData();
        GameManager.Instance.userData.onUserDataUpdate += Refresh;
        GameManager.Instance.userData.onUserDataUpdate += GameManager.Instance.SaveUserData;


        Refresh();
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

    public void OnClickCancle()
    {
        atmUI.SetActive(true);
        depositUI.SetActive(false);
        withdrawalUI.SetActive(false);
        customDepositInput.text = "";
        customWithdrawalInput.text = "";
    }

    public void OnClickFailCancle()
    {
        customDepositInput.text = "";
        customWithdrawalInput.text = "";
        failUI.SetActive(false);
    }


    public void Refresh()
    {
        name.text = GameManager.Instance.userData.UserName;
        cash.text = string.Format("{0:N0}", GameManager.Instance.userData.UserCash);
        balance.text = string.Format("{0:N0}", GameManager.Instance.userData.UserBalance);
    }

    public void DoDeposit(int amount)
    {
        if (int.TryParse(customDepositInput.text, out int amount2))
        {
            amount = amount2;
        }

        if (amount <= GameManager.Instance.userData.UserCash)
        {

            GameManager.Instance.userData.UserCash -= amount;
            GameManager.Instance.userData.UserBalance += amount;
        }
        else failUI.SetActive(true);
    }

    public void DoWithdrawal(int amount)
    {
        if(int.TryParse(customWithdrawalInput.text, out int amount2))
        {
            amount = amount2;
        }

        if (amount <= GameManager.Instance.userData.UserBalance)
        {
            GameManager.Instance.userData.UserCash += amount;
            GameManager.Instance.userData.UserBalance -= amount;
        }
        else failUI.SetActive(true);
    }
}
