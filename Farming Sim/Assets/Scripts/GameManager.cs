using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int curDay;
    public int money;
    public int cropInventory;


    public CropData selectedCropToPlant;

    public event UnityAction onNewDay;

    public TMP_Text invTxt;
    public GameObject invDisplay;

    private TMP_Text moneyTxt;
    public GameObject moneyDisplay;

    public GameObject storeUI;

    // Singleton
    public static GameManager instance;

    private GameObject launchBtn;
    public int boatProgress;

    void OnEnable()
    {
        Crop.onPlantCrop += OnPlantCrop;
        Crop.onHarvestCrop += OnHarvestCrop;
    }

    void OnDisable()
    {
        Crop.onPlantCrop -= OnPlantCrop;
        Crop.onHarvestCrop -= OnHarvestCrop;
    }

    void Awake()
    {
        storeUI.SetActive(false);

        launchBtn = GameObject.Find("LaunchBtn");
        launchBtn.SetActive(false);

        // Initialize the singleton.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Money()
    {
        moneyTxt = moneyDisplay.GetComponent<TMP_Text>();
        moneyTxt.text = "Money: " + money;
    }

    // Called when a crop has been planted.
    // Listening to the Crop.onPlantCrop event.
    public void OnPlantCrop(CropData cop)
    {
        cropInventory--;

        invDisplay = GameObject.Find("CropInvText");
        invTxt = invDisplay.GetComponent<TMP_Text>();
        invTxt.text = "Seeds: " + cropInventory;
    }

    // Called when a crop has been harvested.
    // Listening to the Crop.onCropHarvest event.
    public void OnHarvestCrop(CropData crop)
    {
        money += crop.sellPrice;
        Money();

    }


    // Do we have enough crops to plant?
    public bool CanPlantCrop()
    {
        return cropInventory > 0;
    }


    public void BoatProgressCheck()
    {
        if (boatProgress == 4)
        {
            Debug.Log("threshhold reached");
            launchBtn.SetActive(true);
        }
    }
}