using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio; 
using System;

public class ActivateSoundSettings : MonoBehaviour
{
    private void Start()
    {
        SoundHandler.Instance.UpdateSfx();
        SoundHandler.Instance.UpdateMusic();
    }
}