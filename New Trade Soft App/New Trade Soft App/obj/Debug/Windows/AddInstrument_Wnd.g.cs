﻿#pragma checksum "..\..\..\Windows\AddInstrument_Wnd.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "47802583A51018409E1045B2B2E32F5F8E02790C8F4C865122D00930C4641DF4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using New_Trade_Soft_App.Windows;
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


namespace New_Trade_Soft_App.Windows {
    
    
    /// <summary>
    /// AddInstrument_Wnd
    /// </summary>
    public partial class AddInstrument_Wnd : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 46 "..\..\..\Windows\AddInstrument_Wnd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button close_btn;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\Windows\AddInstrument_Wnd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label attemt_lbl;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\Windows\AddInstrument_Wnd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox symb_cmb;
        
        #line default
        #line hidden
        
        
        #line 166 "..\..\..\Windows\AddInstrument_Wnd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox quotesIntrv_txt;
        
        #line default
        #line hidden
        
        
        #line 197 "..\..\..\Windows\AddInstrument_Wnd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox lot_txt;
        
        #line default
        #line hidden
        
        
        #line 222 "..\..\..\Windows\AddInstrument_Wnd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sbmt_btn;
        
        #line default
        #line hidden
        
        
        #line 231 "..\..\..\Windows\AddInstrument_Wnd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label bottom_txt;
        
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
            System.Uri resourceLocater = new System.Uri("/New_Trade_Soft_App;component/windows/addinstrument_wnd.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\AddInstrument_Wnd.xaml"
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
            
            #line 10 "..\..\..\Windows\AddInstrument_Wnd.xaml"
            ((New_Trade_Soft_App.Windows.AddInstrument_Wnd)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.close_btn = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\Windows\AddInstrument_Wnd.xaml"
            this.close_btn.Click += new System.Windows.RoutedEventHandler(this.close_btn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.attemt_lbl = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            
            #line 95 "..\..\..\Windows\AddInstrument_Wnd.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Image_MouseLeave);
            
            #line default
            #line hidden
            
            #line 95 "..\..\..\Windows\AddInstrument_Wnd.xaml"
            ((System.Windows.Controls.Image)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Image_MouseEnter);
            
            #line default
            #line hidden
            return;
            case 5:
            this.symb_cmb = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.quotesIntrv_txt = ((System.Windows.Controls.ComboBox)(target));
            
            #line 178 "..\..\..\Windows\AddInstrument_Wnd.xaml"
            this.quotesIntrv_txt.GotFocus += new System.Windows.RoutedEventHandler(this.lot_txt_GotFocus);
            
            #line default
            #line hidden
            return;
            case 7:
            this.lot_txt = ((System.Windows.Controls.TextBox)(target));
            
            #line 210 "..\..\..\Windows\AddInstrument_Wnd.xaml"
            this.lot_txt.GotFocus += new System.Windows.RoutedEventHandler(this.lot_txt_GotFocus);
            
            #line default
            #line hidden
            return;
            case 8:
            this.sbmt_btn = ((System.Windows.Controls.Button)(target));
            
            #line 222 "..\..\..\Windows\AddInstrument_Wnd.xaml"
            this.sbmt_btn.Click += new System.Windows.RoutedEventHandler(this.sbmt_btn_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.bottom_txt = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

