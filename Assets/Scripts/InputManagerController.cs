using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class InputManagerController : MonoBehaviour
{
    public Camera player1Camera;
    public Camera player2Camera;
    public CinemachineVirtualCamera player1VCam;
    public CinemachineVirtualCamera player2VCam;
    private int numberOfPlayers = 0;
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        var player = playerInput.GetComponent<PlayerController>();
        switch (numberOfPlayers)
        {
            case 0:
                player1VCam.Follow = player.transform;
                player1VCam.LookAt = player.transform;
                player1VCam.GetComponent<cmPOVextension>().player = player;
                player.cameraTransform = player1Camera.transform;
                break;
            case 1:
                player2VCam.Follow = player.transform;
                player2VCam.LookAt = player.transform;
                player2VCam.GetComponent<cmPOVextension>().player = player;
                player.cameraTransform = player2Camera.transform;
                break;
        }
    }
}
