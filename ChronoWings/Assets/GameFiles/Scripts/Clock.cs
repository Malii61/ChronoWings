using UnityEngine;

public class Clock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Bird bird))
        {
            if (!bird.IsAI())
            {
                Level.GetInstance().PIPE_MOVE_SPEED += 5;
                BackgroundSound.Instance.OnClockCollected();
                SoundManager.PlaySound(SoundManager.Sound.ClockCollected);
                gameObject.SetActive(false);
            }
        }
    }
}