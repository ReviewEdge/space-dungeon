using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagList
{
    public const string playerTag = "Player";
    public const string enemyTag = "Enemy";
    public const string bulletTag = "Bullet";
    public const string wallTag = "Wall";
    public const string weaponTag = "Weapon";
    public const string healthpackTag = "Healthpack";
    public const string speedUpTag = "SpeedUp";
    public const string gunDropTag = "Gun";
    public const string sniperDropTag = "Sniper";
    public const string swordDropTag = "Sword";
    public const string NumPadTag = "NumPad";
    public const string PrisonerTag = "Prisoner";

    public const string playerBulletLayer = "Player Bullet";
    public const string enemyBulletLayer = "Enemy Bullet";

    public const string swordSwipe4 = "SwordSwipe4";
    public const string finalLevel = "Level11";
    
}
public enum WeaponType
{
    LaserSword,
    RayGun,
    Sniper,
    Punch
}

public class Weapon {
    public WeaponType weaponType;
    public int ammo;

    public Weapon(WeaponType weaponType, int ammo = 0) {
        this.weaponType = weaponType;
        this.ammo = ammo;
    }
}

