using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Shape
{
    public string shapeName;
    public Vector2 blockOne;
    public Vector2 blockTwo;
    public Vector2 blockThree;
    public Vector2 blockFour;
    public Vector2 pivot;
}
public class SpawnManager : MonoBehaviour {

	public GameObject[] shapeTypes;
    public Shape[] Shapes;
    public GameObject shape;
    public GameObject pivot;
    public GameObject previewShape;
    int nextShape;

    Vector2 StartPosition;
    GameObject[] childBlocks;
    void Start()
    {
        childBlocks = new GameObject[4];
        StartPosition = shape.transform.position;
        nextShape = -1;
    }

    public void Spawn()
	{

        if (nextShape == -1)
            nextShape = Random.Range(0, Shapes.Length);

        int i = nextShape;

		//// Random Shape
  // 		 int i = Random.Range(0, Shapes.Length);
        // Spawn Group at current Position
        shape.SetActive(true);
        shape.GetComponent<TetrisShape>().enabled = true;
        shape.transform.position = StartPosition;
        GameObject temp = GetComponent<ObjectPooler>().GetPooledObject();
        if (temp)
        {
            temp.transform.parent = shape.transform;
            temp.transform.localPosition = Shapes[i].blockOne;
            temp.SetActive(true);
            childBlocks[0] = temp;
        }
        temp = GetComponent<ObjectPooler>().GetPooledObject();
        if (temp)
        {
            temp.transform.parent = shape.transform;
            temp.transform.localPosition = Shapes[i].blockTwo;
            temp.SetActive(true);
            childBlocks[1] = temp;
        }
        temp = GetComponent<ObjectPooler>().GetPooledObject();
        if (temp)
        {
            temp.transform.parent = shape.transform;
            temp.transform.localPosition = Shapes[i].blockThree;
            temp.SetActive(true);
            childBlocks[2] = temp;
        }
        temp = GetComponent<ObjectPooler>().GetPooledObject();
        if (temp)
        {
            temp.transform.parent = shape.transform;
            temp.transform.localPosition = Shapes[i].blockFour;
            temp.SetActive(true);
            childBlocks[3] = temp;
        }
        pivot.transform.localPosition = Shapes[i].pivot;
//        Debug.Log(Shapes[i].shapeName);
        shape.GetComponent<TetrisShape>().AssignRandomColor();
        Managers.Game.currentShape = shape.GetComponent<TetrisShape>();
        if (Managers.Game.currentShape != null)
            Managers.Game.currentShape.movementController.StopFall();

        // setup next shape
        nextShape = Random.Range(0, Shapes.Length);
        ShapeHolder nextOne = previewShape.GetComponent<ShapeHolder>();

        nextOne.blocks[0].transform.localPosition = Shapes[nextShape].blockOne;

        nextOne.blocks[1].transform.localPosition = Shapes[nextShape].blockTwo;

        nextOne.blocks[2].transform.localPosition = Shapes[nextShape].blockThree;

        nextOne.blocks[3].transform.localPosition = Shapes[nextShape].blockFour;

        if (Managers.Input.m_AmountOfPlayers > 1)
        {
            int nextPlayer = (Managers.Input.m_CurrentPlayer + 1) % Managers.Input.m_AmountOfPlayers;
            nextOne.blocks[0].GetComponent<SpriteRenderer>().color = Managers.Palette.GetColourFromTheme(nextPlayer);
            nextOne.blocks[1].GetComponent<SpriteRenderer>().color = Managers.Palette.GetColourFromTheme(nextPlayer);
            nextOne.blocks[2].GetComponent<SpriteRenderer>().color = Managers.Palette.GetColourFromTheme(nextPlayer);
            nextOne.blocks[3].GetComponent<SpriteRenderer>().color = Managers.Palette.GetColourFromTheme(nextPlayer);

        }


        //GameObject temp =Instantiate(shapeTypes[i]) ;
        //Managers.Game.currentShape = temp.GetComponent<TetrisShape>();
        //temp.transform.parent = Managers.Game.blockHolder;
        Managers.Input.isActive = true;
    }

    public void DetachChildren()
    {
        for (int i = 0; i < childBlocks.Length; ++i)
        {
            childBlocks[i].transform.parent = shape.transform.parent;
        }
    }
}
