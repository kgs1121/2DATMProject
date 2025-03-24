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
        inputPs.text = "";  // 비밀번호 필드 초기화
        signpS.text = "";  // 비밀번호 필드 초기화
        signpSConfirm.text = "";  // 비밀번호 확인 필드도 초기화
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
            Debug.LogWarning("비밀번호가 일치하지 않습니다.");
            failSignUp.SetActive(true);
            failBg.SetActive(true);
            return;
        }
        
        GameManager.Instance.LoadUserData();

        foreach (var user in GameManager.Instance.userS)
        {
            if (user.UserId == signId.text)
            {
                Debug.Log("중복된 ID입니다.");
                failSignUp.SetActive(true);
                failBg.SetActive(true);
                failId.text = "ID를 확인해주세요.";
                return;
            }
        }

        UserData newUserData = new UserData(signId.text, signpS.text, signName.text, 100000, 50000);
        GameManager.Instance.userS.Add(newUserData);

        GameManager.Instance.SaveAllUserData();

        Debug.Log("회원가입 완료");
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
