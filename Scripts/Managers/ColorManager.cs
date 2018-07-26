using UnityEngine;
using System.Collections;

public enum ColorTheme
{
	PASTEL,
    GRAM,
    RGB
}

public class ColorManager : MonoBehaviour {

	public ColorTheme activeTheme;

    public Color[] themePack_Pastel;
    public Color[] themePack_Gram;
    public Color[] themePack_RGB;

    public void SwitchTheme()
    {
        switch (activeTheme)
        {
            case ColorTheme.PASTEL:
                activeTheme = ColorTheme.GRAM;
                break;
            case ColorTheme.GRAM:
                activeTheme = ColorTheme.RGB;
                break;
            case ColorTheme.RGB:
                activeTheme = ColorTheme.PASTEL;
                break;
            default:
                break;
        }
    }

	public Color TurnRandomColorFromTheme()
	{
		Color temp;
        int rand = 0;
        switch (activeTheme)
		{
			case ColorTheme.PASTEL:
                rand = Random.Range(0, themePack_Pastel.Length);
                temp = themePack_Pastel[rand];
				break;
            case ColorTheme.GRAM:
		        rand = Random.Range(0,themePack_Gram.Length); 
                temp = themePack_Gram[rand];
                break;
            case ColorTheme.RGB:
		        rand = Random.Range(0, themePack_RGB.Length); 
                temp = themePack_RGB[rand];
                break;
            default:
				temp = Color.black;
				break;
		}

		return temp;
	}

    public Color GetColourFromTheme(int index)
    {
        Color temp;
        switch (activeTheme)
        {
            case ColorTheme.PASTEL:
                temp = themePack_Pastel[index];
                break;
            case ColorTheme.GRAM:
                temp = themePack_Gram[index];
                break;
            case ColorTheme.RGB:
                temp = themePack_RGB[index];
                break;
            default:
                temp = Color.black;
                break;
        }
        return temp;
    }

}
