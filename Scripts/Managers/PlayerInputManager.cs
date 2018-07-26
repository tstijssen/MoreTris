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

[System.Serializable]
public struct Player
{
    public Color Colour;
    public string HorizontalInput;
    public string UpButton;
    public string DownButton;
    public bool Active;
    public int Score;
}


public enum InputMethod
{
    KeyboardInput,
    MouseInput,
    TouchInput,
    GamepadInput
}

public class PlayerInputManager : MonoBehaviour
{
    public bool isActive;
    public InputMethod inputType;
    bool movingDown = false;
    bool canInteract = false;

    public Player[] m_Players;
    public int m_CurrentPlayer;
    public int m_AmountOfPlayers;
    public int m_PreviousPlayer;

	public GameObject m_ButtonControlScheme; 
	bool m_Freefall = false;
	string m_ControlScheme;
	bool movingRight;
	bool movingLeft;

    void Awake()
    {
        canInteract = false;
        StartCoroutine(MenuDelay());
    }

	public void SetupControlScheme()
	{
		m_ControlScheme = PlayerPrefs.GetString ("ControlScheme");

		if(m_ControlScheme == "Buttons")
			m_ButtonControlScheme.SetActive (true);
		else
			m_ButtonControlScheme.SetActive (false);


	}

    public void SetupPlayers(int number)
    {
        for (int i = 0; i < m_Players.Length; ++i)
        {
            m_Players[i].Active = false;
        }
        for (int i = 0; i < number; ++i)
        {
            m_Players[i].Active = true;
        }
    }

    void Update()
    {
        if (isActive)
        {
            m_AmountOfPlayers = 0;
            for (int i = 0; i < m_Players.Length; ++i)
            {
                if (m_Players[i].Active)
                {
                    m_AmountOfPlayers++;
                    m_Players[i].Colour = Managers.Palette.GetColourFromTheme(i);
                }
            }

			if (m_ControlScheme == "Touch")
				TouchInput ();
			else 
			{
				if (movingLeft && canInteract) 
				{
					Managers.Game.currentShape.movementController.MoveHorizontal(Vector2.left);
					canInteract = false;
					StartCoroutine(MenuDelay());
				}
				else if (movingRight && canInteract)
				{
					Managers.Game.currentShape.movementController.MoveHorizontal(Vector2.right);
					canInteract = false;
					StartCoroutine(MenuDelay());
				}
			}

            if (inputType == InputMethod.KeyboardInput)
                KeyboardInput(m_CurrentPlayer);
			/*
            else if (inputType == InputMethod.MouseInput)
                MouseInput();
            else if (inputType == InputMethod.TouchInput)
                TouchInput();
            else if (inputType == InputMethod.GamepadInput)
                GamepadInput();
                */
        }
    }

    IEnumerator MenuDelay()
    {
        yield return new WaitForSeconds(0.25f);
        canInteract = true;
    }

    #region KEYBOARD
    void KeyboardInput(int playerNum)
    {
        if(canInteract && Input.GetButton(m_Players[playerNum].UpButton))
        {
            Managers.Game.currentShape.movementController.RotateClockWise(false);
            canInteract = false;
            StartCoroutine(MenuDelay());
        }
        //if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow))
        //else if (Input.GetKeyDown(KeyCode.D))
        //    Managers.Game.currentShape.movementController.RotateClockWise(true);

         else if (canInteract && Input.GetAxis(m_Players[playerNum].HorizontalInput) < 0)
        {
            Managers.Game.currentShape.movementController.MoveHorizontal(Vector2.left);
            canInteract = false;
            StartCoroutine(MenuDelay());
        }
        else if (canInteract && Input.GetAxis(m_Players[playerNum].HorizontalInput) > 0)
        {
            Managers.Game.currentShape.movementController.MoveHorizontal(Vector2.right);
            canInteract = false;
            StartCoroutine(MenuDelay());
        }

        if (Input.GetButton(m_Players[playerNum].DownButton) && Managers.Game.currentShape != null)
        {
            Managers.Game.currentShape.movementController.InstantFall();
        }
        else if(Managers.Game.currentShape != null)
            Managers.Game.currentShape.movementController.StopFall();

    }
    #endregion

	#region BUTTONS

	public void RotateButton()
	{
		Managers.Game.currentShape.movementController.RotateClockWise(false);
	}

	public void StartMovingRight()
	{
		movingRight = true;
		movingLeft = false;
	}

	public void StopMovingRight()
	{
		movingRight = false;
	}

	public void StartMovingLeft()
	{
		movingLeft = true;
		movingRight = false;
	}

	public void StopMovingLeft()
	{
		movingLeft = false;
	}

	public void EnableFreefall()
	{
		if (Managers.Game.currentShape != null) 
		{
			Managers.Game.currentShape.movementController.InstantFall ();
		}
	}

	public void DisableFreefall()
	{
		if (Managers.Game.currentShape != null) 
		{
			Managers.Game.currentShape.movementController.StopFall ();
		}
	}
		

	#endregion

    #region MOUSE
    Vector2 _startPressPosition;
    Vector2 _endPressPosition;
    Vector2 _currentSwipe;
    float _buttonDownPhaseStart;
    public float tapInterval;

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            _startPressPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            _buttonDownPhaseStart = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - _buttonDownPhaseStart > tapInterval)
            {
                //save ended touch 2d point
                _endPressPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                //create vector from the two points
                _currentSwipe = new Vector2(_endPressPosition.x - _startPressPosition.x, _endPressPosition.y - _startPressPosition.y);

                //normalize the 2d vector
                _currentSwipe.Normalize();

                //swipe left
                if (_currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
                {
                    Managers.Game.currentShape.movementController.MoveHorizontal(Vector2.left);
                }
                //swipe right
                if (_currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
                {
                    Managers.Game.currentShape.movementController.MoveHorizontal(Vector2.right);
                }

                //swipe down
                if (_currentSwipe.y < 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f)
                {
                    if (Managers.Game.currentShape != null)
                    {
                        //isActive = false;
                        Managers.Game.currentShape.movementController.InstantFall();
                    }
                }

            }
            else
            {
                if (_startPressPosition.x < Screen.width / 2)
                    Managers.Game.currentShape.movementController.RotateClockWise(false);
                else
                    Managers.Game.currentShape.movementController.RotateClockWise(true);
            }
        }
        else if (Managers.Game.currentShape != null)
            Managers.Game.currentShape.movementController.StopFall();
    }
    #endregion

    #region TOUCH
    void TouchInput()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _startPressPosition = touch.position;
                _endPressPosition = touch.position;
                _buttonDownPhaseStart = Time.time;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                _endPressPosition = touch.position;
                //create vector from the two points
                _currentSwipe = new Vector2(_endPressPosition.x - _startPressPosition.x, _endPressPosition.y - _startPressPosition.y);

                //normalize the 2d vector
                _currentSwipe.Normalize();

                //swipe down
                if (_currentSwipe.y < 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f)
                {
                    if (Managers.Game.currentShape != null)
                    {
                        //isActive = false;
                        Managers.Game.currentShape.movementController.InstantFall();
                    }
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                if (Managers.Game.currentShape != null)
                    Managers.Game.currentShape.movementController.StopFall();
                if (Time.time - _buttonDownPhaseStart > tapInterval)
                {
                    //save ended touch 2d point
                    _endPressPosition = new Vector2(touch.position.x, touch.position.y);

                    //create vector from the two points
                    _currentSwipe = new Vector2(_endPressPosition.x - _startPressPosition.x, _endPressPosition.y - _startPressPosition.y);

                    //normalize the 2d vector
                    _currentSwipe.Normalize();

                    //swipe left
                    if (_currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
                    {
                        Managers.Game.currentShape.movementController.MoveHorizontal(Vector2.left);
                    }
                    //swipe right
                    if (_currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
                    {
                        Managers.Game.currentShape.movementController.MoveHorizontal(Vector2.right);
                    }
                }

                else if (_currentSwipe.x + _currentSwipe.y < 0.5f)
                {
                    if (_startPressPosition.x < Screen.width / 2)
                        Managers.Game.currentShape.movementController.RotateClockWise(false);
                    else
                        Managers.Game.currentShape.movementController.RotateClockWise(true);
                }
            }
        }

    }
    #endregion

    #region GAMEPAD

    void GamepadInput()
    {

    }

    #endregion
}
