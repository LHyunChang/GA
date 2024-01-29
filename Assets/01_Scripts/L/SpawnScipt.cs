using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnScipt : MonoBehaviourPunCallbacks
{
    public GameObject[] classNumPrefabs;
    public int classNum;

    public void CreatePlayer()
    {
        int curSlotNum = SelectSlot.slotNum;
        classNum = PlayerPrefs.GetInt($"{curSlotNum}_ClassNum");
        //Debug.Log(classNum);

        Debug.Log("spawnScript���� curSlotNum�� "+curSlotNum);
        if (PhotonNetwork.IsConnected)
        {
            Transform[] points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
            int idx = Random.Range(1, points.Length);

            //PhotonNetwork.Instantiate(characterPrefabs[(int)DataMgr.instance.currentCharacter].name, points[idx].position, points[idx].rotation, 0);
            PhotonNetwork.Instantiate(classNumPrefabs[classNum].name, points[idx].position, points[idx].rotation, 0);
        }
    }
}
