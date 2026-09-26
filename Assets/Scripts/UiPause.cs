using UnityEngine;

// Escape menu. Pauses time and hides the crosshair while open.
public class UiPause : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject crosshairCanvas;
    public PlayerMove player;
    public Interactor interactor;

    bool paused = false;

    void Start()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused) ResumeGame();
            else OpenPause();
        }
    }

    public void OpenPause()
    {
        if (player != null && !player.enabled) return;

        paused = true;
        Time.timeScale = 0f;
        if (pausePanel != null) pausePanel.SetActive(true);
        if (crosshairCanvas != null) crosshairCanvas.SetActive(false);
        if (player != null) player.enabled = false;
        if (interactor != null) interactor.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1f;
        if (pausePanel != null) pausePanel.SetActive(false);
        if (crosshairCanvas != null) crosshairCanvas.SetActive(true);
        if (player != null) player.enabled = true;
        if (interactor != null) interactor.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
