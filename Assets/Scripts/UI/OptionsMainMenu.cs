using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMainMenu : MonoBehaviour
{
    public GameObject[] OptionContent;
    public void onClick()
    {
        foreach (GameObject go in OptionContent)
        {
            go.SetActive(!go.activeSelf);
        }
    }
}
