using System;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for new scene management
using UnityStandardAssets.CrossPlatformInput;

public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // if we have forced a reset ...
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
            //... reload the scene using the new scene management API
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}



