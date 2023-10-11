using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EventFinale : MonoBehaviour
{
    public float shakeIntensity = 5f;
    public float shakeTime = 5f;

    public Animator animator;
    public PlayableDirector timeline;
    public Collider2D[] playerList;
    public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void LateUpdate()
    {
        CheckCollision();
        if(playerList.Length == 2)
        {
            animator.enabled = true;
            timeline.Play();
        }
    }

    public void startShake()
    {
        CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTime);
    }

    void CheckCollision()
    {
        playerList = Physics2D.OverlapBoxAll(gameObject.transform.position, gameObject.GetComponent<BoxCollider2D>().size, 90f, playerLayer);

    }

}
