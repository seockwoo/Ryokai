using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void YesEvent();
public delegate void NoEvent();

public class UI_Popup : BaseObject {

    UILabel TitleLabel;
    UILabel ContentLabel;

    UIButton YesButton;
    UIButton NoButton;

    YesEvent Yes;
    NoEvent No;

    private void Awake()
    {
        TitleLabel = FindInChild("Title").GetComponent<UILabel>();
        ContentLabel = FindInChild("Content").GetComponent<UILabel>();

        YesButton = FindInChild("YesButton").GetComponent<UIButton>();
        NoButton = FindInChild("NoButton").GetComponent<UIButton>();

        EventDelegate.Add(YesButton.onClick, new EventDelegate(this, "OnClickYesButton"));
        EventDelegate.Add(NoButton.onClick, new EventDelegate(this, "OnClickNoButton"));

    }

    public void Set(YesEvent _yes, NoEvent _no, string _title, string _content)
    {
        Yes = _yes;
        No = _no;
        TitleLabel.text = _title;
        ContentLabel.text = _content;
    }

    public void OnClickYesButton()
    {
        if(Yes != null)
        {
            Yes();
        }
    }
    public void OnClickNoButton()
    {
        if(No != null)
        {
            No();
        }
    }

}
