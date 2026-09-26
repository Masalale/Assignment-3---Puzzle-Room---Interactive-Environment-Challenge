using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject winPanel;
    public GameObject losePanel;

    public PuzzleManager puzzleManager;
    public WiltTimer timer;
    public PlayerMove player;
    public Interactor interactor;
    public Transform exitPoint;

    bool started = false;
    bool ended = false;

    void Start()
    {
        if (startPanel != null) startPanel.SetActive(true);
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        SetPlaying(false);
    }

    public void StartGame()
    {
        if (started) return;
        started = true;

        if (startPanel != null) startPanel.SetActive(false);
        SetPlaying(true);
    }

    void SetPlaying(bool on)
    {
        if (player != null) player.enabled = on;
        if (interactor != null) interactor.enabled = on;
        // switching the timer off is what freezes the countdown, it reads Time.deltaTime in Update
        if (timer != null) timer.enabled = on;
        Cursor.lockState = on ? CursorLockMode.Locked : CursorLockMode.None;
    }

    void Update()
    {
        if (!started || ended) return;

        if (timer != null && timer.lost)
        {
            ended = true;
            if (losePanel != null) losePanel.SetActive(true);
            SetPlaying(false);
            return;
        }

        // the door opening isn't the win, the player still has to walk out through it
        if (puzzleManager != null && puzzleManager.IsAllDone()
            && exitPoint != null && player != null
            && Vector3.Distance(player.transform.position, exitPoint.position) < 1.5f)
        {
            ended = true;
            if (winPanel != null) winPanel.SetActive(true);
            SetPlaying(false);
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
