using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scene_Manager : MonoSingleton<Scene_Manager> {

    bool IsAsync = true;
    AsyncOperation Operation = null;

    eSceneType CurrentState = eSceneType.SCENE_LOGO;
    eSceneType NextState = eSceneType.SCENE_NONE;

    float StackTime = 0.0f;
    public eSceneType CURRENT_SCENE { get { return CurrentState; } }

    public void LoadScene(eSceneType _type, bool _async = true)
    {
        if (CurrentState == _type)
            return;

        NextState = _type;
        IsAsync = _async;
    }


	// Update is called once per frame
	void Update () {


        // 비동기가 주기적으로 받아오는 것.
        // 동기는 계속

        // 비동기적으로 작동하고 있다면
        if (Operation != null)
        {
            StackTime += Time.deltaTime;
            // Loading UI set
            UI_Tools.Instance.ShowLoadingUI(StackTime / 2f);

            if (Operation.isDone == true && StackTime >= 2.0f)
            {
                CurrentState = NextState;
                CompleteLoad(CurrentState);

                Operation = null;
                NextState = eSceneType.SCENE_NONE;

                // Loading UI 삭제
                UI_Tools.Instance.HideUI(eUIType.PF_UI_LOADING, true);
            }
            else
                return;
        }

        if (CurrentState == eSceneType.SCENE_NONE)
            return;

        if(NextState != eSceneType.SCENE_NONE && CurrentState != NextState)
        {
            DisableScene(CurrentState);

            //비동기 로드
            if (IsAsync)
            {
                //아래의 내용은 즉, 이넘타입의 이름과 실제 씬 이름이 같아야함.
                Operation = SceneManager.LoadSceneAsync(NextState.ToString("F"));
                StackTime = 0.0f;

                //Loading UI set
                UI_Tools.Instance.ShowLoadingUI(0.0f);
            }
            //동기 로드
            else
            {
                SceneManager.LoadScene(NextState.ToString("F"));
                CurrentState = NextState;
                NextState = eSceneType.SCENE_NONE;
                CompleteLoad(CurrentState);
            }
        }

	}

    void CompleteLoad(eSceneType _type)
    {
        switch (_type)
        {
            case eSceneType.SCENE_NONE:
                break;
            case eSceneType.SCENE_LOGO:
                break;
            case eSceneType.SCENE_GAME:
                {
                    GameManager.Instance.LoadGame();
                }
                break;
            case eSceneType.SCENE_LOBBY:
                {
                    LobbyManager.Instance.LoadLobby();
                    GameManager.Instance.GameInit();
                }
                break;
            default:
                break;
        }
    }

    void DisableScene(eSceneType _type)
    {

        // Scene UI 삭제
        UI_Tools.Instance.Clear();

        switch (_type)
        {
            case eSceneType.SCENE_NONE:
                break;
            case eSceneType.SCENE_LOGO:
                break;
            case eSceneType.SCENE_GAME:
                SkillManager.Instance.ClearSkill();
                break;
            case eSceneType.SCENE_LOBBY:
                LobbyManager.Instance.DisableLobby();
                break;
            default:
                break;
        }
    }


}
