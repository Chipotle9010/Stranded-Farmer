using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchScript : MonoBehaviour
{
    public GameObject launchUI;

        public void LaunchBoat()
    {
        launchUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
