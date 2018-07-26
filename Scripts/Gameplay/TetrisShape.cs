using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public enum ShapeType
{
    I,
    T,
    O,
    L,
    J,
    S,
    Z
}



public class TetrisShape : MonoBehaviour
{
    [HideInInspector]
    public ShapeType type;

    public Vector2[] blockPositions;

    [HideInInspector]
    public ShapeMovementController movementController;

    void Awake()
    {
        movementController = GetComponent<ShapeMovementController>();
        //AssignRandomColor();
    }

    void Start()
    {
        blockPositions = new Vector2[4];


    }

    public void AssignRandomColor()
    {
        // Default position not valid? Then it's game over
        if (!Managers.Grid.IsValidGridPosition(this.transform))
        {
            Debug.LogError("Invalid!");
            Managers.Game.SetState(typeof(GameOverState));
            this.gameObject.SetActive(false);
        }

//        Debug.LogWarning(Managers.Input.m_AmountOfPlayers);

        Color temp;
        if (Managers.Input.m_AmountOfPlayers != 1)
            temp = Managers.Palette.GetColourFromTheme(Managers.Input.m_CurrentPlayer); //Managers.Input.m_Players[Managers.Input.m_CurrentPlayer].Colour;
        else
            temp = Managers.Palette.TurnRandomColorFromTheme();

        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>().ToList())
            renderer.color = temp;
    }
}
