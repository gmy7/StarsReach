using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnalyzer : MonoBehaviour
{
    // Get called by Event Manager during the Ship Launch phase
    // Centers the player ship on the ship components
    // Stores component position information
    // Analyzes the ship components and calculates values relating to the sum of components (i.e. ship speed based on number of engines)
    // Turns off "isTrigger" for colliders (isTrigger is intially on because the components are dragged over each other in the ship building portion)

    [SerializeField] TileGridManager tileGridManager;
    public Dictionary<Vector2Int, GameObject> shipMap = new Dictionary<Vector2Int, GameObject>();

    public void RunShipAnalysis()
    {
        shipMap = tileGridManager.shipMap;
        CenterPlayerShipOnScreen();
        TurnOffIsTrigger();
    }
    private void CenterPlayerShipOnScreen()
    {
        Vector2 sumOfCoordinates = new Vector2(0,0);
        Vector2 coordinateAverage;
        Vector2 centerOfPlayerShip;
        foreach(Vector2Int coordinate in shipMap.Keys)
        {
            sumOfCoordinates = new Vector2(sumOfCoordinates.x + coordinate.x, sumOfCoordinates.y + coordinate.y);
        }
        coordinateAverage = new Vector2(sumOfCoordinates.x / shipMap.Count, sumOfCoordinates.y / shipMap.Count);
        centerOfPlayerShip = tileGridManager.ConvertGridCoordinateToWorldSpace(coordinateAverage);

        foreach (GameObject component in shipMap.Values)
        {
            component.transform.position = new Vector2(component.transform.position.x - centerOfPlayerShip.x, component.transform.position.y - centerOfPlayerShip.y);
        }
    }
    private void TurnOffIsTrigger()
    {
        foreach (GameObject component in shipMap.Values)
        {
            component.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
