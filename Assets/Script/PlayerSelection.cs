using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelection : MonoBehaviour
{

    void Start()
    {

    }

    public void CharacterSelection(int characterIndex)
    {
        PlayerPrefs.SetInt("characterno", characterIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level 1");
    }
}