using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTargetNode : BTNode
{
    private EnemyCharacter enemyCharacter;
    private Transform targetTransform;
    private Vector3 targetPoint;
    private float frequencyOfRouteCalculation = 0.2f;
    private ElapsedTimeChecker elapsedTimeChecker;
    private NavMeshPath pathToTarget;

    public Transform Target { get => targetTransform; set => targetTransform = value; }
    public Vector3 TargetPoint { get => targetPoint; set => targetPoint = value; }

    public MoveToTargetNode(EnemyCharacter enemyCharacter, Transform target)
    {
        this.enemyCharacter = enemyCharacter;
        pathToTarget = new NavMeshPath();
        this.targetTransform = target;
        elapsedTimeChecker = new ElapsedTimeChecker(frequencyOfRouteCalculation);
    }
    public MoveToTargetNode(EnemyCharacter enemyCharacter, Vector3 targetPoint)
    {
        this.enemyCharacter = enemyCharacter;
        pathToTarget = new NavMeshPath();
        this.targetPoint = targetPoint;
        elapsedTimeChecker = new ElapsedTimeChecker(frequencyOfRouteCalculation);
    }


    public override NodeStates Evaluate()
    {
        MoveAlongTheWay();
        return NodeStates.SUCCESS;
    }

    private void MoveAlongTheWay()
    {
        FindWayToTarget();
        if(pathToTarget.corners.Length > 0)
        enemyCharacter.Move(pathToTarget.corners[1]);
    }
    private void FindWayToTarget()
    {
        if (elapsedTimeChecker.CheckElapsedTime())
        {
            
            NavMesh.CalculatePath(enemyCharacter.transform.position, targetTransform != null ? targetTransform.position : targetPoint, NavMesh.AllAreas, pathToTarget);
                

            elapsedTimeChecker.StartCountTime();
        }
        for (int i = 0; i < pathToTarget.corners.Length - 1; i++)
            Debug.DrawLine(pathToTarget.corners[i], pathToTarget.corners[i + 1], Color.red);

    }
}
