using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{
    public MainMenu UIManager;

    public void CreditsHidden()
    {
        UIManager.CreditsFadedAway();
    }
}
