using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeEffects : MonoBehaviour
{
    [SerializeField] CinemachineImpulseSource impulse;
    [SerializeField] Vector3 shakeDirection;


    public void ScreenShake(int facingDirection)
    {
        impulse.m_DefaultVelocity = new Vector3(shakeDirection.x , 1* facingDirection, shakeDirection.y);
        impulse.GenerateImpulse();
    }
}
