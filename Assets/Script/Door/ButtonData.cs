using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonData : MonoBehaviour
{
    [SerializeField] public bool isPressed;
    public Animator anim;
    public AudioSource audioSource;

    public bool invertInput = false;
    public bool hasPlayed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        bool value = isPressed;
        anim.SetBool("isPressed", value);
        if (anim.GetBool("isPressed") == true)
        {

        }
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || (collision.gameObject.CompareTag("Object")))
        {
            
            if (invertInput)
            {
                if(!hasPlayed)
                {
                    hasPlayed = true;
                    PlaySound();
                }
                isPressed = false;
            }
            else
            {
                if (!hasPlayed)
                {
                    hasPlayed = true;
                    PlaySound();
                }
                isPressed = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || (collision.gameObject.CompareTag("Object")))
        {
            if (invertInput)
            {
                isPressed = true;
                hasPlayed = false;
            }
            else
            {
                isPressed = false;
                hasPlayed = false;
            }
        }
    }
}
