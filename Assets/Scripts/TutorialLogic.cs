using UnityEngine;

using System;
using UnityEngine.Events;

public class TutorialLogic : MonoBehaviour
{
    private UnityAction _onButtonPressed;

    public UnityAction OnButtonPressed { get => _onButtonPressed; set => _onButtonPressed = value; }

    public void ButtonPressed()
    {
        _onButtonPressed?.Invoke();
    }
}
