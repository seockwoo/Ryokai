using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Gacha : MonoBehaviour {

    UILabel Label = null;
    UITexture Texture = null;

    ItemInstance ItemInst;
    public ItemInstance ITEM_INSTANCE
    {
        get { return ItemInst; }
        set { ItemInst = value; }
    }

    public void Init(ItemInstance instance)
    {
        ItemInst = instance;

        Label = this.transform.FindChild("Content").GetComponent<UILabel>();
        Texture = this.transform.FindChild("Texture").GetComponent<UITexture>();

        Label.text = instance.ITEM_INFO.GetSlotString() + " : " + ItemInst.ITEM_INFO.NAME + "\n" + instance.ITEM_INFO.STATUS.StatusString();

        Texture.mainTexture = Resources.Load<Texture>("Textures/" + ItemInst.ITEM_INFO.ITEM_IMAGE);

        TweenAlpha tween = transform.FindChild("Effect").GetComponent<TweenAlpha>();
        tween.ResetToBeginning();
        tween.enabled = true;

    }


    public void YesClick()
    {
        UI_Tools.Instance.HideUI(eUIType.PF_UI_GACHA);
    }
	
}
