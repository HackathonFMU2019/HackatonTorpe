using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScritpt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void destroyCanvas()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
