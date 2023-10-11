using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EventConfiner : MonoBehaviour
{
    public PolygonCollider2D confiner2;
    public GameObject camera;
    public CinemachineConfiner CM_Confiner;

    public GameObject gameManager;

    public int eventIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //CM_Confiner = gameObject.GetComponent<CinemachineVirtualCameraBase>().GetCinemachineComponent<CinemachineConfiner>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && eventIndex == 1)
        {
            CM_Confiner = camera.GetComponent<CinemachineConfiner>();
            CM_Confiner.m_BoundingShape2D = confiner2;

            gameManager.GetComponent<SwitchCharacterHandler>().enabled = true;
        }

        if (collision.CompareTag("Player") && eventIndex == 2)
        {
            gameManager.GetComponent<SwitchCharacterHandler>().enabled = true;
        }
    }
}
