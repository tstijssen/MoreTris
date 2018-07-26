//  /*********************************************************************************
//   *********************************************************************************
//   *********************************************************************************
//   * Produced by Skard Games										                 *
//   * Facebook: https://goo.gl/5YSrKw											     *
//   * Contact me: https://goo.gl/y5awt4								             *
//   * Developed by Cavit Baturalp Gürdin: https://tr.linkedin.com/in/baturalpgurdin *
//   *********************************************************************************
//   *********************************************************************************
//   *********************************************************************************/

using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour
{
    public GameObject gameOverPopUp;
    public GameObject settingsPopUp;
    public GameObject playerStatsPopUp;
    public GameObject playerSetupPopup;

    public void ActivateSetupPopup()
    {
        playerSetupPopup.transform.parent.gameObject.SetActive(true);
        playerSetupPopup.SetActive(true);
        Managers.UI.activePopUp = playerSetupPopup;
    }

    public void ActivateGameOverPopUp()
    {
        gameOverPopUp.transform.parent.gameObject.SetActive(true);
        gameOverPopUp.SetActive(true);
        Managers.UI.activePopUp = gameOverPopUp;
    }

    public void ActivateSettingsPopUp()
    {
        settingsPopUp.transform.parent.gameObject.SetActive(true);
        settingsPopUp.SetActive(true);
        Managers.UI.activePopUp = settingsPopUp;
    }

    public void ActivatePlayerStatsPopUp()
    {
        playerStatsPopUp.transform.parent.gameObject.SetActive(true);
        playerStatsPopUp.SetActive(true);
        Managers.UI.activePopUp = playerStatsPopUp;
    }

}
