﻿#pragma checksum "..\..\..\Views\SalaryView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DB04FE038744E13C9E41313F098C71ED"
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
    /// SalaryView
    /// </summary>
    public partial class SalaryView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\Views\SalaryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid salary;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Views\SalaryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tableID;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Views\SalaryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tableDate;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Views\SalaryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tableAmount;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Views\SalaryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tableBonus;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Views\SalaryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tableTotal;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\Views\SalaryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Amount;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\Views\SalaryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Bonus;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\Views\SalaryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EntryNo;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\Views\SalaryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Date;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\Views\SalaryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\Views\SalaryView.xaml"
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
            System.Uri resourceLocater = new System.Uri("/AccountingSystem;component/views/salaryview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\SalaryView.xaml"
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
            this.salary = ((System.Windows.Controls.DataGrid)(target));
            
            #line 21 "..\..\..\Views\SalaryView.xaml"
            this.salary.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dg1_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tableID = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 3:
            this.tableDate = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 4:
            this.tableAmount = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 5:
            this.tableBonus = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 6:
            this.tableTotal = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 7:
            this.Amount = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.Bonus = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.EntryNo = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.Date = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            
            #line 79 "..\..\..\Views\SalaryView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Save_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.image = ((System.Windows.Controls.Image)(target));
            return;
            case 13:
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

