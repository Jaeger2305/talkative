using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject AudioCrystal;
    public GameObject AudioShipRepair;

    public void playCrystal()
    {
        AudioCrystal.gameObject.GetComponent<AudioSource>().Play();
    }

    public void playShipRepair()
    {
        AudioShipRepair.gameObject.GetComponent<AudioSource>().Play();
    }
}
