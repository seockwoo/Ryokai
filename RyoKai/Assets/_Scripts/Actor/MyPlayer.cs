using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyPlayer : Actor {

    NavMeshAgent Agent;
    private void Start()
    {
        IS_PLAYER = true;
        Agent = SelfComponent<NavMeshAgent>();
    }

    //protected override void Update()
    //{
    //    if (Stick.IsPressed)
    //    {
    //        Vector3 movePosition = transform.position;
    //        movePosition += new Vector3(Stick.Axis.x, 0, Stick.Axis.y);

    //        AI.ClearAI();
    //        Agent.Resume();
    //        Agent.SetDestination(movePosition);
    //    }
    //    else
    //        base.Update();
    //}
}
