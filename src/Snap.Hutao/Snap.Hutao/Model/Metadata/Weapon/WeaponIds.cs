// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Snap.Hutao.Model.Primitive;
using System.Collections.Frozen;

namespace Snap.Hutao.Model.Metadata.Weapon;

internal static class WeaponIds
{
    public static readonly WeaponId SwordFalcon = 11501;
    public static readonly WeaponId SwordDvalin = 11502;

    public static readonly WeaponId ClaymoreDvalin = 12501;
    public static readonly WeaponId ClaymoreWolfmound = 12502;

    public static readonly WeaponId PoleDvalin = 13502;
    public static readonly WeaponId PoleMorax = 13505;

    public static readonly WeaponId CatalystDvalin = 14501;
    public static readonly WeaponId CatalystFourwinds = 14502;

    public static readonly WeaponId BowDvalin = 15501;
    public static readonly WeaponId BowAmos = 15502;

    private static readonly FrozenSet<WeaponId> StandardWishIds =
    [
        SwordFalcon,
        SwordDvalin,
        ClaymoreDvalin,
        ClaymoreWolfmound,
        PoleDvalin,
        PoleMorax,
        CatalystDvalin,
        CatalystFourwinds,
        BowDvalin,
        BowAmos,
    ];

    public static bool IsStandardWish(in WeaponId weaponId)
    {
        return StandardWishIds.Contains(weaponId);
    }
}