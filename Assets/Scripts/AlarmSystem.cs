using UnityEngine;
using System.Collections;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField, Range(0.1f, 1f)] private float _maxVolume = 1f;
    [SerializeField, Range(0.1f, 2f)] private float _volumeChangeSpeed = 1f;

    private Coroutine _volumeAdjustmentCoroutine;
    private bool _isAlarmActive;

    public void SubscribeToDetector(AlarmTrigger detector)
    {
        detector.OnIntruderEntered.AddListener(Activate);
        detector.OnIntruderExited.AddListener(Deactivate);
    }

    private void Activate()
    {
        if (_isAlarmActive) 
            return;

        _isAlarmActive = true;
        SetTargetVolume(_maxVolume);
    }

    private void Deactivate()
    {
        if (_isAlarmActive == false) 
            return;

        _isAlarmActive = false;
        SetTargetVolume(0f);
    }

    private void SetTargetVolume(float targetVolume)
    {
        if (_volumeAdjustmentCoroutine != null)
            StopCoroutine(_volumeAdjustmentCoroutine);

        _volumeAdjustmentCoroutine = StartCoroutine(AdjustVolumeToTarget(targetVolume));
    }

    private IEnumerator AdjustVolumeToTarget(float targetVolume)
    {
        if (ShouldStartPlaying(targetVolume))
            _alarmSound.Play();

        while (ReachedTargetVolume(targetVolume) == false)
        {
            UpdateVolumeTowards(targetVolume);
            yield return null;
        }

        if (ShouldStopPlaying(targetVolume))
            _alarmSound.Stop();

        _volumeAdjustmentCoroutine = null;
    }

    private bool ShouldStartPlaying(float targetVolume)
    {
        return targetVolume > 0 && _alarmSound.isPlaying == false;
    }

    private bool ReachedTargetVolume(float targetVolume)
    {
        return Mathf.Approximately(_alarmSound.volume, targetVolume);
    }

    private bool ShouldStopPlaying(float targetVolume)
    {
        return Mathf.Approximately(targetVolume, 0f);
    }

    private void UpdateVolumeTowards(float targetVolume)
    {
        _alarmSound.volume = Mathf.MoveTowards(
            _alarmSound.volume,
            targetVolume,
            _volumeChangeSpeed * Time.deltaTime
        );
    }
}