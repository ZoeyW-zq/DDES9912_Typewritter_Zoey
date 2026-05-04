using UnityEngine;

public class KeyboardState : MonoBehaviour
{
    bool shiftNext = false;
    bool shiftLock = false;

    public bool IsUpper()
    {
        return shiftNext || shiftLock;
    }

    public void PressShift()
    {
        shiftNext = true;
        Debug.Log(shiftNext);
    }

    public void ToggleShiftLock()
    {
        shiftLock = !shiftLock;
    }

    public void ConsumeShift()
    {
        shiftNext = false;
    }
}