using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AttachGun : MonoBehaviour
{
    public XRGrabInteractable interactable;
    public XRDirectInteractor interactor;
    // Start is called before the first frame update
    void Start()
    {
        interactor = GetComponentInChildren<XRDirectInteractor>();

        if(interactor != null && interactable != null)
        {
            StartCoroutine(AutoGrabObject()); 
        }
    }

    private IEnumerator AutoGrabObject()
    {
        // Wait a frame to ensure everything is initialized
        yield return null;

        // Manually select the interactable object
        interactor.interactionManager.SelectEnter(interactor, interactable);
    }
}
