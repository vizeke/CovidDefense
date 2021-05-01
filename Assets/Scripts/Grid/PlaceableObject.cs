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

    public event OnChangeState OnChangeStateEvent;
    public delegate void OnChangeState(PlaceableObject target, State state);


    public void SetPosition(Vector3 position)
    {
        transform.SetPositionAndRotation(position, transform.rotation);
    }

    public bool CanBePlaced()
    {
        return state == State.ValidPlace;
    }

    public bool IsPlaced()
    {
        return state == State.Placed;
    }

    public void PlaceIt()
    {
        if (state == State.Placed) return;

        state = State.Placed;
        Collider.isTrigger = false;
        if (OnChangeStateEvent != null) OnChangeStateEvent(this, state);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Placeable") return;

        if (!collidingObjects.Contains(other.gameObject))
        {
            collidingObjects.Add(other.gameObject);
        }

        if (state != State.InvalidPlace)
        {
            state = State.InvalidPlace;
            if (OnChangeStateEvent != null) OnChangeStateEvent(this, state);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag != "Placeable") return;

        collidingObjects.Remove(other.gameObject);
        if (collidingObjects.Count == 0)
        {
            state = State.ValidPlace;
            if (OnChangeStateEvent != null) OnChangeStateEvent(this, state);
        }
    }

    

    public enum State
    {
        ValidPlace,
        InvalidPlace,
        Placed,
    }
}
