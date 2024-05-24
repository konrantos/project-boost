using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitAplication : MonoBehaviour
{

// Με escape κλείνει το παιχνίδι
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();

        }
    }
}