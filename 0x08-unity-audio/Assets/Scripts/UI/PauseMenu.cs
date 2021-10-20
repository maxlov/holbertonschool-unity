using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public InputActionAsset InputMap;
    private InputAction PauseAction;

    private GameObject GameManager;
    private GameManager gameManager;

    public GameObject PauseCanvas;

    public AudioMixerSnapshot SnapshotPaused;
    public AudioMixerSnapshot SnapshotUnpaused;

    private void Awake()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = GameManager.GetComponent<GameManager>();

        PauseAction = InputMap.FindAction("Pause");
        PauseAction.performed += togglePause;
    }

    private void togglePause(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        if (PauseCanvas.activeSelf)
            Resume();
        else
            Pause();
    }

    private void Pause()
    {
        PauseCanvas.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SnapshotPaused.TransitionTo(.01f);
    }

    private void Resume()
    {
        PauseCanvas.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SnapshotUnpaused.TransitionTo(.01f);
    }

    public void Restart()
    {
        gameManager.Reload();
    }

    public void MenuButton()
    {
        gameManager.Load("MainMenu");
    }

    public void OptionsButton()
    {
        gameManager.Load("Options");
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        PauseAction.Enable();
    }

    private void OnDisable()
    {
        PauseAction.Disable();
        PauseAction.actionMap.Disable();
    }
}
