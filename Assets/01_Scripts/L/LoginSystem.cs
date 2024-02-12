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
            Debug.Log("userID�� �Է����ּ���!");
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
            Debug.Log("userID�� �Է����ּ���!");
        }
        else
        {
            if (PlayerPrefs.HasKey("UserID"))
            {
                SceneManager.LoadScene("ChSelect");
            }
            else
            {
                Debug.Log("userID�� �����ϴ�. ȸ�������� ���ּ���");
            }
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}