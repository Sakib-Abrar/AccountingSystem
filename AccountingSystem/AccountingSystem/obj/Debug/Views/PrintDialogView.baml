   M S B A M L   `   `   `  ����K  GAccountingSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:AccountingSystem.ViewsAccountingSystem.Views  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   ,    &AccountingSystem.Views.PrintDialogView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:AccountingSystem.Views     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	     � Title$  Print Options��5	   	   $��10��5
   	    � SizeToContent$ WidthAndHeight=�5   	   $	��100��5   	    � 
ResizeMode$ CanResizeWithGrip=�5   	    � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=�5   	   .��5      � .+�5   
   � $��15q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� -   5        $��Label1��5      $��1��6"   .��6/   Print Data From6A   (  "System.Windows.Controls.DatePicker  5      -   5        $��FromDatePicker��5      $
��Left=�6/   $��1��6J   $��2��6W   $	��Top=�6g   
 �� Now   SelectedDate# Z 6   �� 5      -   5        $��Label2��5      $��2��6"   .��6/   To63     5      -   5        $��ToDatePicker��5      $
��Left=�6-   $��2��6H   $��2��6U   $	��Top=�6e   # Z 6}   � 5!      $��3��6   $��2��6%   $��Right=�69   $��	0,10,10,5q�6U   .+�5"      �� -   5        $��btnDialogOk��5"   *   	 �� 	IsDefault	 . $��60��6W   $��0,0,10,0q�6e   .��6w   _Ok6|   �� 5#      
 �� IsCancel
 . $��60��6)   .��67   	_Cancel6@   5$      5%      5'      5(      