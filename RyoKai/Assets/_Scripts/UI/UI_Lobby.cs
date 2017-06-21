using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Lobby : BaseObject {


    UIButton StageButton = null;
    UIButton GachaButton = null;
    UIButton InvenButton = null;

    private void Awake()
    {
        Transform trans = FindInChild("StageButton");
        if (trans == null)
        {
            Debug.LogError("StageButton is not found");
        }
        StageButton = trans.GetComponent<UIButton>();
        EventDelegate.Add(StageButton.onClick, new EventDelegate(this, "ShowStage"));

        trans = FindInChild("GachaButton");
        if (trans == null)
        {
            Debug.LogError("GachaButton is not found");
        }
        GachaButton = trans.GetComponent<UIButton>();
        EventDelegate.Add(GachaButton.onClick, () =>
        {
            ItemManager.Instance.Gacha();
        });
        trans = FindInChild("InventoryButton");
        if (trans == null)
        {
            Debug.LogError("InventoryButton is not found");
        }
        InvenButton= trans.GetComponent<UIButton>();
        EventDelegate.Add(InvenButton.onClick, new EventDelegate(this, "ShowInventory"));
    }

    void ShowStage()
    {
        GameObject go = UI_Tools.Instance.ShowUI(eUIType.PF_UI_STAGE);
        UI_Stage stage = go.GetComponent<UI_Stage>();
        stage.Init();
    }

    void ShowInventory()
    {
        GameObject go = UI_Tools.Instance.ShowUI(eUIType.PF_UI_INVENTORY);
        UI_Inventory inven = go.GetComponent<UI_Inventory>();
        inven.Init();
        inven.Reset();
    }

}
