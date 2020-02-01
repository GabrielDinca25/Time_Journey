using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAheadDown : MonoBehaviour
{
    // The Cinemaching Framing Transposer
    CinemachineFramingTransposer cvc;

    /// <summary>
    /// The method called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        cvc = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            if (cvc.m_ScreenY > 0.2f)
            {
                cvc.m_ScreenY -= 0.01f;
            }
        }
        else if (cvc.m_ScreenY < 0.5f)
        {
            cvc.m_ScreenY += 0.01f;
        }
        else
        {
            cvc.m_ScreenY = 0.5f;
        }
    }
}
