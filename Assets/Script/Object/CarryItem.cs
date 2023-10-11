using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryItem : MonoBehaviour
{
    [Header("Box Sound")]
    public AudioClip boxDrop;
    private AudioSource boxSource;

    [Header("Box Parameter")]
    public GameObject itemBox;
    [SerializeField]
    public Vector3 offset;
    public Vector3 targetPosition;
    public Transform itemTransform;

    public GameObject[] itemList;

    private Collider objectCollider;

    public bool isCarrying;
    [SerializeField]
    public bool isFlip;
    // Start is called before the first frame update
    void Start()
    {
        itemList = GameObject.FindGameObjectsWithTag("Object");
    }

    // Update is called once per frame
    void Update()
    {
        FlipTransform();
        UpdatePosition();


        #region checkInput
        if (Input.GetKeyDown(KeyCode.K) && itemBox != null && isCarrying == false)
        {
            TakeItem();
        }
        else if (Input.GetKeyDown(KeyCode.L) && itemBox != null)
        {
            DropItem();
            boxSource = itemBox.GetComponent<AudioSource>();
            boxSource.PlayOneShot(boxDrop, 0.1f);
        }
        #endregion
    }

    public void FlipTransform()
    {
        isFlip = GetComponentInChildren<SpriteRenderer>().flipX;

        if (isFlip)
        {
            itemTransform.localPosition = new Vector3(1.6f, itemTransform.localPosition.y, 0);
        }

        else if (!isFlip)
        {
            itemTransform.localPosition = new Vector3(-1.6f, itemTransform.localPosition.y, 0);
        }
    }

    void TakeItem()
    {
        isCarrying = true;
        
        itemBox.GetComponent<BoxCollider2D>().enabled = false;
        itemBox.GetComponent<Rigidbody2D>().isKinematic = true;
        //itemBox.transform.Translate(gameObject.transform.position + offset);
    }

    void DropItem()
    {
        isCarrying = false;
        itemBox.transform.parent = null;
        itemBox.GetComponent<BoxCollider2D>().enabled = true;
        itemBox.GetComponent<Rigidbody2D>().isKinematic = false;
        //itemBox = null;
    }

    void UpdatePosition()
    {
        if (isCarrying)
        {
            itemBox.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            itemBox.transform.position = itemTransform.position;
            itemBox.transform.parent = GameObject.Find("Destination").transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Object") && isCarrying == false)
        {
            itemBox = collision.transform.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isCarrying == false)
        {
            itemBox = null;
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if(itemList[i] != null && itemList[i] != itemBox)
            {
                Gizmos.DrawWireSphere(itemList[i].transform.position,
            itemList[i].GetComponent<CircleCollider2D>().radius);

                if(itemBox != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(itemBox.transform.position,
                        itemBox.GetComponent<CircleCollider2D>().radius);
                }
            }
        }
    }
}
