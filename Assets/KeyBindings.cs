using UnityEngine;

[CreateAssetMenu(fileName = "Keybindings", menuName = "Keybindings")]
public class KeyBindings : ScriptableObject
{
    [System.Serializable]
    public class KeybindingCheck
    {
        public KeyBindingActions keyBindingAction;
        public KeyCode keyCode;
    }

    public KeybindingCheck[] keybindingChecks;
}