using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlatform : MonoBehaviour
{
    public List<GameObject> targetPositionList = new List<GameObject>();
    public GameObject targetPosition;
    public GameObject targetGO;
    public bool canTeleport = false;

    public float duration = 3f;
    public float timeRemaining = 0;
    public int positionIndex;

    public Color onColor;
    public Color32 offColor;

    // Start is called before the first frame update
    void Start()
    {
        if(targetPosition == null)
        {
            targetPosition = targetPositionList[0];
            //targetPosition = targetGO.transform.position;
        }
        if(timeRemaining <= 0)
        {
            timeRemaining = duration;
        }
    }

    private void Update()
    {
        if (canTeleport)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
            }
        }

        for (int i = 0; i < targetPositionList.Count; i++)
        {
            if(targetPosition == targetPositionList[i])
            {
                targetPositionList[i].GetComponent<SpriteRenderer>().color = Color.white;
                targetPositionList[i].GetComponent<TeleportPlatform>().enabled = true;
            }
            else
            {
                targetPositionList[i].GetComponent<SpriteRenderer>().color = Color.gray;
                targetPositionList[i].GetComponent<TeleportPlatform>().enabled = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            canTeleport = true;

            if (timeRemaining <= 0)
            {
                collision.transform.position = targetPosition.transform.position;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canTeleport = false;
        timeRemaining = 3f;
    }

    public void SwitchTarget()
    {
        if (positionIndex < targetPositionList.Count - 1)
            positionIndex++;
        else
            positionIndex = 0;
            targetPosition = targetPositionList[positionIndex];
    }

    private void OnEnable()
    {
        //gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    private void OnDisable()
    {
        //gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
