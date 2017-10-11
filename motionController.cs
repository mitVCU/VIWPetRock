using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Kinect;

public class motionController : MonoBehaviour {

    //we are going to create a custome gesture using the kinect ! 
    //the GETJOINTPOSITIONDEMO.cs was a real help 



    // Use this for initialization
  
    private KinectInterop.JointType head = KinectInterop.JointType.SpineShoulder; //we used the KinectInterop which holds all the information and handles the data , we called information from the joint class, specifically the right hand
    private KinectInterop.JointType hip = KinectInterop.JointType.SpineBase;
    private KinectInterop.JointType leftHand = KinectInterop.JointType.HandLeft;
    private KinectInterop.JointType rightHand = KinectInterop.JointType.HandRight;

    public Vector3 headJointPosition;
    public Vector3 hipjointPosition;
    public Vector3 leftHand;
    public Vector3 rightHand;

    private long trackedUsersId;
    public float speed = 10.0f;


	void Start () {
        	
	}

    void checkPosture() {
        KinectManager manager = KinectManager.Instance;
        trackedUsersId = manager.GetPrimaryUserID();
        if (manager.IsUserDetected() && manager.IsUserTracked(trackedUsersId))
        {
            print("user detected");
            if (manager.IsJointTracked(trackedUsersId, (int)head) && manager.IsJointTracked(trackedUsersId, (int)hip))
            {
                print("got the joints .. of the body");
                headJointPosition = manager.GetJointPosition(trackedUsersId, (int)head);//change the headJointPosition to the position of the current head joint's position

                hipjointPosition = manager.GetJointPosition(trackedUsersId, (int)hip);// change the hipJointPosition to the position of the current jip joint's position

                rightHandPosition = manager.GetJointPosition(trackedUsersId, (int)rightHand);// gets current right hand position

                leftHandPosition = manager.GetJointPosition(trackedUsersId, (int)leftHand);

                //now we want to use our imagination and imagine us performing the gesture, so we want to move the cube whenever we lean forward.. We are using information
                // that deals with the head and hip.. 
                // so when we think of leaning forward, assuming we have perfect posture, we visualize the head of the person being further outward then the hip. 
                // so now we have to use some knowledge of x, y and z coordinates.. 

                // using the XYZ mode of the person we see that we would be interested in testing the y axis of the head and the hip 
                //in theory: if we lean forward, our head's y position will be more than our hips y position
                //so lets try and highlight that in our code 

                if ((headJointPosition.z - hipjointPosition.z) < -0.5)
                {
                    gameObject.transform.Translate(0, 0, -(speed * Time.deltaTime));// we only need it to move in the y position soo we only change the y position, to make it more natural speed we multiply speed by Time.deltaTime
                    // also gameObject is the specific object this script is attached to, GameObject is a variable type 
                    // so im moving the object this script is attached to
                    print(headJointPosition.z - hipjointPosition.z);
                    print("going forward");
            



                else if ((headJointPosition.z - hipjointPosition.z) > 0.6)
                {
                    gameObject.transform.Translate(0, 0, (speed * Time.deltaTime));
                    print(headJointPosition.z - hipjointPosition.z);
                    print("going backward");
                }
                else
                {
                    gameObject.transform.Translate(0, 0, 0);
                    print(headJointPosition.z - hipjointPosition.z);

                    print("not moving");
                }


                //in here we have access to the joint's positon, and its a variable type called Vector3. This means we have access to the 3D coordinates of the joint..



            }//if the joint is tracked ny this user.. 

        }//if the kinect senses atleast a user and if the user is the primary user id, or the first sensed user
    }

	
	// Update is called once per frame
	void Update () {
        checkPosture();
       

    }
}
