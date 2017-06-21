using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    UILabel Label;


    //Dont Destroy를 하지 않기 위함
    public override void Init()
    {
        Label = transform.FindChild("GameOverLabel").GetComponent<UILabel>();
    }

    public void SetText(bool isKill, float data)
    {
        if(isKill)
        {
            Label.text = "Kill Count : " + ((int)data).ToString();
        }
        else
        {
            Label.text = string.Format("Time {0} : {1}", (int)data / 60, (int)data % 60);
        }
    }


}
