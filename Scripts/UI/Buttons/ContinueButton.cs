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

public class ContinueButton : MonoBehaviour {

    public void OnClickContinueButton(int numOfPlayers)
    {
        Managers.Audio.PlayUIClick();
        if (Managers.Game.isGameActive)
        {
            Managers.Grid.ClearBoard();
            Managers.Game.isGameActive = false;
        }
        Managers.Game.SetState(typeof(GamePlayState));
        Managers.Input.m_CurrentPlayer = 0;
        Managers.UI.HideCurrentPanel();
		Managers.Input.SetupControlScheme ();
        Managers.Input.SetupPlayers(numOfPlayers);

		for (int i = 0; i < Managers.Input.m_Players.Length; ++i)
		{
			Managers.Input.m_Players [i].Score = 0;
		}
		Managers.UI.inGameUI.UpdateScoreUI();

    }

    public void OnClickResumeButton()
    {
        Managers.Audio.PlayUIClick();
        Managers.Game.SetState(typeof(GamePlayState));
		Managers.Input.SetupControlScheme ();
        Managers.UI.HideCurrentPanel();
    }
}
