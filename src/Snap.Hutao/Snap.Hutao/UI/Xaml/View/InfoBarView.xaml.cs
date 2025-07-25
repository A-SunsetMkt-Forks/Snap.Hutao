// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Snap.Hutao.Service.Notification;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace Snap.Hutao.UI.Xaml.View;

[DependencyProperty("InfoBars", typeof(ObservableCollection<InfoBarOptions>))]
internal sealed partial class InfoBarView : UserControl
{
    public InfoBarView()
    {
        InitializeComponent();
        DataContext = this;

        IServiceProvider serviceProvider = Ioc.Default;
        IInfoBarService infoBarService = serviceProvider.GetRequiredService<IInfoBarService>();
        InfoBars = infoBarService.Collection;
        InfoBars.CollectionChanged += OnInfoBarsCollectionChanged;
        Unloaded += OnUnloaded;
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        InfoBars.CollectionChanged -= OnInfoBarsCollectionChanged;
    }

    private void OnInfoBarsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
    {
        HandleInfoBarsCollectionChangedAsync(args).SafeForget();

        [SuppressMessage("", "SH003")]
        async Task HandleInfoBarsCollectionChangedAsync(NotifyCollectionChangedEventArgs args)
        {
            if (InfoBars.Count > 0)
            {
                if (VisibilityRoot is not null)
                {
                    VisibilityRoot.Visibility = Visibility.Visible;
                }
            }

            if (args.Action is NotifyCollectionChangedAction.Add)
            {
                if (InfoBarPanelTransitionHelper is not null)
                {
                    InfoBarPanelTransitionHelper.Source = ShowButtonBorder;
                    InfoBarPanelTransitionHelper.Target = InfoBarItemsBorder;

                    if (ShowButtonBorder is not null && VisualTreeHelper.GetParent(ShowButtonBorder) is not null &&
                        InfoBarItemsBorder is not null && VisualTreeHelper.GetParent(InfoBarItemsBorder) is not null)
                    {
                        try
                        {
                            await InfoBarPanelTransitionHelper.StartAsync().ConfigureAwait(true);
                        }
                        catch (COMException)
                        {
                            // 0x8000FFFF Catastrophic failure
                            // Happened when the app is exiting
                        }
                    }
                }

                // After adding, InfoBars.Count is not 0, so we skip the following code
                if (InfoBars.Count > 0)
                {
                    return;
                }
            }

            if (InfoBars.Count is 0)
            {
                if (InfoBarPanelTransitionHelper is not null)
                {
                    InfoBarPanelTransitionHelper.Source = InfoBarItemsBorder;
                    InfoBarPanelTransitionHelper.Target = ShowButtonBorder;

                    if (InfoBarItemsBorder is not null && VisualTreeHelper.GetParent(InfoBarItemsBorder) is not null &&
                        ShowButtonBorder is not null && VisualTreeHelper.GetParent(ShowButtonBorder) is not null)
                    {
                        try
                        {
                            await InfoBarPanelTransitionHelper.StartAsync().ConfigureAwait(true);
                        }
                        catch (COMException)
                        {
                            // 0x8000FFFF Catastrophic failure
                            // Happened when the app is exiting
                        }
                    }
                }

                if (VisibilityRoot is not null)
                {
                    try
                    {
                        VisibilityRoot.Visibility = Visibility.Collapsed;
                    }
                    catch (COMException)
                    {
                        // 0x8000FFFF Catastrophic failure
                        // Happened when the app is exiting
                    }
                }
            }
        }
    }

    private void OnInfoBarClosed(InfoBar sender, InfoBarClosedEventArgs args)
    {
        try
        {
            InfoBars.Remove((InfoBarOptions)sender.DataContext);
        }
        catch (COMException)
        {
            // 0x80004005
        }
    }

    private void OnClearAllButtonClick(object sender, RoutedEventArgs e)
    {
        RemoveInfoBarsAsync().SafeForget();

        [SuppressMessage("", "SH003")]
        async Task RemoveInfoBarsAsync()
        {
            try
            {
                while (InfoBars.Count > 0)
                {
                    InfoBars.RemoveLast();
                    await Task.Delay(50).ConfigureAwait(true);
                }
            }
            catch (COMException)
            {
                // 0x8000FFFF Catastrophic failure
                // Happened when the app is exiting
            }
        }
    }
}