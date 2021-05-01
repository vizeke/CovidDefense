using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlaceableObject : MonoBehaviour
{
    public State state = State.ValidPlace;

    private BoxCollider colliderCache = null;
    private BoxCollider Collider { get => colliderCache ?? (colliderCache = GetComponent<BoxCollider>()); }

    private List<GameObject> collidingObjects = new List<GameObject>();

    public void SetPosition(Vector3 position)
    {
        transform.SetPositionAndRotation(position, transform.rotation);
    }

    public bool CanBePlaced()
    {
        return state == State.ValidPlace;
    }

    public void PlaceIt()
    {
        state = State.Placed;
        Collider.isTrigger = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Placeable") return;

        if (!collidingObjects.Contains(other.gameObject))
        {
            collidingObjects.Add(other.gameObject);
        }

        state = State.InvalidPlace;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag != "Placeable") return;

        collidingObjects.Remove(other.gameObject);
        if (collidingObjects.Count == 0)
        {
            state = State.ValidPlace;
        }
    }

    

    public enum State
    {
        ValidPlace,
        InvalidPlace,
        Placed,
    }
}
