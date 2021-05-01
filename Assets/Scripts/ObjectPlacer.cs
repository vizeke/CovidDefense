using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    const int TERRAIN_LAYER_MASK = 1 << 8;
    const float RAY_MAX_LENGTH = 1000f;

    public PlaceableObject currentPlaceableObject = null;
    public SnappingGrid grid = null;

    private Ray ray;
    private RaycastHit raycastHit;
    private Vector3 lastPlacementPosition;

    // Update is called once per frame
    void Update()
    {
        if (currentPlaceableObject == null) return;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hasCollision = Physics.Raycast(ray, out raycastHit, RAY_MAX_LENGTH, TERRAIN_LAYER_MASK);
        if (hasCollision)
        {
            var gridPosition = grid.SnapPositionToGrid(raycastHit.point);
            currentPlaceableObject.SetPosition(gridPosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            PlaceCurrentObject();
        }
    }

    public void PlaceCurrentObject()
    {
        if (currentPlaceableObject == null) return;
        if (!currentPlaceableObject.CanBePlaced()) return;

        currentPlaceableObject.PlaceIt();
        currentPlaceableObject = null;
    }
}
