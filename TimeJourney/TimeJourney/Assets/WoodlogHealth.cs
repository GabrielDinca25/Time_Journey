﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodlogHealth : Health
{
    public GameObject watermapBlocked;
    public GameObject watermapFree;

    public override void GetDamage(int dmgAmount)
    {
        watermapBlocked.SetActive(false);
        watermapFree.SetActive(true);
        Destroy(gameObject);
    }
}