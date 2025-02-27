// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Snap.Hutao.ViewModel;

namespace Snap.Hutao.UI.Xaml.View;

internal sealed partial class TitleView : UserControl
{
    public TitleView()
    {
        this.InitializeDataContext<TitleViewModel>();
        InitializeComponent();
    }

    public FrameworkElement DragArea { get => DraggableGrid; }

    public IEnumerable<FrameworkElement> Passthrough { get; } = [];
}