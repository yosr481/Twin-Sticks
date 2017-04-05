using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour {

    const int bufferFrames = 100;
    MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];

    Rigidbody rigid;
	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Record();
	}

    void Record()
    {
        rigid.isKinematic = false;
        int frame = Time.frameCount % bufferFrames;
        float time = Time.time;
        print("Writing frame: " + frame);

        keyFrames[frame] = new MyKeyFrame(time, transform.position, transform.rotation);
    }

    public void Playback()
    {
        rigid.isKinematic = true;
        int frame = Time.frameCount % bufferFrames;
        Debug.Log("Reading frame: " + frame);
        transform.position = keyFrames[frame].myPos;
        transform.rotation = keyFrames[frame].myRot;

    }
}

/// <summary>
/// A structure to storing a time float, position and rotation of a gameObject.
/// </summary>
public struct MyKeyFrame
{
    public float myTime;
    public Vector3 myPos;
    public Quaternion myRot;

    public MyKeyFrame(float time, Vector3 pos, Quaternion rot)
    {
        myTime = time;
        myPos = pos;
        myRot = rot;
    }
}
