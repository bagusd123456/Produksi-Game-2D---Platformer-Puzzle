using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlatform : MonoBehaviour
{
    private Material mat;
    [Header("Audio Paraameter")]
    public AudioSource audioSource;
    public AudioClip buttonSound;

    [Header("Button Parameter")]
    public bool useTimer = false;
    public float timerDuration = 2f;
    public float timer;
    public Collider2D colliderGO;
    public LayerMask layer;
    public float circleRadius;

    public Animator anim;
    public DoorHandler doorHandler;
    public bool enableButtonData;

    [ColorUsage(true,true)]
    public Color activeColor;
    [ColorUsage(true, true)]
    public Color nonActiveColor;

    [Header("Button Status")]
    public bool isActive;
    private bool defaultState;

    // Start is called before the first frame update
    void Start()
    {
        mat = new Material(gameObject.GetComponent<MeshRenderer>().material);
        defaultState = isActive;
    }

    // Update is called once per frame
    void Update()
    {
        SetColor();
        colliderGO = Physics2D.OverlapCircle(transform.position, circleRadius, layer);
        
        UseButton();
        if(anim != null)
        {
            if (isActive)
            {
                anim.SetBool("isOpen", true);
            }
            else 
            {
                anim.SetBool("isOpen", false);
            }
        }
        
    }

    void UseButton()
    {
        
        if (Input.GetKeyDown(KeyCode.K) && colliderGO != null && colliderGO.GetComponent<PlayerController>().enabled == true)
        {
            timer = timerDuration;
            if (!isActive)
            {
                PlaySound();
                isActive = true;
            }

            else
            {
                PlaySound();
                isActive = false;
            }
        }

        if (useTimer)
        {
            if(timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else if(timer <= 0)
            {
                isActive = defaultState;
            }
        }
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(buttonSound, 0.2f);
    }

    void SetColor()
    {
        gameObject.GetComponent<MeshRenderer>().material = mat;
        if (isActive)
        {
            mat.SetVector("_EmissionColor", (Vector4)activeColor);
        }
        else
        {
            mat.SetVector("_EmissionColor", (Vector4)nonActiveColor);
        }
    }

    private void OnDrawGizmos()
    {
        if(colliderGO == null)
        {
            Gizmos.color = Color.white;
        }

        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }
}
