using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableArea : MonoBehaviour
{
    public bool IsAreaAllowed = true;

    private BoxCollider colliderCache = null;
    public BoxCollider Collider { get => colliderCache ?? (colliderCache = GetComponent<BoxCollider>()); }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Placeable") return;

        other.GetComponent<PlaceableObject>().SetIsInValidPlace(IsAreaAllowed);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag != "Placeable") return;

        other.GetComponent<PlaceableObject>().SetIsInValidPlace(!IsAreaAllowed);
    }
}
