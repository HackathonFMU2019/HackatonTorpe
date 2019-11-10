using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTrackUI : MonoBehaviour
{
    [SerializeField] private Image img;
    [SerializeField] private Image img2;
    [SerializeField] private Transform pointTarget;
    [SerializeField] private Transform pointTarget2;
    Camera thisCam;
    // Update is called once per frame

    private void Start()
    {
        thisCam = GetComponent<Camera>();
    }
    void Update()
    {
        TrackPlayer1();
        TrackPlayer2();
    }


    void TrackPlayer1()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minX;

        Vector2 pos = thisCam.WorldToScreenPoint(pointTarget.position);


        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
    }

    void TrackPlayer2()
    {
        float minX2 = img2.GetPixelAdjustedRect().width / 2;
        float maxX2 = Screen.width - minX2;

        float minY2 = img2.GetPixelAdjustedRect().height / 2;
        float maxY2 = Screen.height - minX2;


        Vector2 pos2 = thisCam.WorldToScreenPoint(pointTarget2.position);


        pos2.x = Mathf.Clamp(pos2.x, minX2, maxX2);
        pos2.y = Mathf.Clamp(pos2.y, minY2, maxY2);


        img2.transform.position = pos2;
    }
}
