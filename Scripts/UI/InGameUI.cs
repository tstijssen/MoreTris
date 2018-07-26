//  /*********************************************************************************
//   *********************************************************************************
//   *********************************************************************************
//   * Produced by Skard Games										                  *
//   * Facebook: https://goo.gl/5YSrKw											      *
//   * Contact me: https://goo.gl/y5awt4								              *											
//   * Developed by Cavit Baturalp Gürdin: https://tr.linkedin.com/in/baturalpgurdin *
//   *********************************************************************************
//   *********************************************************************************
//   *********************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public struct PlayerScore
{
    public Text score;
    public Text scoreLabel;
}


public class InGameUI : MonoBehaviour {

	public Text score;
    public Text highScore;
    public Text scoreLabel;
    public Text highScoreLabel;

    public PlayerScore[] scores;

    public GameObject gameOverPopUp;

    public int activePlayers;

    void OnEnable()
    {
//        activePlayers = 0;
//        for (int i = 0; i < Managers.Input.m_Players.Length; ++i)
//        {
//            if (Managers.Input.m_Players[i].Active)
//            {
//                activePlayers++;
//                scores[i].score.gameObject.SetActive(true);
//                scores[i].scoreLabel.gameObject.SetActive(true);
//
//            }
//        }
//
//        if (activePlayers == 1)
//        {
//            highScore.gameObject.SetActive(true);
//            highScoreLabel.gameObject.SetActive(true);
//        }
//
//        for (int i = 0; i < activePlayers; ++i)
//        {
//            scores[i].scoreLabel.color = Managers.Input.m_Players[i].Colour;
//        }
    }

    void Update()
    {
        activePlayers = 0;
        for (int i = 0; i < Managers.Input.m_Players.Length; ++i)
        {
			scores[i].score.gameObject.SetActive(false);
			scores[i].scoreLabel.gameObject.SetActive(false);
            if (Managers.Input.m_Players[i].Active)
            {
                activePlayers++;
                scores[i].score.gameObject.SetActive(true);
                scores[i].scoreLabel.gameObject.SetActive(true);

            }
        }

		if (activePlayers == 1) 
		{
			highScore.gameObject.SetActive (true);
			highScoreLabel.gameObject.SetActive (true);
		} 
		else 
		{
			highScore.gameObject.SetActive (false);
			highScoreLabel.gameObject.SetActive (false);
		}

        for (int i = 0; i < activePlayers; ++i)
        {
            scores[i].scoreLabel.color = Managers.Palette.GetColourFromTheme(i);
        }
    }

    public void UpdateScoreUI()
	{
        if (activePlayers == 1)
            highScore.text = Managers.Score.highScore.ToString();
		Debug.Log (activePlayers);
        for (int i = 0; i < activePlayers; ++i)
        {
            scores[i].score.text = Managers.Input.m_Players[i].Score.ToString();
        }
    }

    public void InGameUIStartAnimation()
    {
        if (activePlayers == 1)
        {
            highScore.rectTransform.DOAnchorPosY(-375, 1, true);
            highScoreLabel.rectTransform.DOAnchorPosY(-334, 1, true);
        }
        for (int i = 0; i < activePlayers; ++i)
        {
            scores[i].score.rectTransform.DOAnchorPosY(-375, 1, true);
            scores[i].scoreLabel.rectTransform.DOAnchorPosY(-334, 1, true);
        }
    }

    public void InGameUIEndAnimation()
    {

        if (activePlayers == 1)
        {
            highScore.rectTransform.DOAnchorPosY(-375 + 650, 0.3f, true);
            highScoreLabel.rectTransform.DOAnchorPosY(-334 + 650, 0.3f, true);
        }

        for (int i = 0; i < activePlayers; ++i)
        {
            scores[i].score.rectTransform.DOAnchorPosY(-375 + 650, 0.3f, true);
            scores[i].scoreLabel.rectTransform.DOAnchorPosY(-334 + 650, 0.3f, true);
        }
    }


}
