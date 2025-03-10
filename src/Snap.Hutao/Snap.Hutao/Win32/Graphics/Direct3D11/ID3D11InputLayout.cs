// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Snap.Hutao.Win32.Graphics.Direct3D11;

[SupportedOSPlatform("windows6.1")]
internal static unsafe class ID3D11InputLayout
{
    internal static ref readonly Guid IID
    {
        get => ref MemoryMarshal.AsRef<Guid>([0xDC, 0x9D, 0x81, 0xE4, 0xF0, 0x4C, 0x25, 0x40, 0xBD, 0x26, 0x5D, 0xE8, 0x2A, 0x3E, 0x07, 0xB7]);
    }

    internal readonly struct Vftbl
    {
        internal readonly ID3D11DeviceChild.Vftbl ID3D11DeviceChildVftbl;
    }
}