using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPausePanelAnimation : MonoBehaviour
{
    [SerializeField]
    GameObject imageBackgroundOverlay;
    [SerializeField]
    Animator animator;

    public void OnAppear()
    {
        imageBackgroundOverlay.SetActive(true);

        gameObject.SetActive(true);

        animator.SetTrigger("onAppear");
    }

    public void OnDisappear()
    {
        animator.SetTrigger("OnDisappear");
    }

    public void EndOfDisappear()
    {
        imageBackgroundOverlay.SetActive(false);

        gameObject.SetActive(false);
    }
}

