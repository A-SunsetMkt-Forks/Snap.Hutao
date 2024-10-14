﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

namespace Snap.Hutao.Web.Hoyolab.Passport;

internal sealed class AuthTicketWrapper
{
    [JsonPropertyName("ticket")]
    public string Ticket { get; set; } = default!;
}