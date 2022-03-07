using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Animator anim;
    private float timer;

    [Header("Door Setting")]
    public GameObject doorGameObject;
    [SerializeField] public float interactRadius = 40f;
    [SerializeField] public float doorTimer = 2f;

    [Header("Door Parameter")]
    public doorType _doorType = doorType.BASIC;
    [SerializeField] public enum doorType { BASIC, TRIGGER, BUTTON, KEY };
    [SerializeField] private bool isAble;    
    [SerializeField] private Collider[] colliderArray;
    
    private void Start()
    {
        anim = doorGameObject.GetComponent<Animator>();
        timer = doorTimer;
    }

    // Update is called once per frame
    void Update()
    {
        isAble = Physics.CheckSphere(gameObject.transform.position, interactRadius, 1 << 8);
        colliderArray = Physics.OverlapSphere(gameObject.transform.position, interactRadius, 1 << 8);

        if(_doorType == doorType.BASIC)
        {
            if (isAble) // open using button
            {
                anim.SetBool("isOpen", true);
                //timer -= Time.time;
            }
        }

        if (timer > 0) // auto open
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                anim.SetBool("isOpen", false);
            }
        }

        if (_doorType == doorType.TRIGGER && isAble)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                anim.SetBool("isOpen", true);
            }
        }

        if (_doorType == doorType.TRIGGER && isAble)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                anim.SetBool("isOpen", false);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer = doorTimer;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer = doorTimer;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        //anim.SetBool("isOpen", false);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < colliderArray.Length; i++)
        {
            if (isAble)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }
        }
        Gizmos.DrawWireSphere(gameObject.transform.position, interactRadius);
        
    }
}
