﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstValue 
{
	// JSON 관련
	public const string CharacterTemplatePath =
		"JSON/CHARACTER_TEMPLATE";
	public const string CharacterTemplateKey =
		"CHARACTER_TEMPLATE";

	public const string SkillTemplatePath = "JSON/SKILL_TEMPLATE";
	public const string SkillDataPath = "JSON/SKILL_DATA";

    


	// StatusData Key 관련
	public const string CharacterStatusDataKey =
		"CHARACTER_TEMPLATE";

	// GetData Key관련
	public const string ActorData_Team = "TEAM_TYPE";
	public const string ActorData_SetTarget = "SET_TARGET";
	public const string ActorData_GetTarget = "GET_TARGET";
	public const string ActorData_AttackRange = "ATTACK_RANGE";
	public const string ActorData_Character = "CHARACTER";
	public const string ActorData_Hit = "HIT";
	public const string ActorData_SkillData = "SKILL_DATA";

	// ThrowEvent Key 관련
	public const string EventKey_EnemyInit = "E_INIT";
	public const string EventKey_Hit = "E_HIT";
	public const string EventKey_SelectSkill = "SELECT_SKILL";
	public const string EventKey_SelectModel = "SELECT_SKILL_MODEL";

	// SetData Key 관련
	public const string SetData_HP = "BOARD_HP";
	public const string SetData_Damage = "BOARD_DAMAGE";

	// UI Path  관련
	public const string UI_PATH_HP = "Prefabs/UI/HP_Board";
	public const string UI_PATH_Damage = "Prefabs/UI/Damage_Board";

    //LocalSave
    public const string LocalSave_ItemInstance = "ITEM_INSTANCE";

}
