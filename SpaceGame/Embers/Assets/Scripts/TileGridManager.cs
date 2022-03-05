using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileGridManager : MonoBehaviour
{
    // Generates and stores empty tilemap for ship building
    // Stores ship map which is handled by Ship Analyzer

    [Header("Tile Grid Attributes")]
    [SerializeField] int gridWidth = 10;
    [SerializeField] int gridHeight = 10;
    [SerializeField] GameObject emptyTile;
    [SerializeField] GameObject tileGrid;
    [SerializeField] float spacingModifier = .54f; //modifies spacing between tiles. Equal to pixel length/width of square tile

    [Header("Ship Components")]
    public GameObject engine;
    public GameObject cabin;
    public GameObject weapons;
    public GameObject bridge;

    public Dictionary<Vector2Int, GameObject> tileMap = new Dictionary<Vector2Int, GameObject>();
    public Dictionary<Vector2Int, GameObject> shipMap = new Dictionary<Vector2Int, GameObject>();

    #region Generate Tiles and Tile Map
    public void GenerateGrid()
    {
        int widthBottomCorner = Mathf.RoundToInt(-gridWidth / 2);
        int heightBottomCorner = Mathf.RoundToInt(-gridHeight / 2);
        for (int y = widthBottomCorner; y < -widthBottomCorner; y++)
        {
            for (int x = heightBottomCorner; x < -heightBottomCorner; x++)
            {
                GameObject newTile = Instantiate(emptyTile, new Vector2(x * spacingModifier, y * spacingModifier), Quaternion.identity);
                newTile.transform.parent = tileGrid.transform;
                AddTileToTileMap(x,y, newTile);
            }
        }
    }
    public void AddTileToTileMap(int _xCoord, int _yCoord, GameObject tile)
    {
        tileMap.Add(new Vector2Int(_xCoord, _yCoord), tile);
        tile.name = $"({_xCoord},{_yCoord})";
    }
    public void AddTileToShipMap(int _xCoord, int _yCoord, GameObject tile)
    {
        shipMap.Add(new Vector2Int(_xCoord, _yCoord), tile);
    }

    public Vector2Int ConvertWorldSpaceToGridCoordinate(Vector2 worldSpace)
    {
        Vector2Int gridCoordinates;
        return gridCoordinates = new Vector2Int(Mathf.RoundToInt(worldSpace.x / spacingModifier), Mathf.RoundToInt(worldSpace.y / spacingModifier));
    }

    public Vector2 ConvertGridCoordinateToWorldSpace(Vector2 gridCoordinate)
    {
        Vector2 worldSpace;
        return worldSpace = new Vector2(gridCoordinate.x * spacingModifier, gridCoordinate.y * spacingModifier);
    }

    #endregion
}