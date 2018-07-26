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
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class Score : IComparable
{
	public int score;
	public int playerNumber;

	public Score(int newScore, int playerNum)
	{
		this.score = newScore;
		this.playerNumber = playerNum;
	}

	public int CompareTo(object other)
	{
		Score otherScore = other as Score;

		if (otherScore != null)
			return this.score.CompareTo (otherScore.score);
		else
			throw new ArgumentException("This is not a score object");
	}

	public override string ToString ()
	{
		return String.Format ("Player {0}: {1}", this.playerNumber, this.score);

	}

}

public class GameOverPopUp : MonoBehaviour {

    public Text gameOverScore;
    public Text [] scoresTextInfo;
	List<Score> scoreboard = new List<Score>();
    
    void OnEnable()
    {

		if (Managers.Input.m_AmountOfPlayers > 1)
		{
			gameOverScore.gameObject.SetActive (false);
			for (int i = 0; i < Managers.Input.m_AmountOfPlayers; i++) 
			{
				scoreboard.Add (new Score (Managers.Input.m_Players [i].Score, i + 1));

			}
				
			scoreboard.Sort ();

			for (int i = 0; i < Managers.Input.m_AmountOfPlayers; i++)
			{
				scoresTextInfo [i].gameObject.SetActive (true);
				scoresTextInfo[i].color = Managers.Input.m_Players [i].Colour;
				scoresTextInfo [i].text = scoreboard [i].ToString ();
			}
		} 
		else
		{
			gameOverScore.gameObject.SetActive (true);

			for (int i = 0; i < scoresTextInfo.Length; ++i)
			{
				scoresTextInfo [i].gameObject.SetActive (false);
			}
			gameOverScore.text = Managers.Score.currentScore.ToString();
		}

        Managers.UI.panel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        Managers.Grid.ClearBoard();
        Managers.Audio.PlayUIClick();
        Managers.UI.panel.SetActive(false);
        Managers.Game.SetState(typeof(MenuState));
        gameObject.SetActive(false);
    }
}
