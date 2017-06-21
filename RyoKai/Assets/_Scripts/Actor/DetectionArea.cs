using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionArea : BaseObject
{
	public List<Actor> List_Actor = new List<Actor>();

	Actor AttachActor = null;
	CapsuleCollider CapColl;

	public void Init(Actor actor, float radius)
	{
		AttachActor = actor;
		CapColl = SelfComponent<CapsuleCollider>();
		if(CapColl == null)
		{
			CapColl = SelfObject.AddComponent<CapsuleCollider>();
		}
		CapColl.isTrigger = true;
		CapColl.radius = radius;
	}

	private void OnTriggerEnter(Collider other)
	{
		Actor actor = other.GetComponent<Actor>();
		if (actor == null)
			return;

		if (List_Actor.Contains(actor))
			return;

		// if(AttachActor.tea)
		List_Actor.Add(actor);
	}

	private void OnTriggerExit(Collider other)
	{
		Actor actor = other.GetComponent<Actor>();
		if (actor == null)
			return;

		if (List_Actor.Contains(actor))
			List_Actor.Remove(actor);
	}

	public Actor GetFirst()
	{
		Actor returnActor = null;

		while(returnActor == null)
		{
			if (List_Actor.Count <= 0)
				break;

			returnActor = List_Actor[0];

			//-- 예외처리
			if (returnActor == null)
				List_Actor.RemoveAt(0);
		}
		return returnActor;
	}





}
