using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : BaseObject
{
    bool IsPlayer = false;
    public bool IS_PLAYER {
        get { return IsPlayer; }
        set { IsPlayer = value; }
    }

	[SerializeField]
	eTeamType TeamType;
	public eTeamType TEAM_TYPE
	{
		get
		{
			return TeamType ;
		}
	}

	[SerializeField]
	string TemplateKey = string.Empty;

	GameCharacter SelfCharacter = null;
	public GameCharacter SELF_CHARACTER
	{ get { return SelfCharacter; } }


	// AI
	BaseAI Ai = null;
	public BaseAI AI
	{ get { return Ai; } }


	BaseObject TargetObject = null;

    // Board -> HP Bar
    [SerializeField]
    bool bEnableBoard = true;


	private void Awake()
	{
		GameObject aiObject = new GameObject();
		aiObject.name = "NormalAI";
		Ai = aiObject.AddComponent<NormalAI>();
		aiObject.transform.SetParent(SelfTransform);

		// 없으면 동작  X
		Ai.Target = this;

		GameCharacter gameCharacter = CharacterManager.Instance.AddCharacter(TemplateKey);
		gameCharacter.TargetComponenet = this;
		SelfCharacter = gameCharacter;

		for(int i = 0; 
			i< gameCharacter.CHARACTER_TEMPLATE.LIST_SKILL.Count;
			i++)
		{
			SkillData data =
				SkillManager.Instance.GetSkillData(
				gameCharacter.CHARACTER_TEMPLATE.LIST_SKILL[i]);

			if (data == null)
			{
				Debug.LogError(
					gameCharacter.CHARACTER_TEMPLATE.LIST_SKILL[i]
					+ " 스킬 키를 찾을수 없습니다.");
			}
			else
				gameCharacter.AddSkill(data);
		}

        if(bEnableBoard)
        {
            BaseBoard board = BoardManager.Instance.AddBoard(this, eBoardType.BOARD_HP);

            board.SetData(ConstValue.SetData_HP, GetStatusData(eStatusData.MAX_HP), SelfCharacter.CURRENT_HP);

        }

		ActorManager.Instance.AddActor(this);
	}

	public double GetStatusData(eStatusData statusData)
	{
		return SelfCharacter.CHARACTER_STATUS.GetStatusData(statusData);
	}

	public override object GetData(string keyData, params object[] datas)
	{
		if (keyData == ConstValue.ActorData_Team)
			return TeamType;
		else if (keyData == ConstValue.ActorData_Character)
			return SelfCharacter;
		else if (keyData == ConstValue.ActorData_GetTarget)
			return TargetObject;
		else if(keyData == ConstValue.ActorData_SkillData)
		{
			int index = (int)datas[0];
			return SelfCharacter.GetSkillByIndex(index);
		}

		// Base 부모클래스 -> BaseObject
		return base.GetData(keyData, datas);
	}

	public override void ThrowEvent(string keyData, params object[] datas)
	{
		if(keyData == ConstValue.EventKey_Hit)
		{
			if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
				return;

			// 공격 주체의 케릭터
			GameCharacter casterCharacter
				= datas[0] as GameCharacter;
			SkillTemplate skillTemplate =
				datas[1] as SkillTemplate;

            //현재 상태는 기본 데미지 + 스킬 데미지
			casterCharacter.CHARACTER_STATUS.AddStatusData("SKILL", skillTemplate.STATUS_DATA);

			double attackDamage =
				casterCharacter
				.CHARACTER_STATUS
				.GetStatusData(eStatusData.ATTACK);
			// SelfCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.DEFFENCE);

			casterCharacter.CHARACTER_STATUS.RemoveStatusData("SKILL");

			SelfCharacter.IncreaseCurrentHP(-attackDamage);

            if(OBJECT_STATE == eBaseObjectState.STATE_DIE)
            {
                Debug.Log(SelfObject.name + " 죽음!");
                GameManager.Instance.KillCheck(this);
            }

            //HP Board
            BaseBoard board = BoardManager.Instance.GetBoardData(this, eBoardType.BOARD_HP);
            if (board != null)
                board.SetData(ConstValue.SetData_HP, GetStatusData(eStatusData.MAX_HP), SelfCharacter.CURRENT_HP);

            board = null;

            // DamageBoard
            board = BoardManager.Instance.AddBoard(this, eBoardType.BOARD_DAMAGE);
            if (board != null)
                board.SetData(ConstValue.SetData_Damage, attackDamage);


			// 피격 에니메이션
			AI.ANIMATOR.SetInteger("Hit", 1);
		}
		else if(keyData == ConstValue.EventKey_SelectSkill)
		{
			int index = (int)datas[0];
			SkillData data = SelfCharacter.GetSkillByIndex(index);
			SelfCharacter.SELECT_SKILL = data;
		}
		else if(keyData == ConstValue.ActorData_SetTarget)
		{
			TargetObject = datas[0] as BaseObject;
		}
		else
			base.ThrowEvent(keyData, datas);
	}

	protected virtual void Update()
	{
		AI.UpdateAI();
		if(AI.END)
		{
			Destroy(SelfObject);
		}
	}

	public void RunSkill()
	{
		SkillData selectSkill = SelfCharacter.SELECT_SKILL;
		if (selectSkill == null)
			return;

		for(int i = 0; i < selectSkill.SKILL_LIST.Count;i++)
		{
			SkillManager.Instance.RunSkill(this, selectSkill.SKILL_LIST[i]);
		}
	}

    private void OnEnable()
    {
        if(BoardManager.Instance != null)
        {
            BoardManager.Instance.ShowBoard(this, true);
        }
    }

    public void OnDisable()
	{
        if (BoardManager.Instance != null && GameManager.Instance.GAME_OVER == false)
        {
            BoardManager.Instance.ShowBoard(this, false);
        }
    }

	public void OnDestroy()
	{
        if (BoardManager.Instance != null)
        {
            BoardManager.Instance.ClearBoard(this);
        }

        if (ActorManager.Instance != null)
		{
			ActorManager.Instance.RemoveActor(this);
		}
	}

}
