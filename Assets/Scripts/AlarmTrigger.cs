using UnityEngine;
using System;

public interface IIntruder{ }

[RequireComponent(typeof(SphereCollider))]
public class AlarmTrigger : MonoBehaviour
{
    public event Action IntruderEntered;
    public event Action IntruderExited;

    private void Awake() => GetComponent<SphereCollider>().isTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IIntruder>(out IIntruder intruder))
            IntruderEntered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IIntruder>(out IIntruder intruder))
            IntruderExited?.Invoke();
    }
}