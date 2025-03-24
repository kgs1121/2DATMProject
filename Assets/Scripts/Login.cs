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
    }

    public void SignUp()
    {
        if (string.IsNullOrEmpty(signId.text) || string.IsNullOrEmpty(signName.text) || string.IsNullOrEmpty(signpS.text) || string.IsNullOrEmpty(signpSConfirm.text)) // 빈 입력칸 확인
        {
            failSignUp.SetActive(true);
            failBg.SetActive(true);
            return;
        }
        
        if (signpS.text != signpSConfirm.text) // 회원가입 비밀번호 일치 확인
        {
            Debug.LogWarning("비밀번호가 일치하지 않습니다.");
            failSignUp.SetActive(true);
            failBg.SetActive(true);
            return;
        }
        
        GameManager.Instance.LoadUserData();

        foreach (var user in GameManager.Instance.userS)  // 회원가입 아이디 중복 확인
        {
            if (user.UserId == signId.text)
            {
                Debug.Log("중복된 ID입니다.");
                failSignUp.SetActive(true);
                failBg.SetActive(true);
                failId.text = "중복된 ID입니다.";
                return;
            }
        }

        UserData newUserData = new UserData(signId.text, signpS.text, signName.text, 100000, 50000);
        GameManager.Instance.userS.Add(newUserData); // 신규 유저 데이터 추가

        GameManager.Instance.SaveAllUserData(); // 데이터 저장

        Debug.Log("회원가입 완료");
        failId.text = "";
        successSignUp.SetActive(true);
        OnClickCancle();
    }

    public void OnClickSignUp() // Sign Up 버튼
    {
        signUpPopup.SetActive(true);
        signUpBg.SetActive(true);
    }

    public void OnClickCancle() // Cancle 버튼
    {
        signUpPopup.SetActive(false);
        signUpBg.SetActive(false);
    }

    public void OnClickCheckFail() // 오류 확인 버튼
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
            if(user.UserId == inputId.text && user.UserPs == inputPs.text) // 유저 아이디 비번 확인 후 유저 정보를 가지고 메인 씬으로 이동
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
