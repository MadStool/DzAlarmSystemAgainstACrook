using UnityEngine;
using UnityEngine.Events;

public class AlarmTrigger : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float _triggerRadius = 4f;

    public UnityEvent OnIntruderEntered { get; } = new UnityEvent();
    public UnityEvent OnIntruderExited { get; } = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IIntruder>(out IIntruder intruder))
        {
            OnIntruderEntered.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IIntruder>(out IIntruder intruder))
        {
            OnIntruderExited.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawSphere(transform.position, _triggerRadius);
    }
}