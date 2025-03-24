using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public GameObject signUpPopup;
    public GameObject signUpBg;
    public GameObject failBg;
    public GameObject failSignUp;
    public GameObject successSignUp;

    public TMP_InputField signId;
    public TMP_InputField signName;
    public TMP_InputField signpS;
    public TMP_InputField signpSConfirm;

    public TMP_InputField inputId;
    public TMP_InputField inputPs;

    public TextMeshProUGUI failId;

    private void Start()
    {
        inputPs.contentType = TMP_InputField.ContentType.Password;
        signpS.contentType = TMP_InputField.ContentType.Password;
        signpSConfirm.contentType = TMP_InputField.ContentType.Password;
        inputPs.text = "";  // ��й�ȣ �ʵ� �ʱ�ȭ
        signpS.text = "";  // ��й�ȣ �ʵ� �ʱ�ȭ
        signpSConfirm.text = "";  // ��й�ȣ Ȯ�� �ʵ嵵 �ʱ�ȭ
    }

    public void SignUp()
    {
        if (string.IsNullOrEmpty(signId.text) || string.IsNullOrEmpty(signName.text) || string.IsNullOrEmpty(signpS.text) || string.IsNullOrEmpty(signpSConfirm.text))
        {
            failSignUp.SetActive(true);
            failBg.SetActive(true);
            return;
        }
        
        if (signpS.text != signpSConfirm.text)
        {
            Debug.LogWarning("��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            failSignUp.SetActive(true);
            failBg.SetActive(true);
            return;
        }
        
        GameManager.Instance.LoadUserData();

        foreach (var user in GameManager.Instance.userS)
        {
            if (user.UserId == signId.text)
            {
                Debug.Log("�ߺ��� ID�Դϴ�.");
                failSignUp.SetActive(true);
                failBg.SetActive(true);
                failId.text = "ID�� Ȯ�����ּ���.";
                return;
            }
        }

        UserData newUserData = new UserData(signId.text, signpS.text, signName.text, 100000, 50000);
        GameManager.Instance.userS.Add(newUserData);

        GameManager.Instance.SaveAllUserData();

        Debug.Log("ȸ������ �Ϸ�");
        failId.text = "";
        successSignUp.SetActive(true);
        OnClickCancle();
    }

    public void OnClickSignUp()
    {
        signUpPopup.SetActive(true);
        signUpBg.SetActive(true);
    }

    public void OnClickCancle()
    {
        signUpPopup.SetActive(false);
        signUpBg.SetActive(false);
    }

    public void OnClickCheckFail()
    {
        successSignUp.SetActive(false);
        failSignUp.SetActive(false);
        failBg.SetActive(false);
    }

    public void OnClickLogin()
    {
        GameManager.Instance.LoadUserData();

        foreach(var user in GameManager.Instance.userS)
        {
            if(user.UserId == inputId.text && user.UserPs == inputPs.text)
            {
                GameManager.Instance.currentUser = user;
                SceneManager.LoadScene("SampleScene");
                return;
            }
        }

        failSignUp.SetActive(true);
        failBg.SetActive(true);
    }
}
