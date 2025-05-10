using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CreditsScroll : MonoBehaviour
{
    private Scrollbar scrollBar;
    [SerializeField] bool hasTouchedScreen = false;
    [SerializeField] float scrollDownSpeed;
    
    void Start()
    {
        scrollBar = GetComponent<Scrollbar>();
        StartCoroutine(AutoScrollDown());
    }

    IEnumerator AutoScrollDown()
    {
        while (true)
        {
            while (!hasTouchedScreen)
            {
                scrollBar.value -= scrollDownSpeed * Time.deltaTime;
                yield return null;
            }
            yield return null;
        }

    }

    public void OnScreenPress(InputAction.CallbackContext context)
    {

    }

    public void OnScreenPressAndRelease(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            hasTouchedScreen = !hasTouchedScreen;
        }
    }
}
