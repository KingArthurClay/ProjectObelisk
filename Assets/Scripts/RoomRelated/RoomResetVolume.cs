using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** https://docs.unity3d.com/ScriptReference/ExecuteInEditMode.html
 * Only does the things in the "edit" mode of the editor
 */

[ExecuteInEditMode] 
public class RoomResetVolume : MonoBehaviour
{

    [SerializeField] private templateRoomEnemyData _roomData;
    [SerializeField] private const string _ENEMY_TAG = "Enemy";
    [SerializeField] private LayerMask _ignoreMask;
    private HashSet<EnemyController> enemies;
    
    private BoxCollider _roomField;
    
    private void Awake() {
        enemies = new HashSet<EnemyController>();
        _roomField = GetComponent<BoxCollider>();
    }

    private void Update() {
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (!Application.isPlaying)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    private void OnTriggerEnter(Collider other) {
        //If we find a gameobject tagged "ENEMY" & it has an EnemyController, we add it to the spawnlist in the room data SerializableObject
        
        if (other.gameObject.CompareTag(_ENEMY_TAG) 
            && other.gameObject.GetComponent<EnemyController>() != null 
            && !enemies.Contains(other.gameObject.GetComponent<EnemyController>())) {
            enemies.Add(other.gameObject.GetComponent<EnemyController>());
            _roomData.enemySpawnPoints.Add(Transform.Instantiate<Transform>(other.gameObject.transform), GameObject.Instantiate(other.gameObject));
            other.gameObject.SetActive(false);
        }
    }

    /**
    private void OnTriggerExit(Collider other) {
        //If we find a gameobject tagged "ENEMY" & it has an EnemyController, we remove it from the spawnlist in the room data SerializableObject
        if (other.CompareTag(_ENEMY_TAG) && other.gameObject.GetComponent<EnemyController>() != null && enemies.Contains(other.gameObject.GetComponent<EnemyController>())) {
            enemies.Remove(other.gameObject.GetComponent<EnemyController>());
            _roomData.enemySpawnPoints.Remove(other.gameObject.transform);
        }
    }*/
}
