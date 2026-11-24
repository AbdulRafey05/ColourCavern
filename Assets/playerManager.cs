using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public GameObject[] Players;

    void Start()
    {
        Players[PlayerPrefs.GetInt("characterno")].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
