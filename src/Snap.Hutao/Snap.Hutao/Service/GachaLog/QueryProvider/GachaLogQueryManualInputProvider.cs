// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Snap.Hutao.Factory.ContentDialog;
using Snap.Hutao.UI.Xaml.View.Dialog;
using System.Collections.Specialized;
using System.Web;

namespace Snap.Hutao.Service.GachaLog.QueryProvider;

[ConstructorGenerated]
[Injection(InjectAs.Transient, typeof(IGachaLogQueryProvider), Key = RefreshOption.ManualInput)]
internal sealed partial class GachaLogQueryManualInputProvider : IGachaLogQueryProvider
{
    private readonly IContentDialogFactory contentDialogFactory;
    private readonly CultureOptions cultureOptions;

    /// <inheritdoc/>
    public async ValueTask<ValueResult<bool, GachaLogQuery>> GetQueryAsync()
    {
        GachaLogUrlDialog dialog = await contentDialogFactory.CreateInstanceAsync<GachaLogUrlDialog>().ConfigureAwait(false);
        (bool isOk, string url) = await dialog.GetInputUrlAsync().ConfigureAwait(false);

        if (!isOk)
        {
            return new(false, default);
        }

        string? queryString = Match(url, "index.html") ?? Match(url, "getGachaLog");
        if (queryString is null)
        {
            return new(false, GachaLogQuery.Invalid(SH.ServiceGachaLogUrlProviderManualInputInvalid));
        }

        NameValueCollection query = HttpUtility.ParseQueryString(queryString);

        if (!query.TryGetSingleValue("auth_appid", out string? appId) || appId is not "webview_gacha")
        {
            return new(false, GachaLogQuery.Invalid(SH.ServiceGachaLogUrlProviderManualInputInvalid));
        }

        string? queryLanguageCode = query["lang"];
        if (!cultureOptions.LanguageCodeFitsCurrentLocale(queryLanguageCode))
        {
            string message = SH.FormatServiceGachaLogUrlProviderUrlLanguageNotMatchCurrentLocale(queryLanguageCode, cultureOptions.LanguageCode);
            return new(false, GachaLogQuery.Invalid(message));
        }

        return new(true, new(url));
    }

    private static string? Match(string url, string match)
    {
        ReadOnlySpan<char> urlSpan = url;

        int index = urlSpan.LastIndexOf(match);
        if (index >= 0)
        {
            index += match.Length;
            return urlSpan[index..].ToString();
        }

        return null;
    }
}