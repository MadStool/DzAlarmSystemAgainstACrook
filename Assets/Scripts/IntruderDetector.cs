using UnityEngine;

public class IntruderDetector : MonoBehaviour
{
    [Header("Settings Alarm")]
    public float alarmRadius = 5f;
    public AudioSource alarmSound;
    public float maxVolume = 1f;
    public float volumeChangeSpeed = 0.5f;

    private float targetVolume = 0f;

    void Update()
    {
        bool intruderDetected = CheckForIntruders();

        targetVolume = intruderDetected ? maxVolume : 0f;
        alarmSound.volume = Mathf.MoveTowards(alarmSound.volume, targetVolume, volumeChangeSpeed * Time.deltaTime);

        if (alarmSound.volume > 0 && alarmSound.isPlaying == false)
        {
            alarmSound.Play();
        }
        else if (alarmSound.volume == 0 && alarmSound.isPlaying)
        {
            alarmSound.Stop();
        }
    }

    private bool CheckForIntruders()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, alarmRadius);

        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Intruder>() != null)
            {
                return true;
            }
        }

        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alarmRadius);
    }
}