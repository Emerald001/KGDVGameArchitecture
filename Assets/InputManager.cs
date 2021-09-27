using UnityEngine;

public class InputManager
{
    private KeyBindings keyBindings;
    
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

    public bool GetKeyDown(KeyBindingActions _key)
    {
        foreach (var keyBindingsCheck in keyBindings.keybindingChecks) 
        {
            if (keyBindingsCheck.keyBindingAction == _key)
            {
                return Input.GetKeyDown(keyBindingsCheck.keyCode);
            }
        }
        
        return false;
    }
    
    public bool GetKey(KeyBindingActions _key)
    {
        foreach (var keyBindingsCheck in keyBindings.keybindingChecks) 
        {
            if (keyBindingsCheck.keyBindingAction == _key)
            {
                return Input.GetKey(keyBindingsCheck.keyCode);
            }
        }
        
        return false;
    }
    
    public bool GetKeyUp(KeyBindingActions _key)
    {
        foreach (var keyBindingsCheck in keyBindings.keybindingChecks) 
        {
            if (keyBindingsCheck.keyBindingAction == _key)
            {
                return Input.GetKeyUp(keyBindingsCheck.keyCode);
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
