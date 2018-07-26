using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeChanger : MonoBehaviour {

    public Text themeName;

    public void OnClickChangeTheme()
    {
        Managers.Palette.SwitchTheme();
        switch (Managers.Palette.activeTheme)
        {
            case ColorTheme.GRAM:
                themeName.text = "GRAM";
                break;
            case ColorTheme.PASTEL:
                themeName.text = "PASTEL";
                break;
            case ColorTheme.RGB:
                themeName.text = "RGB";
                break;
            default:
                themeName.text = "???";
                break;
        }

    }
}
