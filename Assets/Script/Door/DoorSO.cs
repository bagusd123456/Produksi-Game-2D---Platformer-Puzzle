using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum doorType { BASIC, TRIGGER, BUTTON, KEY }

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DoorScriptableObject", order = 1)]

public class DoorSO : ScriptableObject
{
    [Header("Door Type")]
    public doorType door;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
