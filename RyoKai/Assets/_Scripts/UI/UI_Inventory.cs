using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : BaseObject {

    bool IsInit = false;
    GameObject ItemPrefab = null;

    UIGrid Grid;
    UIButton CloseButton = null;

	
    UILabel WeaponLabel = null;
    UILabel ArmorLabel = null;
    UILabel HelmetLabel = null;
    UILabel GuntletLabel = null;


	//UI Inventory Eqip Box Player Texture Label
	UILabel WeaponTextureLabel = null;
	UILabel ArmorTextureLabel = null;
	UILabel HelmetTextureLabel = null;
	UILabel GuntletTextureLabel = null;

	//UI Inventory Equip Box  Player Texture
	UITexture WeaponTexture = null;
	UITexture ArmorTexture = null;
	UITexture HelmetTexture = null;
	UITexture GuntletTexture = null;

	//UI Player Load

	Actor PlayerModel = null;




	public void Init()
    {
        if (IsInit)
            return;

        ItemPrefab = Resources.Load("Prefabs/UI/PF_UI_ITEM") as GameObject;
        Grid = GetComponentInChildren<UIGrid>();

        CloseButton = FindInChild("CloseButton").GetComponent<UIButton>();
        EventDelegate.Add(CloseButton.onClick, new EventDelegate(this, "HideInventory"));

        WeaponLabel = FindInChild("Weapon").FindChild("ItemName").GetComponent<UILabel>();
        ArmorLabel = FindInChild("Armor").FindChild("ItemName").GetComponent<UILabel>();
        HelmetLabel = FindInChild("Helmet").FindChild("ItemName").GetComponent<UILabel>();
        GuntletLabel = FindInChild("Guntlet").FindChild("ItemName").GetComponent<UILabel>();

		//UI Inventory Eqip Box Player Texture Label
		WeaponTextureLabel = FindInChild("EquipBox").FindChild("Weapon").FindChild("Text").GetComponent<UILabel>();
		ArmorTextureLabel = FindInChild("EquipBox").FindChild("Armor").FindChild("Text").GetComponent<UILabel>();
		HelmetTextureLabel = FindInChild("EquipBox").FindChild("Helmet").FindChild("Text").GetComponent<UILabel>();
		GuntletTextureLabel = FindInChild("EquipBox").FindChild("Guntlet").FindChild("Text").GetComponent<UILabel>();

		//UI Inventory Equip Box  Player Texture
		WeaponTexture = FindInChild("EquipBox").FindChild("Weapon").FindChild("Texture").GetComponent<UITexture>();
		ArmorTexture = FindInChild("EquipBox").FindChild("Armor").FindChild("Texture").GetComponent<UITexture>();
		HelmetTexture = FindInChild("EquipBox").FindChild("Helmet").FindChild("Texture").GetComponent<UITexture>();
		GuntletTexture = FindInChild("EquipBox").FindChild("Guntlet").FindChild("Texture").GetComponent<UITexture>();

		if(WeaponTexture == null || ArmorTexture == null || HelmetTexture == null || GuntletTexture == null)
		{
			Debug.Log("Weapon, Armor, Helmet, Guntlet Texture is NULL Check");
		}

		if(WeaponTextureLabel == null || ArmorTextureLabel == null || HelmetTextureLabel == null || GuntletTextureLabel == null)
		{
			Debug.Log("Weapon, Armor, Helmet, Guntlet Texture Label is NULL Check");
		}



		ShowPlayerModel();
		EquipItemReset();
        ItemManager.Instance.EquipE = EquipItemReset;

        IsInit = true;
    }

    public void Reset()
    {
        for(int i=0;i<Grid.transform.childCount; i++)
        {
            Destroy(Grid.transform.GetChild(i).gameObject);
        }

        AddItem();
    }

    void AddItem()
    {
        List<ItemInstance> list = ItemManager.Instance.LIST_ITEM;
        for(int i = 0; i < list.Count; i++)
        {
            GameObject go = Instantiate(ItemPrefab, Grid.transform) as GameObject;
            go.transform.localScale = Vector3.one;
            go.GetComponent<UI_Item>().Init(list[i]);
        }

        Grid.repositionNow = true;
    }

    public void EquipItemReset()
    {
        Dictionary<eSlotType, ItemInstance> dic = ItemManager.Instance.DIC_EQUIP;

        foreach(var pair in dic)
        {
            switch (pair.Key)
            {
                case eSlotType.SLOT_WEAPON:
                    WeaponLabel.text = pair.Value.ITEM_INFO.NAME;
                    break;
                case eSlotType.SLOT_ARMOR:
                    ArmorLabel.text = pair.Value.ITEM_INFO.NAME;
                    break;
                case eSlotType.SLOT_HELMET:
                    HelmetLabel.text = pair.Value.ITEM_INFO.NAME;
                    break;
                case eSlotType.SLOT_GUNTLET:
                    GuntletLabel.text = pair.Value.ITEM_INFO.NAME;
                    break;
            }
        }
    }

    void HideInventory()
    {
        UI_Tools.Instance.HideUI(eUIType.PF_UI_INVENTORY);
    }

	void ShowPlayerModel()
	{

		//Inventory Player Model
		PlayerModel = ActorManager.Instance.PlayerLoad();
		PlayerModel.GetComponent<Player>().enabled = false;
		//GameObject PlayerParents = FindInChild("Player").GetComponent<GameObject>();
		//if (PlayerModel == null)
		//{
		//	Debug.Log("PlayerModel is null");
		//}


		GameObject go = GameObject.Find("TextureCamera");
		//Transform go = this.transform.Find("TextureCamera");

		if (go == null)
		{
			Debug.Log("go is nul");

		}

		//PlayerModel 초기화
		
		PlayerModel.transform.parent = go.transform;



		PlayerModel.transform.position = (go.transform.position + new Vector3(0, -0.5f ,1.0f));
		PlayerModel.transform.rotation = new Quaternion(0, 180, 0, 0);
		PlayerModel.transform.localScale = Vector3.one;


		


		////레이어 변경 UI
		//Transform[] tran = PlayerModel.GetComponentsInChildren<Transform>();
		//foreach (Transform t in tran)
		//{
		//	t.gameObject.layer = (int)eLayerType.LAYER_UI;
		//}

		
	

		
	

	}







}
