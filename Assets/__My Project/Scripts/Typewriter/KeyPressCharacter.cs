using UnityEngine;

public class KeyPressCharacter : MonoBehaviour
{
    [Tooltip("Print this character when shift key is unlocked")]
    public string lowerCharacter;
    [Tooltip("Print this character when shift key is locked")]
    public string upperCharacter;
    [Tooltip("To check whether the shift key is locked")]
    public KeyboardState keyboardState;
    public Printer printer;

    public void PressKey()
    {
        string output;

        if (keyboardState.IsUpper())
            output = upperCharacter;
        else
            output = lowerCharacter;

        printer.PrintCharacter(output);

        keyboardState.ConsumeShift();
    }
}
