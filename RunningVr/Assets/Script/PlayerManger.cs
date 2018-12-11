using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class PlayerManger : MonoBehaviour
{

    protected OVRCameraRig CameraRig = null;
    public float speed;
    public Transform pos;

    private float SimulationRate = 60f;
    public float RotationRatchet = 90.0f;
    public float SideRate = 10.0f;
    public bool SnapRotation = true;
    public bool SnapSide = true;

    public float RotationAmount = 1.5f;
    private float RotationScaleMultiplier = 1.0f;
    private bool ReadyToSnapTurn; // Set to true when a snap turn has occurred, code requires one frame of centered thumbstick to enable another snap turn.
    private bool ReadyToSnapSide;

    // Use this for initialization
    void Strat()
    {
        speed = 50.0f;
        pos = GetComponent<Transform>();
        var p = CameraRig.transform.localPosition;
        p.z = OVRManager.profile.eyeDepth;
        CameraRig.transform.localPosition = p;
    }
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
    //SecondaryThumbstick = RightController Stick
    //PrimaryThumbstick = LeftController Stick
    void MovePlayer()
    {
        Vector3 euler = transform.rotation.eulerAngles;
        float rotateInfluence = SimulationRate * Time.deltaTime * RotationAmount * RotationScaleMultiplier;
        float side = 10.0f * Time.deltaTime;

        if (SnapSide) {
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
            {
                if (ReadyToSnapSide)
                {
                    Debug.Log("Up");
                    speed = 75.0f;
                    ReadyToSnapSide = false;
                }
            }
            else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
            {
                if (ReadyToSnapSide)
                {
                    Debug.Log("Down");
                    speed = 0.0f;
                    ReadyToSnapSide = false;
                }
            }
           else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft)) {
                if (ReadyToSnapSide)
                {
                    Debug.Log("Left");
                    transform.Translate(Vector3.left * side);
                    ReadyToSnapSide = false;
                }
            }
            else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight))
            {
                if (ReadyToSnapSide)
                {
                    Debug.Log("Right");
                    transform.Translate(Vector3.right * side);
                    ReadyToSnapSide = false;
                }
            }
            else
            {
                speed = 20.0f;
                ReadyToSnapSide = true;
            }
        }

        if (SnapRotation)
        {

            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft)) 
            {
                if (ReadyToSnapTurn)
                {
                    euler.y-= RotationRatchet; //y축 -90만큼 회전
                    ReadyToSnapTurn = false;
                }
            }
            else if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight))
            {
                if (ReadyToSnapTurn)
                {
                    euler.y += RotationRatchet;//y축 +90만큼 회전
                    ReadyToSnapTurn = false;
                }
            }
            else
            {
                ReadyToSnapTurn = true;
            }
        }
        else
        {
            Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            euler.y += secondaryAxis.x * rotateInfluence;
        }

        
        float move = speed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(euler);
        transform.Translate(Vector3.forward * move);
    }
}
