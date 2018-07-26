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

public class RestartButton : MonoBehaviour {

    public void OnClickRestartButton()
    {
		Debug.Log("Restarting");
        Managers.Audio.PlayUIClick();
        Managers.Grid.ClearBoard();
        Managers.Game.isGameActive = false;
        Managers.Game.SetState(typeof(GamePlayState));
        Managers.UI.inGameUI.gameOverPopUp.SetActive(false);
        Managers.Input.m_CurrentPlayer = 0;
		Managers.Input.SetupControlScheme ();
        Managers.Input.SetupPlayers(Managers.Input.m_AmountOfPlayers);
		for (int i = 0; i < Managers.Input.m_Players.Length; ++i)
		{
			Managers.Input.m_Players [i].Score = 0;
		}
		Managers.UI.inGameUI.UpdateScoreUI();

    }
}
