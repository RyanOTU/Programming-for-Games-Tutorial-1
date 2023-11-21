using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticUtility : MonoBehaviour
{
    public static readonly int PlayerLayerID = 1 << LayerMask.NameToLayer("Player");
    public static readonly int EnemyLayerID = 1 << LayerMask.NameToLayer("Enemy");
    public static readonly int GroundLayerID = 1 << LayerMask.NameToLayer("Ground");

    public static readonly int MoveLayer = PlayerLayerID | EnemyLayerID | GroundLayerID;
}
