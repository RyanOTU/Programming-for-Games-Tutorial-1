using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public static class Controls
{
    private static PlayerAction controls;
    private static Player _owner;
    private static InputAction inputAction;
    private static Camera _camera;
    public static void Init(Player player, Camera cam = null)
    {
        ///This code absolutely breaks everything.
        //BindNewPlayer(player);
        //SetControllerCamera(cam ? cam : Camera.main);
        controls = new PlayerAction();

        controls.Game.Move.performed += ctx =>
        {
            player.SetMovementDirection(ctx.ReadValue<Vector2>());
        };
        controls.Game.Jump.performed += ctx =>
        {
            player.Jump();
        };
        controls.Game.Look.performed += ctx =>
        {
            player.SetLook(ctx.ReadValue<Vector2>());
        };
        controls.Game.Shoot.performed += ctx =>
        {
            player.Shoot();
        };
        controls.Game.MoveTo.performed += ctx =>
        {
            player.MoveTo(CamToWorldRay());
        };
        PlayMode();
    }
    public static void SetControllerCamera(Camera cam)
    {
        _camera = cam;
        _camera.transform.SetParent(_camera.transform, true);
    }
    public static void BindNewPlayer(Player newPlayer)
    {
        _owner = newPlayer;
        _camera.transform.SetParent(_owner.transform, true);
    }
    public static void SetCommander(Player newTarget)
    {
        _owner = newTarget;
        _camera.transform.SetParent(_owner.transform, true);
    }

    private static Ray CamToWorldRay()
    {
        return _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        _camera.transform.SetParent(_owner.transform, true);
    }

    public static void PlayMode() {
        controls.Enable();
    }
}
