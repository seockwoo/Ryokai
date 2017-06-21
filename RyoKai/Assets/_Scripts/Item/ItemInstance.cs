using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemInstance {

    int ItemID = -1;
    int ItemNo = -1;

    //실제 착용 위치
    eSlotType EquipSlotType = eSlotType.SLOT_NONE;
    ItemInfo Info = null;

    public int ITEM_ID { get { return ItemID; } }
    public int ITEM_NO { get { return ItemNo; } }
    public ItemInfo ITEM_INFO { get { return Info; } }
	public eSlotType SLOT_TYPE
    {
        get { return EquipSlotType; }
        set { EquipSlotType = value; }
    }

    public ItemInstance(int _id, eSlotType _type, ItemInfo _info)
    {
        ItemID = _id;
        EquipSlotType = _type;

        ItemNo = int.Parse(_info.KEY);
        Info = _info;
    }
}
