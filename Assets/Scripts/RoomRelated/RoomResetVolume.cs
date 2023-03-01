using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomResetVolume : MonoBehaviour
{

    [SerializeField] private ScriptableObject roomData;
    
    private BoxCollider _roomField;
    private List<ItemResetHandler> _resetItems;
    private List<EnemyResetHandler> _resetEnemies;
    
    private void Start() {
        _roomField = GetComponent<BoxCollider>();
    }

    List<ItemResetHandler> getResetableItems() {
        

        return new List<ItemResetHandler>(); //Remove me you retard!
    }
}
