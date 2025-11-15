using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInputManager : MonoBehaviour
{
    public static ControllerInputManager instance;
    public bool MenuOpenCloseInput { get; private set; }
    private PlayerInput _playerInput;

    private InputAction _menuOpenClose;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        _playerInput = GetComponent<PlayerInput>();
        //_menuOpenClose = _playerInput.actions["MenuOpenClose"];
    }
    private void Update()
    {
        //MenuOpenCloseInput = _menuOpenClose.WasPressedThisFrame();
    }

}
