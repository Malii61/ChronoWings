using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
   public static BackgroundSound Instance;
   private AudioSource audioSource;
   private void Awake()
   {
      Instance = this;
      audioSource = GetComponent<AudioSource>();
   }

   public void OnClockCollected()
   {
      audioSource.pitch += 0.1f;
   }
}
