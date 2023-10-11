using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlatform : MonoBehaviour
{
    [SerializeField] public bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || (collision.gameObject.CompareTag("Object")))
        {
            isPressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || (collision.gameObject.CompareTag("Object")))
        {
            isPressed = false;
        }
    }

}
