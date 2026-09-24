using UnityEngine;
using UnityEngine.UI;

public class WiltTimer : MonoBehaviour
{
    public float timeLeft = 300f;
    public Text timerText;
    public bool lost = false;
    public AudioClip loseSound;

    void Update()
    {
        if (lost) return;

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0f) timeLeft = 0f;

        if (timerText != null)
        {
            int m = (int)(timeLeft / 60f);
            int s = (int)(timeLeft % 60f);
            timerText.text = m + ":" + s.ToString("00");
        }

        if (timeLeft <= 0f)
        {
            lost = true;
            AudioSource audio = GetComponent<AudioSource>();
            if (audio != null && loseSound != null) audio.PlayOneShot(loseSound);
            Debug.Log("time up - you wilted");
        }
    }
}
