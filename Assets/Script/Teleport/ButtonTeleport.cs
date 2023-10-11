using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTeleport : MonoBehaviour
{
    public TeleportPlatform teleportPlatform;

    [Header("Button Parameter")]
    public float circleRadius = 1f;
    public LayerMask layerCollider;
    public bool collideDoor = false;

    [Header("Sound Parameter")]
    public AudioSource audioSource;
    public AudioClip buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        collideDoor = Physics2D.OverlapCircle(transform.position, circleRadius,layerCollider);

        if (collideDoor)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                audioSource.PlayOneShot(buttonSound, 0.2f);
                teleportPlatform.SwitchTarget();
            }
        }
    }
}
