using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonURL : MonoBehaviour
{
    public void LoadUrl (string url)
    {
        Application.OpenURL(url);
    }
}
