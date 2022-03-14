using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInspectHandler : MonoBehaviour
{

    public GameObject tutorialPanel;
    public TMP_Text tutorialText;
    public Transform tutorialPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            tutorialPanel.SetActive(true);
            tutorialText.text = "Tekan key 'A' atau 'D' untuk bergerak ke kiri atau kekanan.";
            //Debug.Log("Tutorial 1");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        tutorialPanel.SetActive(false);
    }
}
