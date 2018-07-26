using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetupButton : MonoBehaviour {

    public void OnClickSetupButton()
    {
        Managers.Audio.PlayUIClick();
        Managers.UI.popUps.ActivateSetupPopup();
        Managers.UI.panel.SetActive(true);
    }
}
