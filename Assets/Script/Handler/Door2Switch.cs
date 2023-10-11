using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2Switch : DoorHandler
{
    public ButtonData[] activeButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_doorType == doorType.BUTTON && buttonDataArray != null)
        {
            if(activeButton[0].isPressed && activeButton[1].isPressed)
            {
                OpenDoor();
            }
            else
            {
                CloseDoor();
            }
        }
    }
}
