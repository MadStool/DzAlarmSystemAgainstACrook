using UnityEngine;
using System.Collections;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource alarmSound;
    [SerializeField, Range(0.1f, 1f)] private float _maxVolume = 1f;
    [SerializeField, Range(0.1f, 2f)] private float _volumeChangeSpeed = 1f;

    private Coroutine _volumeCoroutine;
    private bool _isAlarmActive;

    public void ActivateAlarm()
    {
        if (_isAlarmActive) 
            return;

        _isAlarmActive = true;
        AdjustVolume(_maxVolume);
    }

    public void DeactivateAlarm()
    {
        if (_isAlarmActive == false)
            return;

        _isAlarmActive = false;
        AdjustVolume(0f);
    }

    private void AdjustVolume(float targetVolume)
    {
        if (_volumeCoroutine != null)
            StopCoroutine(_volumeCoroutine);

        _volumeCoroutine = StartCoroutine(VolumeAdjustment(targetVolume));
    }

    private IEnumerator VolumeAdjustment(float targetVolume)
    {
        if (targetVolume > 0 && alarmSound.isPlaying == false)
            alarmSound.Play();

        while (Mathf.Approximately(alarmSound.volume, targetVolume) == false)
        {
            alarmSound.volume = Mathf.MoveTowards(
                alarmSound.volume,
                targetVolume,
                _volumeChangeSpeed * Time.deltaTime
            );
            yield return null;
        }

        if (Mathf.Approximately(targetVolume, 0f))
            alarmSound.Stop();

        _volumeCoroutine = null;
    }
}