using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotClass
{
    Slot0, Slot1, Slot2
}

public class SelectSlot : MonoBehaviour
{
    public string currentSlot = null;
    public static int slotNum;
    public SlotClass slot;
    public SelectSlot[] chars;

    public void OnClickSlotBtn()
    {
        currentSlot = slot.ToString();
        switch (currentSlot)
        {
            case "Slot0":
                slotNum = 0;
                break;
            case "Slot1":
                slotNum = 1;
                break;
            case "Slot2":
                slotNum = 2;
                break;
            default:
                break;
        }
    }
}
