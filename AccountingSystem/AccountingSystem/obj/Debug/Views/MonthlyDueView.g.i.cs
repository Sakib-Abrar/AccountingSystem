﻿#pragma checksum "..\..\..\Views\MonthlyDueView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "660C9CAB055F929A1148E973F2E1C8A1"
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
    /// MonthlyDueView
    /// </summary>
    public partial class MonthlyDueView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\Views\MonthlyDueView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dueDetails;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Views\MonthlyDueView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tableID;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Views\MonthlyDueView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tableName;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Views\MonthlyDueView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tableAccount;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Views\MonthlyDueView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tableAmount;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Views\MonthlyDueView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tableBalance;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\Views\MonthlyDueView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tableFine;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\Views\MonthlyDueView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tableIPaid;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\Views\MonthlyDueView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tableNextDate;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\Views\MonthlyDueView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tableTotal;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\Views\MonthlyDueView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\Views\MonthlyDueView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
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
            System.Uri resourceLocater = new System.Uri("/AccountingSystem;component/views/monthlydueview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\MonthlyDueView.xaml"
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
            this.dueDetails = ((System.Windows.Controls.DataGrid)(target));
            
            #line 20 "..\..\..\Views\MonthlyDueView.xaml"
            this.dueDetails.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dg1_SelectionChanged);
            
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
            this.tableAccount = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 5:
            this.tableAmount = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 6:
            this.tableBalance = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 7:
            this.tableFine = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 8:
            this.tableIPaid = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 9:
            this.tableNextDate = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 10:
            this.tableTotal = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 11:
            this.image = ((System.Windows.Controls.Image)(target));
            return;
            case 12:
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

