using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NextAI
{
	public eStateType StateType;
	public BaseObject TargetObject;
	public Vector3 Position;
}

public class BaseAI : BaseObject
{
	protected List<NextAI> ListNextAI = new List<NextAI>();
	protected eStateType CurrentAIState = eStateType.STATE_IDLE;
	public eStateType CURRENT_AI_STATE
	{
		get { return CurrentAIState; }
	}

	bool bUpdateAI = false;
	bool bAttack = false;
	public bool IS_ATTACK
	{
		get { return bAttack; }
		set { bAttack = value; }
	}

	bool bEnd = false;
	public bool END
	{
		get { return bEnd; }
		set { bEnd = value; }
	}

	protected Vector3 MovePosition = Vector3.zero;
	Vector3 PreMovePosition = Vector3.zero;

	Animator Anim = null;
	NavMeshAgent NavAgent = null;

	public Animator ANIMATOR
	{
		get
		{
			if (Anim == null)
			{
				Anim = SelfObject.GetComponentInChildren<Animator>();
			}
			return Anim;
		}
	}

	public NavMeshAgent NAV_MESH_AGENT
	{
		get
		{
			if (NavAgent == null)
			{
				NavAgent = SelfObject.GetComponent<NavMeshAgent>();
			}
			return NavAgent;
		}
	}

	void ChangeAnimation()
	{
		if (ANIMATOR == null)
		{
			Debug.LogError(SelfObject.name + " 에게 Animator가 없습니다.");
			return;
		}

		ANIMATOR.SetInteger("State", (int)CurrentAIState);
	}

	public virtual void AddNextAI(eStateType nextStateType,
		BaseObject targetObject = null,
		Vector3 position = new Vector3())
	{
		NextAI nextAI = new NextAI();
		nextAI.StateType = nextStateType;
		nextAI.TargetObject = targetObject;
		nextAI.Position = position;

		ListNextAI.Add(nextAI);
	}

	protected virtual void ProcessIdle()
	{
		CurrentAIState = eStateType.STATE_IDLE;
		ChangeAnimation();
	}

	protected virtual void ProcessMove()
	{
		CurrentAIState = eStateType.STATE_WALK;
		ChangeAnimation();
	}

	protected virtual void ProcessAttack()
	{
		Target.ThrowEvent(ConstValue.EventKey_SelectSkill, 0);
		CurrentAIState = eStateType.STATE_ATTACK;
		ChangeAnimation();
	}

	protected virtual void ProcessDie()
	{
		CurrentAIState = eStateType.STATE_DEAD;
		ChangeAnimation();
	}

	protected virtual IEnumerator Idle()
	{
		bUpdateAI = false;
		yield break;
	}

	protected virtual IEnumerator Move()
	{
		bUpdateAI = false;
		yield break;
	}

	protected virtual IEnumerator Attack()
	{
		bUpdateAI = false;
		yield break;
	}

	protected virtual IEnumerator Die()
	{
		bUpdateAI = false;
		yield break;
	}

	void SetNextAI(NextAI nextAI)
	{
		if (nextAI.TargetObject != null)
		{
			Target.ThrowEvent(ConstValue.ActorData_SetTarget,
				nextAI.TargetObject);
		}

		if (nextAI.Position != Vector3.zero)
			MovePosition = nextAI.Position;

		switch (nextAI.StateType)
		{
			case eStateType.STATE_IDLE:
				ProcessIdle();
				break;
			case eStateType.STATE_ATTACK:
				{
					if (nextAI.TargetObject != null)
					{
						SelfTransform.forward =
							(nextAI.TargetObject.SelfTransform.position
							- SelfTransform.position).normalized;
					}
					ProcessAttack();
				}
				break;
			case eStateType.STATE_WALK:
				ProcessMove();
				break;
			case eStateType.STATE_DEAD:
				ProcessDie();
				break;
		}
	}


	public void UpdateAI()
	{
		if (bUpdateAI == true)
			return;

		if (ListNextAI.Count > 0)
		{
			SetNextAI(ListNextAI[0]);
			ListNextAI.RemoveAt(0);
		}

		if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
		{
			ListNextAI.Clear();
			ProcessDie();
		}

		bUpdateAI = true;

		switch (CurrentAIState)
		{
			case eStateType.STATE_IDLE:
				StartCoroutine("Idle");
				break;
			case eStateType.STATE_ATTACK:
				StartCoroutine("Attack");
				break;
			case eStateType.STATE_WALK:
				StartCoroutine("Move");
				break;
			case eStateType.STATE_DEAD:
				StartCoroutine("Die");
				break;
		}
	}

	public void ClearAI()
	{
		ListNextAI.Clear();
	}


	public void ClearAI(eStateType stateType)
	{
		// #1
		//List<int> removeIndex = new List<int>();
		//for(int i = 0;i< ListNextAI.Count; i++)
		//{
		//	if (ListNextAI[i].StateType == stateType)
		//		removeIndex.Add(i);
		//}

		//for(int i = 0; i < removeIndex.Count; i++)
		//{
		//	ListNextAI.RemoveAt(removeIndex[i]);
		//}
		//removeIndex.Clear();

		// #2 Predicate 메서드를 이용한 삭제
		//tempState = stateType;
		//ListNextAI.RemoveAll(RemovePredicate);

		// #3 Lamda식 사용
		// () => {} Lamda
		ListNextAI.RemoveAll(
			(nextAI) => {
				return nextAI.StateType == stateType; }
			);

		//ListNextAI.RemoveAll(
		//	(nextAI) => nextAI.StateType == stateType
		//	);


	}

	//// #2
	//eStateType tempState;
	//public bool RemovePredicate(NextAI nextAI)
	//{
	//	return nextAI.StateType == tempState;
	//}

	protected bool MoveCheck()
	{
		if (NAV_MESH_AGENT.pathStatus == NavMeshPathStatus.PathComplete)
		{
			if (NAV_MESH_AGENT.hasPath == false || NAV_MESH_AGENT.pathPending == false)
			{
				return true;
			}
		}

		return false;
	}

	protected void SetMove(Vector3 position)
	{
		if (PreMovePosition == position)
			return;

		PreMovePosition = position;
		NAV_MESH_AGENT.Resume();
		NAV_MESH_AGENT.SetDestination(position);
	}

	protected void Stop()
	{
		MovePosition = Vector3.zero;
		NAV_MESH_AGENT.Stop();
	}

}