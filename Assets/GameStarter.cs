using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void StartGame()
    {
        SceneManager.LoadScene("demo");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
