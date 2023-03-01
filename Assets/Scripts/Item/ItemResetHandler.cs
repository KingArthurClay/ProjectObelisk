using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemResetHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject resetObject;

    public void reset() {
        Instantiate(resetObject);
    }
}
