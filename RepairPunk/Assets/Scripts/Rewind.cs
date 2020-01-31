using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewind : MonoBehaviour
{
    public float rewindDuration;

    private List<Vector3> rewindPositions;
    private List<Quaternion> rewindRotations;

    private bool saveTransforms;
    private Rigidbody myRigidbody;
    private Transform myTransform;

    private float recordStartTime;
    private float rewindStartTime;

    private bool doRewind;

    private int rewindPosIndex;
    private int rewindQuatIndex;

    private void Awake()
    {
        rewindPositions = new List<Vector3>();
        rewindRotations = new List<Quaternion>();

        myRigidbody = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();
    }

    private void Update()
    {

        if (Time.time >= recordStartTime + rewindDuration && saveTransforms)
        {
            EndRewindRecording();
        }

        if (saveTransforms)
        {
            RecordTransform();
        }

        if (doRewind)
        {
            RewindToTransform();
        }
    }

    public void InitiateRewindRecording()
    {
        if (saveTransforms) return;

        RecordTransform();

        recordStartTime = Time.time;
        saveTransforms = true;

        myRigidbody.isKinematic = false;
    }

    public void EndRewindRecording()
    {
        saveTransforms = false;
        myRigidbody.isKinematic = true;
    }


    public void RewindObject()
    {
        EndRewindRecording();

        rewindPosIndex = rewindPositions.Count - 1;
        rewindQuatIndex = rewindRotations.Count - 1;

        doRewind = true;
    }

    public void EndRewinding()
    {
        rewindPositions.Clear();
        rewindRotations.Clear();
        doRewind = false;
    }

    private void RecordTransform()
    {
        rewindPositions.Add(myTransform.position);
        rewindRotations.Add(myTransform.rotation);
    }

    private void RewindToTransform()
    {
        myTransform.position = rewindPositions[rewindPosIndex];
        myTransform.rotation = rewindRotations[rewindQuatIndex];

        rewindPosIndex--;
        rewindQuatIndex--;

        if (rewindPosIndex < 0 || rewindQuatIndex < 0)
        {
            EndRewinding();
        }
    }
}
