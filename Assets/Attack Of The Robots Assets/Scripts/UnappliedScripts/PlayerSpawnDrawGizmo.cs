using UnityEngine;
using System.Collections;

public class EnemySpawnDrawGizmo : MonoBehaviour {

    void OnDrawGizmos() {
        Gizmos.DrawIcon(transform.position, "enemySpawn.png", true);
    }
}
