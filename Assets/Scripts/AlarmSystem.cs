using UnityEngine;
using System.Collections;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField, Range(0.1f, 1f)] private float _maxVolume = 1f;
    [SerializeField, Range(0.1f, 2f)] private float _volumeChangeSpeed = 1f;

    [Header("References")]
    [SerializeField] private AlarmTrigger _alarmTrigger;

    private Coroutine _volumeCoroutine;
    private bool _isAlarmActive;

    private void Start()
    {
        if (_alarmTrigger != null)
        {
            Initialize(_alarmTrigger);
        }
    }

    public void Initialize(AlarmTrigger detector)
    {
        detector.IntruderEntered += () => SetAlarmState(true);
        detector.IntruderExited += () => SetAlarmState(false);
    }

    private void SetAlarmState(bool active)
    {
        _isAlarmActive = active;

        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        _volumeCoroutine = StartCoroutine(AdjustVolumeCoroutine(
            targetVolume: active ? _maxVolume : 0f));
    }

    private IEnumerator AdjustVolumeCoroutine(float targetVolume)
    {
        if (targetVolume > 0 && _alarmSound.isPlaying == false)
        {
            _alarmSound.Play();
        }

        while (Mathf.Approximately(_alarmSound.volume, targetVolume) == false)
        {
            _alarmSound.volume = Mathf.MoveTowards(
                _alarmSound.volume,
                targetVolume,
                _volumeChangeSpeed * Time.deltaTime);

            yield return null;
        }

        if (targetVolume <= 0 && _alarmSound.isPlaying)
        {
            _alarmSound.Stop();
        }
    }
}