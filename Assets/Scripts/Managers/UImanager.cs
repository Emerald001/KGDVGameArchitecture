using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager
{
    private Text ammoText;
    public UImanager(Text _ammoText)
    {
        ammoText = _ammoText;
    }

    public void OnEnter()
    {
        EventManager<int,int>.Subscribe(EventType.AMMO_CHANGED,ChangeAmmoText);
    }

    private void ChangeAmmoText(int _newAmmoCount, int _maxAmmo)
    {
        Debug.Log("event UI triggered");
        ammoText.text = "AMMO " + _newAmmoCount + "/" + _maxAmmo;
    }

}
