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

    public bool GetButtonDown(KeyBindingActions _key)
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
    
    public bool GetButton(KeyBindingActions _key)
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
    
    public bool GetButtonUp(KeyBindingActions _key)
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
