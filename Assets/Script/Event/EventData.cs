using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventData : MonoBehaviour
{
    [Header("TriggerEvent Parameter")]
    public Collider2D[] playerList;
    public LayerMask playerLayer;
    public targetLevel _targetLevel = targetLevel.TUTORIAL;
    [SerializeField] public enum targetLevel {NEXTLEVEL, TUTORIAL, LEVEL1, LEVEL2, LEVEL3 };

    [Space]
    public Animator canvasAnimator;
    public bool bothPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();

        if (_targetLevel == targetLevel.NEXTLEVEL)
        {
            if(canvasAnimator != null)
            {
                if (!bothPlayer && playerList.Length == 1)
                {
                    canvasAnimator.SetTrigger("fadeOut");
                    if (canvasAnimator.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
                    {
                        NextLevel();
                    }
                }
                else if (bothPlayer && playerList.Length == 2)
                {
                    canvasAnimator.SetTrigger("fadeOut");
                    if (canvasAnimator.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
                    {
                        NextLevel();
                    }
                }
            }

            else
            {
                if (!bothPlayer && playerList.Length == 1)
                {
                    NextLevel();
                }
                else if (bothPlayer && playerList.Length == 2)
                {
                    NextLevel();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(gameObject.transform.position, gameObject.GetComponent<BoxCollider2D>().size);
    }

    void CheckCollision()
    {
        playerList = Physics2D.OverlapBoxAll(gameObject.transform.position, gameObject.GetComponent<BoxCollider2D>().size, 90f, playerLayer);
        
    }

    void LevelTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
