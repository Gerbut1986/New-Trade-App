﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A0CF2A1B5AFF22A002D8E9B99A481DEE89578CDC914DAE4B128FE5A9EBFDFA2F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using New_Trade_Soft_App;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace New_Trade_Soft_App {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 124 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid userGrid;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox servers_cmb;
        
        #line default
        #line hidden
        
        
        #line 172 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addConn_btn;
        
        #line default
        #line hidden
        
        
        #line 190 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox user_txt;
        
        #line default
        #line hidden
        
        
        #line 217 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox pass_txt;
        
        #line default
        #line hidden
        
        
        #line 229 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label user_lbl;
        
        #line default
        #line hidden
        
        
        #line 230 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label pass_lbl;
        
        #line default
        #line hidden
        
        
        #line 231 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label userEmail_lbl;
        
        #line default
        #line hidden
        
        
        #line 240 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label mail_lbl;
        
        #line default
        #line hidden
        
        
        #line 255 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label date_lbl;
        
        #line default
        #line hidden
        
        
        #line 265 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button disconect_btn;
        
        #line default
        #line hidden
        
        
        #line 279 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button setInst_btn;
        
        #line default
        #line hidden
        
        
        #line 317 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button stop_btn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/New_Trade_Soft_App;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\MainWindow.xaml"
            ((New_Trade_Soft_App.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 8 "..\..\MainWindow.xaml"
            ((New_Trade_Soft_App.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.userGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 127 "..\..\MainWindow.xaml"
            this.userGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.servers_cmb = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.addConn_btn = ((System.Windows.Controls.Button)(target));
            
            #line 172 "..\..\MainWindow.xaml"
            this.addConn_btn.Click += new System.Windows.RoutedEventHandler(this.addConn_btn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.user_txt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.pass_txt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.user_lbl = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.pass_lbl = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.userEmail_lbl = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.mail_lbl = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.date_lbl = ((System.Windows.Controls.Label)(target));
            return;
            case 12:
            this.disconect_btn = ((System.Windows.Controls.Button)(target));
            
            #line 265 "..\..\MainWindow.xaml"
            this.disconect_btn.Click += new System.Windows.RoutedEventHandler(this.disconect_btn_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.setInst_btn = ((System.Windows.Controls.Button)(target));
            
            #line 279 "..\..\MainWindow.xaml"
            this.setInst_btn.Click += new System.Windows.RoutedEventHandler(this.setInst_btn_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.stop_btn = ((System.Windows.Controls.Button)(target));
            
            #line 317 "..\..\MainWindow.xaml"
            this.stop_btn.Click += new System.Windows.RoutedEventHandler(this.stop_btn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

