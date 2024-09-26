using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreScript : MonoBehaviour
{


    private GameManager manager;
    private GameObject managerObject;
    public int piecePrice;
    private GameObject launchBtn;

    private void Awake()
    {
        //find gamemanager script
        managerObject = GameObject.Find("Test Player");
        manager = managerObject.GetComponent<GameManager>();
        manager.boatProgress = 0;
    }
    public void BuyPiece()
    {
        //buy boat piece if player has enough money 
        if (manager.money >= piecePrice)
        {
            manager.money = manager.money - piecePrice;

            manager.Money();
            gameObject.SetActive(false);
            manager.boatProgress++;
            manager.BoatProgressCheck();
            //check if player has all boat pieces
        }
    }

}
