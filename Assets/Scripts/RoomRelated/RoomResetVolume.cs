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
    
    private BoxCollider _roomField;
    
    private void Start() {
        _roomField = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other) {
        //If we find a gameobject tagged "ENEMY" & it has an EnemyController, we add it to the spawnlist in the room data SerializableObject
        if (other.CompareTag(_ENEMY_TAG) && other.gameObject.GetComponent<EnemyController>() != null) {
            _roomData.enemySpawnPoints.Add(other.gameObject.transform, other.gameObject.GetComponent<EnemyController>() != null);
        }
    }

    private void OnTriggerExit(Collider other) {
        
    }
}
