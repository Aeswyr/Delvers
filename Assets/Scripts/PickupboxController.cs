using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupboxController : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.parent.TryGetComponent<PickupController>(out var pickup) && pickup.CanPickup()) {
            Destroy(other.transform.parent.gameObject);

            switch (pickup.type) {
                case PickupType.COIN:
                    player.AddCoins(pickup.amt);
                    break;

            }

        }
    }
}
