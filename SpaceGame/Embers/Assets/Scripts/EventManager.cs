using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject tileGrid;
    [SerializeField] GameObject playerShip;
    [SerializeField] GameObject spaceSceneBackground;

    public bool componentsActive = false;
    public void RunShipBuilder()
    {
        tileGrid.GetComponent<TileGridManager>().GenerateGrid();
    }
    public void LaunchShip()
    {
        ActivateComponentScripts();

        playerShip.GetComponent<ShipAnalyzer>().RunShipAnalysis();
        SceneManager.LoadScene("SpaceSandbox");
        var spaceBackground = Instantiate(spaceSceneBackground);
        spaceBackground.transform.parent = Camera.main.transform;

        playerShip.GetComponent<ShipController>().enabled = true;
    }
    public void ActivateComponentScripts()
    {
        componentsActive = true;
    }
}
