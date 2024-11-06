using System;
using System.Collections.Generic;
using UnityEngine;

public class ControlsController : MonoBehaviour
{
    void Start()
    {
        //Debug.Log("Print all controls: \n" + String.Join("\n", getCurrentControls()));
    }


    //Registers & initializes controls
    public List<Tuple<string, KeyCode>> getAllDefaultControls()
    {
        var defaultControls = new List<Tuple<string, KeyCode>>();
        defaultControls.Add(new Tuple<string, KeyCode>("controlsForward", KeyCode.W));
        defaultControls.Add(new Tuple<string, KeyCode>("controlsLeft", KeyCode.A));
        defaultControls.Add(new Tuple<string, KeyCode>("controlsBackwards", KeyCode.S));
        defaultControls.Add(new Tuple<string, KeyCode>("controlsRight", KeyCode.D));
        defaultControls.Add(new Tuple<string, KeyCode>("controlsJump", KeyCode.Space));
        defaultControls.Add(new Tuple<string, KeyCode>("controlsSneak", KeyCode.C));
        defaultControls.Add(new Tuple<string, KeyCode>("controlsSprint", KeyCode.LeftShift));
        defaultControls.Add(new Tuple<string, KeyCode>("controlsInteract", KeyCode.E));
        defaultControls.Add(new Tuple<string, KeyCode>("controlsReload", KeyCode.R));
        defaultControls.Add(new Tuple<string, KeyCode>("controlsShowPlayerStats", KeyCode.Tab));
        defaultControls.Add(new Tuple<string, KeyCode>("controlsEscapeMenu", KeyCode.Escape));
        defaultControls.Add(new Tuple<string, KeyCode>("controlsPrimaryThrowables", KeyCode.G));
        defaultControls.Add(new Tuple<string, KeyCode>("controlsKnife", KeyCode.F));
        defaultControls.Add(new Tuple<string, KeyCode>("controlsFire", KeyCode.Mouse0));
        defaultControls.Add(new Tuple<string, KeyCode>("controlsAim", KeyCode.Mouse1));
        return defaultControls;
    }

    public List<Tuple<string, KeyCode>> getCurrentControls()
    {
        //List with current controls
        List<Tuple<string, KeyCode>> currentControls = new List<Tuple<string, KeyCode>>();

        //Gets default controls
        List<Tuple<string, KeyCode>> defaultControls = getAllDefaultControls();

        for (int i = 0; i < defaultControls.Count; i++)
        {
            //Get saved key from pc
            KeyCode key = getKeyCodeFromPlayerPrefs(defaultControls[i].Item1);
            if (key == KeyCode.None)
            {
                //If key does not exist, then add default value(reset key)
                PlayerPrefs.SetString(defaultControls[i].Item1, defaultControls[i].Item2.ToString());
                Debug.Log("Required key " + defaultControls[i].Item1 + " not found! Replaced: " + defaultControls[i].Item2.ToString());
                currentControls.Add(new Tuple<string, KeyCode>(defaultControls[i].Item1, defaultControls[i].Item2));
            }
            //If key exists, add found key from PC
            currentControls.Add(new Tuple<string, KeyCode>(defaultControls[i].Item1, key));
        }
        return currentControls;
    }

    public void resetAllControls()
    {
        //Gets default controls
        List<Tuple<string, KeyCode>> defaultControls = getAllDefaultControls();

        for (int i = 0; i < defaultControls.Count; i++)
        {
            PlayerPrefs.SetString(defaultControls[i].Item1, defaultControls[i].Item2.ToString());
            Debug.Log("Controls resetet: " + defaultControls[i].Item1 + " Key: " + defaultControls[i].Item2);
        }
    }
     public KeyCode getKeyCodeFromPlayerPrefs(string playerPrefsName)
    {
        //Get saved control into string
        string keyString = PlayerPrefs.GetString(playerPrefsName);
        if (String.IsNullOrEmpty(keyString))
        {
            //Requested key not set or doesnt exists
            Debug.Log("Requested key " + playerPrefsName + " not set or doesnt exists");
            return KeyCode.None; 
        }

        //Convert string to KeyCode
        return (KeyCode) Enum.Parse(typeof(KeyCode), keyString);
    }
}