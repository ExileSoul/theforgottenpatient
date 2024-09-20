using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public PlayerInputActions actions { get; private set; }

    private void Awake()
    {
        actions = new PlayerInputActions();
    }

    private void Start()
    {
        SwitchControlsToGameplay();
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    private void SwitchControlsToGameplay()
    {
        actions.ui.Disable();
        actions.gameplay.Enable();
    }

    private void SwitchControlsToUI()
    {
        actions.gameplay.Disable();
        actions.ui.Enable();
    }
}
