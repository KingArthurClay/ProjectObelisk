using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Info", menuName = "Player/Player Info")]
/// <summary>
/// Scriptable object so that you can assign initial values to the player at the beginning of a level.
/// No current weapon because that will always start as null in the beginning of a level.
/// </summary>
public class PlayerInfoObject : ScriptableObject
{
    public AmmoDictionary Ammo;
    public float Health;
    public float MaxHealth;
    public WeaponHashSet Weapons;
    public Vector3 StartPosition;
}

/// <summary>
/// The actual player info that will be used by player state
/// </summary>
public class PlayerInfo {
    public AmmoDictionary Ammo;
    public float Health;
    public float MaxHealth;
    public WeaponHashSet Weapons;
    public Weapon CurrentWeapon;
    public Vector3 Position;

    /// <summary>
    /// Copies over values from the info object instead of getting references to them
    /// </summary>
    /// <param name="infoObject"></param>
    public PlayerInfo(PlayerInfoObject infoObject) {
        Ammo = infoObject.Ammo.CreateCopy();
        Health = infoObject.Health;
        MaxHealth = infoObject.MaxHealth;
        Weapons = infoObject.Weapons.CreateCopy();
        CurrentWeapon = null;
        Position = infoObject.StartPosition;
    }

    public PlayerInfo() {
        Ammo = new AmmoDictionary();
        Health = 0;
        MaxHealth = 0;
        Weapons = new WeaponHashSet();
        Position = new Vector3();
        CurrentWeapon = null;
        Position = new Vector3();
    }

    public PlayerInfo CreateCopy() {
        return new PlayerInfo() {
            Ammo = this.Ammo.CreateCopy(),
            Health = this.Health,
            MaxHealth = this.MaxHealth,
            Weapons = this.Weapons.CreateCopy(),
            Position = this.Position,
            CurrentWeapon = this.CurrentWeapon
        };
    }
}

