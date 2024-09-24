using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    private GameObject managerObject;
    private GameManager manager;
    private Tile tile;
    private GameObject tilePrefab;
    private GameObject[] tileObjects;

    private TMP_Text dayTxt;
    private GameObject dayDisplay;

    private TMP_Text invTxt;
    private GameObject invDisplay;

    public GameObject storeUI;
    private StoreScript storeScript;

    public GameObject launchUI;



    private void Awake()
    {
        //storeUI.SetActive(false);
    }

    public void StartNextDay()
    {
        managerObject = GameObject.Find("Test Player");
        manager = managerObject.GetComponent<GameManager>();
        manager.curDay++;

        dayDisplay = GameObject.Find("DayTxt");
        dayTxt = dayDisplay.GetComponent<TMP_Text>();
        dayTxt.text = "Day: " + manager.curDay;

        manager.cropInventory = manager.cropInventory + Random.Range(0, 5);

        invDisplay = GameObject.Find("CropInvText");
        invTxt = invDisplay.GetComponent<TMP_Text>();
        invTxt.text = "Seeds: " + manager.cropInventory;



        tileObjects = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tilePrefab in  tileObjects)
        {
            tile = tilePrefab.GetComponent<Tile>();
            tile.OnNewDay();

        }
    }

    public void Store()
    {

        storeUI.SetActive(true);
    }

    public void Exit()
    {
        storeUI.SetActive(false);
    }

    public void LaunchExit()
    {
        if (launchUI != null)
        {
            launchUI.SetActive(false);
        }
    }
}
