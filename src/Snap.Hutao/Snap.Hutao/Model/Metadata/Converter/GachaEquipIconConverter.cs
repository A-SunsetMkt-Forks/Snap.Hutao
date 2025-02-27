// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Snap.Hutao.Core.Text;
using Snap.Hutao.UI.Xaml.Data.Converter;
using Snap.Hutao.Web.Endpoint.Hutao;

namespace Snap.Hutao.Model.Metadata.Converter;

internal sealed partial class GachaEquipIconConverter : ValueConverter<string, Uri>, IIconNameToUriConverter
{
    public static Uri IconNameToUri(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return default!;
        }

        string icon = default!;
        Interpolated.Parse(name, $"UI_{icon}");
        return new Uri(StaticResourcesEndpoints.StaticRaw("GachaEquipIcon", $"UI_Gacha_{icon}.png"));
    }

    public override Uri Convert(string from)
    {
        return IconNameToUri(from);
    }
}