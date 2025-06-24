using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float _triggerRadius = 4f;
    [SerializeField] private AlarmSystem _alarmSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Intruder>(out Intruder intruder))
        {
            _alarmSystem?.ActivateAlarm();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Intruder>(out Intruder intruder))
        {
            _alarmSystem?.DeactivateAlarm();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawSphere(transform.position, _triggerRadius);
    }
}