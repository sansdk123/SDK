using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    private CinemachineVirtualCamera VirtualCamera;
    private float ShakeTimer, StartingAmplitude, ShakeTimerTotal;

    void Start()
    {
        Instance = this;
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void FixedUpdate()
    {
        if (ShakeTimer > 0)
        {
            ShakeTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin Perlin = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            Perlin.m_AmplitudeGain = Mathf.Lerp(StartingAmplitude, 0f, (1 - (ShakeTimer / ShakeTimerTotal)));
        }
    }

    public void camerashake(float Amplitude, float time)
    {
        CinemachineBasicMultiChannelPerlin Perlin = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Perlin.m_AmplitudeGain = Amplitude;
        StartingAmplitude = Amplitude;
        ShakeTimer = time;
        ShakeTimerTotal = time;
    }

}
