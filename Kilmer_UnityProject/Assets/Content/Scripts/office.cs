using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class office : MonoBehaviour
{

    public GameUI gameUi;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == 10)
        {
            Bus bus = collider.GetComponent<Bus>();

            gameUi.AddPlayerScore(bus.GetPoints(), bus.GetSettings().playerId);

            bus.EnterStation();
        }
    }
}
