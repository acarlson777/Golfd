using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Animator[] animatorsToTrigger;
    [SerializeField] private string dialogueToTrigger;
    [SerializeField] private GameObject gameObjectToBeActivated;
    [SerializeField] private GameObject gameObjectToBeHidden;
    [SerializeField] private bool oneTimeButton = false;
    private bool hasBeenPressed = false;

    private void Start(){
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenPressed && oneTimeButton) { return; }

        if (other.gameObject.CompareTag("GolfBall"))
        {
            hasBeenPressed = true;
            animator.SetBool("isOn", true);
            if (animatorsToTrigger.Length == 1){
                animatorsToTrigger[0].SetBool("isOn", true);
            } else {
                foreach (Animator animatorToTrigger in animatorsToTrigger)
                {
                    animatorToTrigger.SetBool("isOn", true);
                }
            }

            if (dialogueToTrigger != "")
            {
                WorldHandler.Instance.clubHandler.enabled = false;
                WorldHandler.Instance.GetDialogueWrapper().StartDialogueSequence(dialogueToTrigger, ()=> {
                    if (gameObjectToBeActivated != null) { gameObjectToBeActivated.SetActive(true); }
                    WorldHandler.Instance.clubHandler.enabled = true;
                });
            } else
            {
                if (gameObjectToBeActivated != null) { gameObjectToBeActivated.SetActive(true); }
                if (gameObjectToBeHidden != null) { gameObjectToBeHidden.SetActive(false); }
            }
        }
    }
}