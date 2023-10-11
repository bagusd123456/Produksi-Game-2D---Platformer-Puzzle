using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCharacterHandler : MonoBehaviour
{
    public Transform characterActive;
    public List<Transform> possibleCharacters;
    [SerializeField]
    public int characterId;
    public PhysicsMaterial2D fullFriction;
    public PhysicsMaterial2D noFriction;

    public CinemachineVirtualCamera cmCamera;

    private void OnValidate()
    {
        if (characterActive == null && possibleCharacters.Count >= 1)
        {
            characterActive = possibleCharacters[0];
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Swap();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (characterId == 0)
            {
                characterId = possibleCharacters.Count - 1;
            }
            else
            {
                characterId -= 1;
            }
            Swap();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (characterId == possibleCharacters.Count - 1)
            {
                characterId = 0;
            }
            else
            {
                characterId += 1;
            }
            Swap();
        }
    }
    public void Swap()
    {
        if(characterActive != null)
        {
            characterActive.GetComponent<Rigidbody2D>().velocity = Vector2.zero; //Set Movement to Idle
            characterActive.GetComponentInChildren<Animator>().SetInteger("Velocity", 0); //Set Animation to Idle
        }
        characterActive = possibleCharacters[characterId];
        //character.GetComponent<PlayerController>().enabled = false;
        for (int i = 0; i < possibleCharacters.Count; i++)
        {
            possibleCharacters[i].GetComponent<PlayerController>().enabled = false;
            possibleCharacters[i].GetComponent<BoxCollider2D>().isTrigger = false;
            possibleCharacters[i].GetComponent<CapsuleCollider2D>().isTrigger = true;
            possibleCharacters[i].GetComponent<Rigidbody2D>().sharedMaterial = fullFriction;
            possibleCharacters[i].GetComponent<Rigidbody2D>().mass = 100f;
            if (possibleCharacters[i] == characterActive)
            {
                possibleCharacters[i].GetComponent<PlayerController>().enabled = true;
                possibleCharacters[i].GetComponent<BoxCollider2D>().isTrigger = true;
                possibleCharacters[i].GetComponent<CapsuleCollider2D>().isTrigger = false;
                possibleCharacters[i].GetComponent<Rigidbody2D>().sharedMaterial = noFriction;
                possibleCharacters[i].GetComponent<Rigidbody2D>().mass = 1f;
                cmCamera.Follow = possibleCharacters[i];
            }
        }
    }
}
