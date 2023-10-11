using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public GameObject[] eventGameobject;
    public BoxCollider2D[] eventCollider;

    private void OnValidate()
    {
        eventGameobject = GameObject.FindGameObjectsWithTag("Event");

        /*for (int i = 0; i <= eventCollider.Length; i++)
        {
            eventCollider[i] = eventGameobject[i].GetComponent<BoxCollider2D>();
        }*/
    }

    private void OnDrawGizmos()
    {
        foreach (var item in eventGameobject)
        {
            if(eventCollider.Length > 0)
            {
                for (int i = 0; i < eventCollider.Length; i++)
                {
                    //eventCollider[i] = eventGameobject[i].GetComponent<BoxCollider2D>();

                    Gizmos.DrawCube(eventGameobject[i].transform.position, eventCollider[i].size);
                }
            }
        }
    }
    private void OnEnable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
