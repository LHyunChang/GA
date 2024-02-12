using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginSystem : MonoBehaviour
{
    public InputField userIDInput;

    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
    }
    public void OnClickCreateBtn()
    {
        if (string.IsNullOrEmpty(userIDInput.text))
        {
            Debug.Log("userID를 입력해주세요!");
        }
        else
        {
            PlayerPrefs.SetString("UserID", userIDInput.text);
            PlayerPrefs.Save();
        }
    }

    public void OnClickLoginBtn()
    {
        if (string.IsNullOrEmpty(userIDInput.text))
        {
            Debug.Log("userID를 입력해주세요!");
        }
        else
        {
            if (PlayerPrefs.HasKey("UserID"))
            {
                SceneManager.LoadScene("ChSelect");
            }
            else
            {
                Debug.Log("userID가 없습니다. 회원가입을 해주세요");
            }
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}