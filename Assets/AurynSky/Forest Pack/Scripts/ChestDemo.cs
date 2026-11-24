using System.Collections;
using UnityEngine;

public class ChestDemo : MonoBehaviour
{
    public Animator chestAnim; // Animator for the chest

    void Awake()
    {
        chestAnim = GetComponent<Animator>();
        // Optionally, start demo purposes here, if needed
        // StartCoroutine(OpenCloseChest());
    }

    public void OpenChest()
    {
        // Play open animation
        chestAnim.SetTrigger("open");
    }

    // Optional method for closing the chest
    public void CloseChest()
    {
        chestAnim.SetTrigger("close");
    }
}
