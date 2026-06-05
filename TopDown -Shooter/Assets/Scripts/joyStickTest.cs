using UnityEngine;
using PinePie.SimpleJoystick;

public class JoystickTest : MonoBehaviour
{
    public JoystickController joystick;

    void Update()
    {
        Debug.Log(joystick.InputDirection);
    }
}