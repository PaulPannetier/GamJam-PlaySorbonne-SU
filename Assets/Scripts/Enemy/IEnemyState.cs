using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using System.Collections;
using UnityEditorInternal;
using Unity.Collections;


public interface IEnemyState
{
    void EnterState(EnemyController enemy);
    void UpdateState(EnemyController enemy);
    void ExitState(EnemyController enemy);
}