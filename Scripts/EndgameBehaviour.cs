using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameBehaviour : MonoBehaviour
{
   public void RestaartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MenuGo()
    {
        SceneManager.LoadScene("Menu");
    }
}
