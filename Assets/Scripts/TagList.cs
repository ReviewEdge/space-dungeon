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
    public const string gunDropTag = "Gun";
    public const string swordDropTag = "Sword";
    public const string NumPadTag = "NumPad";
    public const string PrisonerTag = "Prisoner";

    public const string playerBulletLayer = "Player Bullet";
    public const string enemyBulletLayer = "Enemy Bullet";

    public const string swordSwipe4 = "SwordSwipe4";

    //weapons
    public enum weaponType
    {
        LaserSword,
        RayGun
    }
    public enum directions
    {
        right,
        up,
        down,
        left
    }
}
