using UnityEngine;

public class Validator : MonoBehaviour
{
    [Header("Highlight")]
    [SerializeField] private SpriteRenderer _cropSR; // assign the green highlight sprite

    void Start()
    {
        if (_cropSR != null)
            _cropSR.enabled = false; // start invisible
    }

    public void TurnValidatorOn()
    {
        if (_cropSR != null)
            _cropSR.enabled = true;
    }

    public void TurnValidatorOff()
    {
        if (_cropSR != null)
            _cropSR.enabled = false;
    }
}
