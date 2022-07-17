using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    [SerializeField]
    private Dice _dice;

    [SerializeField]
    private GameObject _winUI;

    private int _isInVictoryTrigger;

    public void CheckWon()
    {
        if (_isInVictoryTrigger > 0)
        {
            Win();
        }
    }

    private void Win()
    {
        _dice.Block();
        Instantiate(_winUI);
    }

    public void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Victory")
            _isInVictoryTrigger++;
    }

    public void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Victory")
            _isInVictoryTrigger--;
    }
}
