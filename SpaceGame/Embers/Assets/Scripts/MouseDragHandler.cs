using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragHandler : MonoBehaviour
{
    // Handles dragging componets onto the empty ship tilemap

    [SerializeField] TileGridManager tileGridManager;
    [SerializeField] GameObject playerShipComponents;

    GameObject selectedObject;
    GameObject grabbedObject;
    GameObject tileUnderneathReleasePoint; //tile which is supplanted by ship component

    Vector2 mousePosition;
    Vector2Int gridPosition;

    private void Update()
    {
        mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        gridPosition = tileGridManager.ConvertWorldSpaceToGridCoordinate(mousePosition);

        SelectDraggableObject();
        DragObject();
        ReleaseObject();
    }
    private void SelectDraggableObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.transform.gameObject.TryGetComponent(out ComponentTile componentTile) && hit.transform.gameObject.GetComponent<ComponentTile>().isDraggable)
                {
                    selectedObject = hit.transform.gameObject;
                }
            }
        }
    }
    private void DragObject()
    {
        if (selectedObject != null)
        {
            grabbedObject = Instantiate(selectedObject, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            selectedObject = null;
        }
    }
    private void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.transform.position = mousePosition;

            if (Input.GetMouseButtonUp(0))
            {
                if (tileGridManager.tileMap.TryGetValue(gridPosition, out tileUnderneathReleasePoint))
                {
                    grabbedObject.transform.position = tileUnderneathReleasePoint.transform.position;
                    grabbedObject.transform.parent = playerShipComponents.transform;
                    Vector2Int componentGridCoordinates = tileGridManager.ConvertWorldSpaceToGridCoordinate(grabbedObject.transform.position);
                    Destroy(tileUnderneathReleasePoint);
                    tileGridManager.AddTileToShipMap(componentGridCoordinates.x, componentGridCoordinates.y, grabbedObject);
                }
                else
                {
                    Destroy(grabbedObject);
                }
                grabbedObject.GetComponent<ComponentTile>().isDraggable = false;
                grabbedObject = null;
            }
        }
    }
}
