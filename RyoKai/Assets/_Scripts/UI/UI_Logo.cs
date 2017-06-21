using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Logo : BaseObject {

    UIButton StartButton = null;

	// Use this for initialization
	void Start () {
        Transform temp = FindInChild("StartButton");
        if(temp == null)
        {
            Debug.LogError(gameObject.name + "에 StartButton이 없습니다.");
            return;
        }

        StartButton = temp.GetComponent<UIButton>();
        //EventDelegate.Add(StartButton.onClick, new EventDelegate(this, "OnClickStartButton"));

        //람다식 사용
        //() => 여기 ()에는 매개변수가 들어감.
        EventDelegate.Add(StartButton.onClick, () => { Scene_Manager.Instance.LoadScene(eSceneType.SCENE_LOBBY); });


	}

    //void OnClickStartButton()
    //{
    //    //GoLobby
    //    Scene_Manager.Instance.LoadScene(eSceneType.SCENE_LOBBY);
    //}
}
