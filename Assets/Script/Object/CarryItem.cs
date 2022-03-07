using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryItem : MonoBehaviour
{
    public GameObject itemBox;
    [SerializeField]
    public Vector3 offset;
    public Transform itemTransform;

    public GameObject[] itemList;

    private Collider objectCollider;

    public bool isCarrying;
    // Start is called before the first frame update
    void Start()
    {
        itemList = GameObject.FindGameObjectsWithTag("Object");
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();


        #region checkInput
        if (Input.GetKeyDown(KeyCode.K) && itemBox != null)
        {
            TakeItem();
        }
        else if (Input.GetKeyDown(KeyCode.L) && itemBox != null)
        {
            DropItem();
        }
        #endregion
    }

    void TakeItem()
    {
        isCarrying = true;
        
        itemBox.GetComponent<CapsuleCollider>().enabled = false;
        itemBox.GetComponent<Rigidbody>().useGravity = false;
        //itemBox.transform.Translate(gameObject.transform.position + offset);
    }

    void DropItem()
    {
        isCarrying = false;
        itemBox.transform.parent = null;
        itemBox.GetComponent<Rigidbody>().useGravity = true;
        itemBox.GetComponent<CapsuleCollider>().enabled = true;
        //itemBox = null;
    }

    void UpdatePosition()
    {
        if (isCarrying)
        {
            itemBox.GetComponent<Rigidbody>().velocity = Vector3.zero;
            itemBox.transform.position = itemTransform.position;
            itemBox.transform.parent = GameObject.Find("Destination").transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            itemBox = other.transform.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isCarrying)
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
                Gizmos.DrawWireCube(itemList[i].transform.position,
            itemList[i].GetComponent<BoxCollider>().size);

                if(itemBox != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireCube(itemBox.transform.position,
                        itemBox.GetComponent<BoxCollider>().size);
                }
            }
        }
    }
}
