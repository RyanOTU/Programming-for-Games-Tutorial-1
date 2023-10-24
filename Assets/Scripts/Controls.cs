using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public static class Controls
{
    private static PlayerAction controls;
    private static InputAction inputAction;
    public static void Init(Player player)
    {
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
        PlayMode();
    }

    private static void Shoot_performed(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    public static void PlayMode() {
        controls.Enable();
    }
}
