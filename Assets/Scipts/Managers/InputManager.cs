using System;
using UnityEngine;

public class InputManager
{
    //Singleton without MonoBehaviour
    public static InputManager instance { get; } = new InputManager();
    private InputManager() { }
    
    private readonly KeyBindings keyBindings = Resources.Load("Keybindings/PlayerKeybindings") as KeyBindings;
    
    public KeyCode GetKeyForAction(KeyBindingActions _action)
    {
        foreach (var keyBindingsCheck in keyBindings.keybindingChecks) 
        {
            if (keyBindingsCheck.keyBindingAction == _action)
            {
                return keyBindingsCheck.keyCode;
            }
        }
        
        return KeyCode.None;
    }

    public int GetButtonDown(KeyBindingActions _key)
    {
        foreach (var keyBindingsCheck in keyBindings.keybindingChecks) 
        {
            if (keyBindingsCheck.keyBindingAction == _key)
            {
                return Convert.ToInt32(Input.GetKeyDown(keyBindingsCheck.keyCode));
            }
        }
        
        return 0;
    }
    
    public int GetButton(KeyBindingActions _key)
    {
        foreach (var keyBindingsCheck in keyBindings.keybindingChecks) 
        {
            if (keyBindingsCheck.keyBindingAction == _key)
            {
                return Convert.ToInt32(Input.GetKey(keyBindingsCheck.keyCode));
            }
        }
        
        return 0;
    }
    
    public int GetButtonUp(KeyBindingActions _key)
    {
        foreach (var keyBindingsCheck in keyBindings.keybindingChecks) 
        {
            if (keyBindingsCheck.keyBindingAction == _key)
            {
                return Convert.ToInt32(Input.GetKeyUp(keyBindingsCheck.keyCode));
            }
        }
        
        return 0;
    }

    public Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }
}

public enum KeyBindingActions
{
    Left,
    Right,
    Up,
    Down,
    Shoot,
    Pause,
}
