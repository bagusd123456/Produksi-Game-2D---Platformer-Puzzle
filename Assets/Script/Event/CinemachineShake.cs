using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera cmVirtualCamera;
    private float shakeTimer;

    public bool activateShake = false;

    private void Awake()
    {
        Instance = this;
        cmVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        activateShake = true;
        CinemachineBasicMultiChannelPerlin cmBasicMultiChannel = 
            cmVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cmBasicMultiChannel.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    public void StopShake()
    {
        activateShake = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f || activateShake == false)
            {
                //Timer Over!
                CinemachineBasicMultiChannelPerlin cmBasicMultiChannel =
                    cmVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cmBasicMultiChannel.m_AmplitudeGain = 0f;
            }
        }
        
    }
}
