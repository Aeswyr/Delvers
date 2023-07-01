using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public PickupType type;
    public int amt;
    public float lockout;

    private float lockoutUntil = float.MaxValue;

    void Awake() {
        lockoutUntil = lockout + Time.time;
    }

    public bool CanPickup() {
        return Time.time > lockoutUntil;
    }
}

public enum PickupType {
    COIN
}
