using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    public float interactionDistance;
    private GameObject lastHighlightedObject;
    private RewindGroup lastRewindGroup;
    

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            if(hit.transform.CompareTag("RewindGroup"))
            {
                if(hit.transform.gameObject != lastHighlightedObject)
                {
                    lastHighlightedObject = hit.transform.gameObject;
                    lastRewindGroup = lastHighlightedObject.GetComponent<RewindGroup>();
                    lastRewindGroup.HightlightForInteraction();
                }
               
            }
            else
            {
                if(lastRewindGroup)
                {
                    lastRewindGroup.RemoveHightlight();
                    lastRewindGroup = null;
                }
                lastHighlightedObject = null;
                    
            }
        }
        else
        {
            if(lastRewindGroup)
            {
                lastRewindGroup.RemoveHightlight();
                lastRewindGroup = null;
            }
            lastHighlightedObject = null;
        }

        if(lastRewindGroup)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                lastRewindGroup.RecallObjects();
                lastRewindGroup = null;
            }
        }
    }
}
