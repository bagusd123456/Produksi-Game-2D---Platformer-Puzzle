using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    private AudioSource sfx;
    [SerializeField]
    public float volumeBGM = 0.8f;
    public GameObject volumeSlider;
    
    [Header("Test Main Menu")]
    public AudioClip mainMenu;

    [Header("In Game Sound")]
    public AudioClip ambiance;
    public AudioClip walking;
    public AudioClip jumping;

    
    // Start is called before the first frame update
    void Start()
    {
        if(audioSource == null)
        {
            audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }
        
        if(mainMenu != null)
        {
            audioSource.clip = mainMenu;
            audioSource.Play();
        }

        if(volumeSlider != null)
        {
            volumeSlider.GetComponentInChildren<Slider>().value = volumeBGM;
        }

        if (ambiance != null)
        {
            if(sfx == null)
                sfx = gameObject.AddComponent<AudioSource>();
            if(!sfx.isPlaying)
            {
                sfx.PlayOneShot(ambiance, (volumeBGM - 0.4f));
            }    
                
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(volumeSlider.activeInHierarchy == true)
        {
            volumeBGM = GameObject.FindGameObjectWithTag("BGM").GetComponentInChildren<Slider>().value;
        }

        if(audioSource != null)
        {
            audioSource.clip = mainMenu;
            if(audioSource.isPlaying == false)
            {
                audioSource.PlayDelayed(0.2f);
            }
        }
        
        audioSource.volume = volumeBGM;
    }
}
