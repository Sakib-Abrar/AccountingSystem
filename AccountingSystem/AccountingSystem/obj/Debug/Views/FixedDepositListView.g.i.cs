﻿#pragma checksum "..\..\..\Views\FixedDepositListView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "09D36EDBA6F7CC43F0B15924FBAE201E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AccountingSystem.Views;
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


namespace AccountingSystem.Views {
    
    
    /// <summary>
    /// FixedDepositListView
    /// </summary>
    public partial class FixedDepositListView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Views\FixedDepositListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid generalDepositlist;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Views\FixedDepositListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tableID;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Views\FixedDepositListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tableName;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Views\FixedDepositListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tablenid;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Views\FixedDepositListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tablecell;
        
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
            System.Uri resourceLocater = new System.Uri("/AccountingSystem;component/views/fixeddepositlistview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\FixedDepositListView.xaml"
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
            this.generalDepositlist = ((System.Windows.Controls.DataGrid)(target));
            
            #line 11 "..\..\..\Views\FixedDepositListView.xaml"
            this.generalDepositlist.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.generalDepositlist_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 11 "..\..\..\Views\FixedDepositListView.xaml"
            this.generalDepositlist.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.generalDepositlist_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tableID = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 3:
            this.tableName = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 4:
            this.tablenid = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 5:
            this.tablecell = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

