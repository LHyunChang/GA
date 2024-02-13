using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public List<string> chatList = new List<string>();

    public Text chatLog;
    public InputField inputChat;
    public ScrollRect scrollRect;

    public Text nickNameTxt;

    public PhotonView pv;
    public Player player;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        player = GetComponent<Player>();

        GameObject canvasObj = GameObject.Find("WorldCanvas");
        GameObject backChSelect = canvasObj.transform.Find("BackChSelect_Chat").gameObject;
        GameObject chattingBox = backChSelect.transform.Find("ChattingBox").gameObject;
        scrollRect = chattingBox.GetComponentInChildren<ScrollRect>();

        inputChat = chattingBox.GetComponentInChildren<InputField>();
        chatLog = scrollRect.content.GetComponentInChildren<Text>();

        Canvas nickCanvas = GetComponentInChildren<Canvas>();
        nickNameTxt = nickCanvas.GetComponentInChildren<Text>();

        inputChat.enabled = false;
        chatLog.text = "";

        StartCoroutine(CheckEnterKey());
    }

    public void Update()
    {
        if (pv.IsMine)
        {
            if (!inputChat.isFocused)
            {
                player.allowMove = true; // 깜빡이지 않으니 움직일수있다.
            }
            else
            {
                player.allowMove = false; // 깜빡이니 못움직임
            }
        }
    }


    #region 채팅 함수
    public void OnClickSendBtn()
    {
        if (inputChat.text.Trim().Equals(""))
        {
            Debug.Log("채팅창 Empty, 채팅창을 비활성화 합니다");
            inputChat.Select();
            inputChat.enabled = false;
            return;
        }
        else
        {
            string msg = string.Format("[{0}] {1}", PhotonNetwork.LocalPlayer.NickName, inputChat.text);
            Debug.Log(msg);
            photonView.RPC("ReceiveMsg", RpcTarget.AllBuffered, msg);
            inputChat.ActivateInputField(); // 메세지를 보내도 활성화
            inputChat.text = "";
        }
    }

    [PunRPC]
    public void ReceiveMsg(string msg)
    {
        chatLog.text += "\n" + msg;
        scrollRect.verticalNormalizedPosition = 0.0f;
    }

    public IEnumerator CheckEnterKey()
    {
        while (true)
        {
            if (pv.IsMine && Input.GetKeyDown(KeyCode.Return))
            {
                if (inputChat != null)
                {
                    if (inputChat.enabled == false)
                    {
                        inputChat.enabled = true;
                        inputChat.ActivateInputField();

                        yield return null;
                    }
                    else
                    {
                        if (!inputChat.isFocused)
                        {
                            OnClickSendBtn();
                        }
                        else
                        {
                            inputChat.ActivateInputField();
                        }
                    }
                }
            }
            yield return null;
        }
    }
    #endregion
}