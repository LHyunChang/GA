using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // 게임의 버전
    private readonly string version = "1.0";

    public LoadPlayerInfo loadPlayerInfo;
    public int slotNum;
    public Canvas chCanvas;
    public GameObject fakeloading;

    void Awake()
    {
        // 마스터 클라이언트의 씬 자동 동기화 옵션
        PhotonNetwork.AutomaticallySyncScene = true;

        // 게임 버전 설정
        PhotonNetwork.GameVersion = version;

        // 포톤 서버와의 데이터의 초당 전송 횟수
        //Debug.Log(PhotonNetwork.SendRate);

        // 포톤 서버 접속
        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        loadPlayerInfo = GameObject.Find("LoadPlayerInfo").GetComponent<LoadPlayerInfo>();
    }


    #region 포톤 콜백 함수
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        MakeHome();
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Town");
        }
    }
    #endregion

    #region UI_BUTTON_EVENT
    public void SetUserId()
    {
        slotNum = SelectSlot.slotNum;
        switch (slotNum)
        {
            case 0:
                PhotonNetwork.NickName = loadPlayerInfo.slot1Text[0].text;
                break;
            case 1:
                PhotonNetwork.NickName = loadPlayerInfo.slot2Text[0].text;
                break;
            case 2:
                PhotonNetwork.NickName = loadPlayerInfo.slot3Text[0].text;
                break;
            default:
                break;
        }
    }

    public void OnClickStartBtn()
    {
        SetUserId();
        StartCoroutine(FakePanelOn());
    }

    IEnumerator FakePanelOn()
    {
        Instantiate(fakeloading, chCanvas.transform);
        yield return new WaitForSeconds(1f);
        JoinHome();
    }

    public void JoinHome()
    {
        PhotonNetwork.JoinRoom("Room_Home");
    }

    public void MakeHome()
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;
        ro.IsOpen = true;
        ro.IsVisible = true;

        PhotonNetwork.CreateRoom("Room_Home", ro);
    }

    public void OnClickGoIntroBtn()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Intro");
    }
    #endregion
}