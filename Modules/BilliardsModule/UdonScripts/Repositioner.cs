using Basis;
using Basis.Scripts.BasisSdk.Interactions;
using Basis.Scripts.Device_Management.Devices;
using System;

using UnityEngine;



public class Repositioner : MonoBehaviour
{
    [NonSerialized] public int idx;

    private BilliardsModule table;
    private BasisPickupInteractable pickup;

    public void _Init(BilliardsModule table_, int idx_)
    {
        table = table_;
        idx = idx_;

        pickup = (BasisPickupInteractable)GetComponent(typeof(BasisPickupInteractable));
        pickup.OnPickupUse += OnPickupUse;
        pickup.OnInteractStartEvent += OnPickup;
        pickup.OnInteractEndEvent += OnDrop;
    }

    private void OnPickupUse(BasisPickUpUseMode mode)
    {
        switch (mode)
        {
            case BasisPickUpUseMode.OnPickUpUseUp:
                OnPickupUseUp();
                break;
            case BasisPickUpUseMode.OnPickUpUseDown:
                OnPickupUseDown();
                break;
            case BasisPickUpUseMode.OnPickUpStillDown:

                break;
        }
    }

    public void OnPickup(BasisInput Input)
    {
        table.repositionManager._BeginReposition(this);
    }

    public  void OnDrop(BasisInput Input)
    {
        table.repositionManager._EndReposition(this);
    }

    public void OnPickupUseDown()
    {
        table.repositionManager.onUseDown();
    }

    public void OnPickupUseUp()
    {
        table.repositionManager.onUseUp();
    }

    public void _Drop()
    {
        pickup.Drop();
    }

    public void _Reset()
    {
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
    }
}
