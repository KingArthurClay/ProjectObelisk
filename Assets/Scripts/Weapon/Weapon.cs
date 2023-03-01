using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum WeaponType { Gun, Melee }
public enum AmmoType { Explosive, Pistol, Energy, Shotgun, NONE }

#region Event Args
public class OnWeaponAmmoChangedArgs : EventArgs {
    private int _oldAmount; public int OldAmount {get => _oldAmount;}
    private int _newAmount; public int NewAmount {get => _newAmount;}
    public OnWeaponAmmoChangedArgs(int oldAmount, int newAmount) {
        _oldAmount = oldAmount;
        _newAmount = newAmount;
    }
}
#endregion

public abstract class Weapon : MonoBehaviour
{

    [Header("Attack Point")]
    // Where the attack comes from (i.e. where bullets come from or where melee hits) 
    [SerializeField] protected Transform _attackPoint;

    [Header("Weapon Info")]
    [SerializeField] private WeaponItem _weaponItem; public WeaponItem WeaponItem { get => _weaponItem; }
    [SerializeField] private List<MeshCollider> _colliders; public List<MeshCollider> Colliders{get => _colliders;}
    // The damage an attack does
    [SerializeField] protected float _damage;
    [SerializeField] protected WeaponType _weaponType;

    [Header("Ammo Costs/Types")]
    [SerializeField] protected int _ammoAmount1; public int AmmoAmount1 { 
        get => _ammoAmount1;
        protected set {
            int oldAmount = _ammoAmount1;
            _ammoAmount1 = value;
            OnWeaponAmmoChanged?.Invoke(this,
            new OnWeaponAmmoChangedArgs(oldAmount, _ammoAmount1));
        } 
    }

    protected GameObject _holder = null; public GameObject Holder { get => _holder; }

    private bool _canPlace = true; public bool CanPlace {get => _canPlace; 
    set {
        _canPlace = value;
    }}

    #region Events
    public delegate void OnWeaponAmmoChangedHandler(object sender, EventArgs e);
    public event OnWeaponAmmoChangedHandler OnWeaponAmmoChanged;
    #endregion

    public void InitializeWeapon(int ammoAmount1, int ammoAmount2) {
        _holder = null;
        transform.parent = null;
        foreach (MeshCollider collider in _colliders) {
            collider.enabled = true;
        }
        transform.GetComponent<Rigidbody>().isKinematic = false;
        _ammoAmount1 = ammoAmount1;
    }

    /// <summary>
    /// Picks up this weapon and puts it in the proper position
    /// </summary>
    /// <param name="holder"></param>
    /// <param name="equipPos"></param>
    public void PickUpWeapon(GameObject holder, Transform equipPos)
    {
        _holder = holder;
        transform.parent = equipPos;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(_holder.transform.forward);
        foreach (MeshCollider collider in _colliders) {
            collider.enabled = false;
        }
        //transform.GetComponent<BoxCollider>().enabled = false;
        transform.GetComponent<Rigidbody>().isKinematic = true;
    }

    /// <summary>
    /// Drops this weapon
    /// </summary>
    public virtual void DropWeapon()
    {
        _holder = null;
        transform.parent = null;

        foreach (MeshCollider collider in _colliders) {
            collider.enabled = true;
        }

        //transform.GetComponent<BoxCollider>().enabled = true;
        transform.GetComponent<Rigidbody>().isKinematic = false;
        if (AmmoAmount1 == 0) {
            Destroy(gameObject);
        }
    }

    // Use ammo defaults to false because player is the only
    // case where ammo is going to be used
    public virtual void Fire1Start(bool useAmmo = false) { }
    public virtual void Fire1Stop(bool useAmmo = false) { }
    public virtual void Fire1Held(bool useAmmo = false) { }

    public virtual void Fire2(bool useAmmo = false) { }
    public virtual void Fire2Stop(bool useAmmo = false) { }

    protected DamageInfo CreateDamageInfo() {
        return new DamageInfo {
            damage = _damage,
            attacker = _holder,
            ammoType = _weaponItem.AmmoType1
        };
    }
}

