using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public Animator anim;
    public float timer;
    [SerializeField]
    public ButtonData buttonData;

    [Header("Door Setting")]
    public GameObject doorGameObject;
    [SerializeField] public float interactRadius = 40f;
    [SerializeField] public float doorTimer = 2f;

    [Header("Door Parameter")]
    public doorType _doorType = doorType.BASIC;
    [SerializeField] public enum doorType { BASIC, TRIGGER, BUTTON, KEY };
    [SerializeField] public bool isAble;    
    [SerializeField] public Collider2D[] colliderArray;
    [SerializeField] public GameObject[] buttonDataArray;
    public bool flip = false;

    [Header("Sound Parameter")]
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;
    private void Start()
    {
        if (audioSource == null)
            audioSource = this.gameObject.GetComponent<AudioSource>();
        anim = doorGameObject.GetComponent<Animator>();
        timer = doorTimer;
        buttonDataArray = GameObject.FindGameObjectsWithTag("Button");
        
        /*if(buttonDataArray.Length != 0)
        {
            buttonData = buttonDataArray[0].GetComponent<ButtonData>();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        isAble = Physics2D.OverlapCircle(gameObject.transform.position, interactRadius, 1 << 8);
        colliderArray = Physics2D.OverlapCircleAll(gameObject.transform.position, interactRadius, 1 << 8);
        //isAble = Physics.CheckSphere(gameObject.transform.position, interactRadius, 1 << 8);
        //colliderArray = Physics.OverlapSphere(gameObject.transform.position, interactRadius, 1 << 8);

        if (_doorType == doorType.BASIC)
        {
            if (isAble) // on Area Can Interact
            {
                anim.SetBool("isOpen", true);
                //timer -= Time.time;
            }
            else // auto close
            {
                if(timer >= 0f)
                {
                    timer -= Time.deltaTime;
                }

                else if (timer <= 0f)
                {
                    anim.SetBool("isOpen", false);
                }
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

        if (_doorType == doorType.BUTTON && buttonData != null && buttonData.isPressed == true)
        {
            timer = doorTimer;
            if(timer >= 0)
            {
                OpenDoor();
            }
            //anim.SetBool("isOpen", true);
        }

        if (_doorType == doorType.BUTTON && buttonData != null && buttonData.isPressed == false)
        {
            if(timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else if (timer <= 0f)
            {
                CloseDoor();
            }
            //anim.SetBool("isOpen", false);
        }

        if (flip)
        {
            if (anim.GetBool("isOpen"))
            {
                anim.SetBool("isOpen", false);
            }
            else
            {
                anim.SetBool("isOpen", true);
            }
        }
    }

    public void OpenDoor()
    {
        anim.SetBool("isOpen", true);
    }

    public void CloseDoor()
    {
        anim.SetBool("isOpen", false);
    }

    public void PlaySound(int number)
    {
        if(number == 0)
        {
            audioSource.PlayOneShot(openSound, 0.4f);
        }
        else if(number == 1)
        {
            audioSource.PlayOneShot(closeSound, 0.4f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timer = doorTimer;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timer = doorTimer;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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
