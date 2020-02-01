using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindGroup : MonoBehaviour
{
    public float recordTime;
    private float recordStartTime;

    private List<RewindableObject> rewindableObjects;

    bool recordTransforms;
    bool rewindObjects;

    private void Awake()
    {
        rewindableObjects = new List<RewindableObject>();

        var childs = transform.GetComponentsInChildren<RewindableObject>();

        foreach(RewindableObject rewindObject in childs)
        {
            rewindableObjects.Add(rewindObject);
        }
    }

    private void Update()
    {
        if (Time.time >= recordStartTime + recordTime && recordTransforms)
        {
            recordTransforms = false;
            StopObjectRecording();
        }

        foreach(RewindableObject r in rewindableObjects)
        {
            r.CustomUpdate();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReleaseObjects();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            RecallObjects();
        }
    }
    
    public void StopObjectRecording()
    {
        foreach (RewindableObject rewindableObject in rewindableObjects)
        {
            rewindableObject.EndRewindRecording();
        }
    }

    public void ReleaseObjects()
    {
        recordStartTime = Time.time;
        recordTransforms = true;

        foreach(RewindableObject rewindableObject in rewindableObjects)
        {
            rewindableObject.InitiateRewindRecording();
        }
    }

    public void RecallObjects()
    {
        foreach (RewindableObject rewindableObject in rewindableObjects)
        {
            rewindableObject.RewindObject();
        }
    }

    public void HightlightForInteraction()
    {
        Debug.Log("Lit For Interaction");
    }

    public void RemoveHightlight()
    {
        Debug.Log("Unlit for no interaction yeeee boiii");
    }

}
