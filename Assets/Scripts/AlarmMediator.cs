using UnityEngine;

public class AlarmMediator : MonoBehaviour
{
    [SerializeField] private AlarmTrigger _intruderDetector;
    [SerializeField] private AlarmSystem _alarmSystem;

    private void Start()
    {
        _alarmSystem.SubscribeToDetector(_intruderDetector);
    }
}