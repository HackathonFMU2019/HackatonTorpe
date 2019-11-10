using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehaviour : MonoBehaviour
{
    [SerializeField] bool IsTrashCan;
    [SerializeField] GameObject trashIndicator;
    CharacterMotor player;
    public int trashID;

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<CharacterMotor>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            trashIndicator.SetActive(true);

            if (player.isFirstPlayer)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (IsTrashCan)
                        player.ThrowTrashInTrashCan(trashID);
                    else
                        player.Grab(this);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (IsTrashCan)
                        player.ThrowTrashInTrashCan(trashID);
                    else
                        player.Grab(this);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            trashIndicator.SetActive(false);
        }
    }
}
