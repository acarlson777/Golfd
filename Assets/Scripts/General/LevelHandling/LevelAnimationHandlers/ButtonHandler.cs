﻿using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Animator[] animatorsToTrigger;
    [SerializeField] private string dialogueToTrigger;

    private void Start(){
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            animator.SetBool("isOn", true);
            foreach (Animator animatorToTrigger in animatorsToTrigger)
            {
                animatorToTrigger.SetBool("isOn", true);
            }
            if (dialogueToTrigger != "")
            {
                WorldHandler.Instance.GetDialogueWrapper().StartDialogueSequence(dialogueToTrigger, ()=> { });
            }
        }
    }
}
