using UnityEngine;
using System.Collections;

public class ClubThrowButtonHandler : MonoBehaviour
{
    [SerializeField] private Animator clubThrowAnimator;
    private bool canThrowClubs;

    private void OnEnable()
    {
        canThrowClubs = PlayerPrefs.GetInt("canThrowClubs") == 1;
        clubThrowAnimator.SetBool("isOn", canThrowClubs);
    }
}
