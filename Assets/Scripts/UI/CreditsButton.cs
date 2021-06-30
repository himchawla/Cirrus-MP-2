using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    public GameObject credits;
    // Start is called before the first frame update
    public void Onclick()
    {
        credits.SetActive(!credits.activeSelf);
    }
}
