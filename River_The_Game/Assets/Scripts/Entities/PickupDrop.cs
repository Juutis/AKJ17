using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupDrop : MonoBehaviour
{
    [SerializeField]
    private Pickup pickupPrefab;
    [SerializeField]
    private Transform dropTransform;
    [SerializeField]
    private DropLocation dropLocation;

    [SerializeField]
    private DropCondition dropCondition;

    private Transform pickupParent;

    private void Start()
    {
        pickupParent = WorldManager.main.PickupContainer;
    }


    public void Drop(Vector3 lastKillPosition)
    {
        Vector3 position = transform.position;
        if (dropLocation == DropLocation.LastKill)
        {
            position = lastKillPosition;
        }
        else if (dropLocation == DropLocation.DropTransform)
        {
            position = dropTransform.position;
        }
        SpawnDrop(position);
    }

    private void SpawnDrop(Vector3 position)
    {
        if (pickupPrefab == null)
        {
            Debug.LogWarning($"No pickup prefab specified for {gameObject.name}!");
            Kill();
        }
        if (dropCondition == DropCondition.Never)
        {
            Kill();
        }
        Pickup newPickup = Instantiate(pickupPrefab, position, Quaternion.identity, pickupParent);
        newPickup.Initialize();
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}

public enum DropCondition
{
    Always,
    Never
}
public enum DropLocation
{
    LastKill,
    DropTransform,
    ThisObjectPosition
}
