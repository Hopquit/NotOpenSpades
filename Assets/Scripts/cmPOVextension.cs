using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class cmPOVextension : CinemachineExtension
{
    private Vector3 startingRotation;
    public PlayerController player;
    public float horizontalSpeed = 10f;
    public float clampAngle = 80f;
    public float verticalSpeed = 10f;
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null)
                {
                    startingRotation = transform.localRotation.eulerAngles;
                }
                    startingRotation.x += player.deltaInput.x * verticalSpeed * Time.deltaTime;
                    startingRotation.y += player.deltaInput.y * horizontalSpeed * Time.deltaTime;
                    startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                    state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
                
            }
        }
    }

}
