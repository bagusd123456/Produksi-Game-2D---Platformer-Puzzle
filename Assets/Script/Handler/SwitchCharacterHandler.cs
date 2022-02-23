using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacterHandler : MonoBehaviour
{
    public Transform character;
    public List<Transform> possibleCharacters;
    public int characterId;
    // Start is called before the first frame update
    void Start()
    {
        if(character == null && possibleCharacters.Count >= 1)
        {
            character = possibleCharacters[0];
        }
        Swap();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(characterId == 0)
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
            if(characterId == possibleCharacters.Count - 1)
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
        character = possibleCharacters[characterId];
        character.GetComponent<PlayerController>().enabled = false;
        for (int i = 0; i < possibleCharacters.Count; i++)
        {
            if(possibleCharacters[i] != character)
            {
                possibleCharacters[i].GetComponent<PlayerController>().enabled = true;
            }
        }
    }
}
