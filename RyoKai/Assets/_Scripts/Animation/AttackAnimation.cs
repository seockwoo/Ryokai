using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : StateMachineBehaviour
{
	Actor TargetActor = null;
	bool bIsAttack = false;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		TargetActor = animator.GetComponentInParent<Actor>();
		if(TargetActor.AI.CURRENT_AI_STATE == eStateType.STATE_ATTACK)
		{
			TargetActor.AI.IS_ATTACK = true;
			bIsAttack = false;
		}
	}

	// 첫 번째와 마지막 프레임을 제외하고 각 업데이트 프레임에서 호출됩니다.
	public override void OnStateUpdate(
		Animator animator, AnimatorStateInfo animatorStateInfo,
		int layerIndex)
	{
        if (animatorStateInfo.normalizedTime >= 0.5f
            && TargetActor.AI.IS_ATTACK)
		{
			//BaseObject bo =
			//	animator.GetComponentInParent<BaseObject>();

			//bo.ThrowEvent("AttackEnd",eStateType.STATE_IDLE);
			if (TargetActor.AI.CURRENT_AI_STATE == eStateType.STATE_ATTACK)
				TargetActor.AI.IS_ATTACK = false;
		}

		if(bIsAttack == false 
			&& animatorStateInfo.normalizedTime >= 0.5f)
		{
			bIsAttack = true;
			TargetActor.RunSkill();
		}
	}


}
