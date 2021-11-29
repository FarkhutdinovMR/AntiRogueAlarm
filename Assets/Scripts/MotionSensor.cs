using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MotionSensor : MonoBehaviour
{
    public UnityAction Entered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Rogue>(out Rogue rogue))
        {
            Entered?.Invoke();
        }
    }
}