﻿using FluentHub.DataModels;
using FluentHub.UserControls;
using FluentHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace FluentHub.Views
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class UserIssuesList : Page
    {
        UserIssueListViewModel vm = new UserIssueListViewModel();

        public UserIssuesList()
        {
            this.DataContext = vm;
            this.InitializeComponent();
        }

        private void ItemsRepeater_Loaded(object sender, RoutedEventArgs e)
        {
            vm.GetUserIssues();
        }
    }
}