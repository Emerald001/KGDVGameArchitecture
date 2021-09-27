using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public static KeyCode GetKeyForAction(KeyBindingActions _action)
    {
        foreach (var keyBindingsCheck in KeyBindings.KeybindingCheck) 
        {
            if (keyBindingsCheck.keybindingAction == _action)
            {
                return keyBindingsCheck.Value;
            }
        }
        
        return KeyCode.None;
    }

    public static bool GetKeyDown(KeyBindingActions _key)
    {
        foreach (var keyBindingsCheck in KeyBindings.keyBindingChecks) 
        {
            if (keyBindingsCheck.Key == _key)
            {
                return Input.GetKeyDown(keyBindingsCheck.Value);
            }
        }
        
        return false;
    }
    
    public static bool GetKey(KeyBindingActions _key)
    {
        foreach (var keyBindingsCheck in KeyBindings.keyBindingChecks) 
        {
            if (keyBindingsCheck.Key == _key)
            {
                return Input.GetKey(keyBindingsCheck.Value);
            }
        }
        
        return false;
    }
    
    public static bool GetKeyUp(KeyBindingActions _key)
    {
        foreach (var keyBindingsCheck in KeyBindings.KeybindingCheck) 
        {
            if (keyBindingsCheck.Key == _key)
            {
                return Input.GetKeyUp(keyBindingsCheck.Value);
            }
        }
        
        return false;
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
