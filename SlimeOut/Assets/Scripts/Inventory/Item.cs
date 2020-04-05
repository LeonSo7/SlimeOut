﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType {
        blue1,
        blue2,
        blue3,
        red1,
        red2,
        red3,
        green1,
        green2,
        green3,
        xp1,
        xp2,
        xp3,
    }

    public ItemType type;

    public Item (ItemType t) {
        type = t;
    }

    public Sprite GetSprite() {
        switch (type) {
            default:
            case ItemType.blue1:    return ItemAssets.instance.blue1Sprite;
            case ItemType.blue2:    return ItemAssets.instance.blue2Sprite;
            case ItemType.blue3:    return ItemAssets.instance.blue3Sprite;
            case ItemType.red1:     return ItemAssets.instance.red1Sprite;
            case ItemType.red2:     return ItemAssets.instance.red2Sprite;
            case ItemType.red3:     return ItemAssets.instance.red3Sprite;
            case ItemType.green1:   return ItemAssets.instance.green1Sprite;
            case ItemType.green2:   return ItemAssets.instance.green2Sprite;
            case ItemType.green3:   return ItemAssets.instance.green3Sprite;
            case ItemType.xp1:       return ItemAssets.instance.xp1Sprite;
            case ItemType.xp2:       return ItemAssets.instance.xp2Sprite;
            case ItemType.xp3:       return ItemAssets.instance.xp3Sprite;
        }
    }
    public int GetCost() {
        switch (type) {
            default:
            case ItemType.blue1:    return 1;
            case ItemType.blue2:    return 2;
            case ItemType.blue3:    return 3;
            case ItemType.red1:     return 1;
            case ItemType.red2:     return 2;
            case ItemType.red3:     return 3;
            case ItemType.green1:   return 1;
            case ItemType.green2:   return 2;
            case ItemType.green3:   return 3;
            case ItemType.xp1:       return 1;
            case ItemType.xp2:       return 2;
            case ItemType.xp3:       return 3;
        }
    }
    public string GetDescription() {
        switch (type) {
            default:
            case ItemType.blue1:    return "blue food level 1";
            case ItemType.blue2:    return "blue food level 2";
            case ItemType.blue3:    return "blue food level 3";
            case ItemType.red1:     return "red food level 1";
            case ItemType.red2:     return "red food level 2";
            case ItemType.red3:     return "red food level 3";
            case ItemType.green1:   return "green food level 1";
            case ItemType.green2:   return "green food level 2";
            case ItemType.green3:   return "green food level 3";
            case ItemType.xp1:       return "exp level 1";
            case ItemType.xp2:       return "exp level 2";
            case ItemType.xp3:       return "exp food level 3";
        }
    }
}
