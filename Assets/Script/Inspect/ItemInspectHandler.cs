using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInspectHandler : MonoBehaviour
{

    public GameObject tutorialPanel;
    public TMP_Text tutorialText;
    public Transform tutorialPoint;

    private bool isAble;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && isAble)
        {
            tutorialPanel.SetActive(true);
            tutorialText.text = "Item ini adalah Indomie.";
        }

        else if(Input.GetKeyDown(KeyCode.L) && !isAble && tutorialPanel != null && tutorialPanel.activeInHierarchy == true)
        {
            tutorialPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tutorialPanel != null && tutorialText != null && tutorialPoint != null)
        {
            if (collision.gameObject.CompareTag("Item"))
            {
                isAble = true;
                //Debug.Log("Tutorial 1");
            }

            if (collision.gameObject.CompareTag("Tips"))
            {
                tutorialPanel.SetActive(true);
                tutorialText.text = collision.gameObject.GetComponent<Text>().text;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tutorialPanel != null && tutorialText != null && tutorialPoint != null)
        {
            tutorialPanel.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            isAble = false;
        }
    }
}
