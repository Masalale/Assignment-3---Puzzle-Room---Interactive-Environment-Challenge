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

        if (player != null) player.enabled = false;
        if (interactor != null) interactor.enabled = false;
        if (timer != null) timer.enabled = false;

        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame()
    {
        if (started) return;
        started = true;

        if (startPanel != null) startPanel.SetActive(false);
        if (player != null) player.enabled = true;
        if (interactor != null) interactor.enabled = true;
        if (timer != null) timer.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!started || ended) return;

        if (puzzleManager != null && puzzleManager.IsAllDone())
        {
            if (exitPoint != null && player != null && Vector3.Distance(player.transform.position, exitPoint.position) < 1.5f)
            {
                ended = true;
                if (winPanel != null) winPanel.SetActive(true);
                if (player != null) player.enabled = false;
                if (interactor != null) interactor.enabled = false;
                if (timer != null) timer.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                return;
            }
        }

        if (timer != null && timer.lost)
        {
            ended = true;
            if (losePanel != null) losePanel.SetActive(true);
            if (player != null) player.enabled = false;
            if (interactor != null) interactor.enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
