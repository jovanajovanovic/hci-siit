   M S B A M L   `   `   `  ����  MVVMSchedulerApplication.    (MVVMSchedulerApplication.AppointmentForm   5      a ]DevExpress.Xpf.Core.v18.1, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      f bDevExpress.Xpf.Scheduler.v18.1, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aCdxsch7http://schemas.devexpress.com/winfx/2008/xaml/scheduler  5      Mdxschi@http://schemas.devexpress.com/winfx/2008/xaml/scheduler/internal  5      -   5          >�Name$  AptForm��5
      $	��640��5      $	��300��6    � 	Resources 5      B  <DevExpress.Xpf.Scheduler.Drawing.TimeSpanToDateTimeConverter   timeSpanToDateTimeConverter<  6DevExpress.Xpf.Scheduler.Drawing.InvertedBoolConverter  invertedBoolConverter��%:   &	        &	        5   
   6-     5   
   6'   5      .��5      � $��12q�6    � ShowGridLines .   � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      -   5         ��Name$ stretchingLine��5      $��* �64   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   $��90��6,   �� 5       $��* �6   .+�5$   
   �� $��0��6   $��0��6!   $��Subject:��61   $��0,0,4,8q�6A   �� 5%   
   -   5         ��Name$ subjectEdit��5&      $��0,0,0,8q�5'      $��0��5(      $��1��5)      ��5%      �� *	cbItems+ �� Text 5+      �� *	Subject+ �� UpdateSourceTrigger$ PropertyChanged=�	 �� Mode$	 TwoWay=�5-      �� 54   
   $��1��6   $��0��6!   $
��Day:��61   $��0,0,4,8q�6=   %  DevExpress.Xpf.Editors.TextEdit  55   
   -   5        
  Name$
 edtStartDate��55      $	��150��6-   $��1��6<   $��1��6I   %  DevExpress.Xpf.Editors.BaseEdit   DisplayFormatString$
 dddd��56      $��0,0,0,8q�66      
IsReadOnly .    	EditValue 57      �� *DisplayStartDate+$ PropertyChanged=�$	 TwoWay=��� 5:   
   $��2��6   $��0��6!   $��Start time:��61   $��0,0,4,8q�6D     5;   
   -   5        $
 edtStartTime��5;      $	��150��6-      MaskType$ DateTime=�6<   $��2��5<   9   $��1��6F   $��0,0,0,8q�6V    5;   P   �� *TimeEditMask+   Mask 5<      �� *TimeEditMask+ 5=      �� *DisplayStartTime+ �� 	Converter# [  $ PropertyChanged=�$	 TwoWay=�5>      �� 5U   
   -   5        $��endTimeLabel��5U      $��3��6(   $��0��65   $��	End time:��6E   $��0,0,4,8q�6V     5V   
   -   5        $
 
edtEndTime��5V      $	��150��6+   $ DateTime=�6:   $��3��5W      $��1��6(   $��0,0,0,8q�68    .  5V   N   �� *TimeEditMask+ 6{   �� *TimeEditMask+ 5W   [   �� *DisplayEndTime+$	 TwoWay=�5X      �� 5m   
   $��4��6   $��0��6!   $��
Classroom:��61   $��0,0,4,8q�6C   9  3DevExpress.Xpf.Scheduler.UI.AppointmentResourceEdit  5n   
   -   5        $��resourceEdit��5n   (   $��4��6<   $��0,0,0,8q�6I   $��1��6Z   $��22��5o   {   <  6DevExpress.Xpf.Scheduler.UI.SchedulerBoundComboBoxEdit   SchedulerControl 5n   j   �� *	Control+ 5o   (   �� *
ResourceId+$ PropertyChanged=�$	 TwoWay=�6�   �� 5q   
   $��5��6   $��0��6!   $��Description:��61   $��0,0,4,8q�6E     5r   
   -	   5        $��descriptionEdit��5r      $��5��6.   $��1��6;   $��0,0,0,8q�6K   $��Stretch=�5s      $��50��63   )  #DevExpress.Xpf.Editors.TextEditBase   AcceptsReturn .    TextWrapping$
 Wrap=�5t   \   "   VerticalScrollBarVisibility$ Visible=�5u      $
��Left=�6=   $	��Top=�6_    5t      �� *Description+$	 TwoWay=�5u   _   �� 5x   
   $��6��6   $��1��6"   $��2��62    �� Orientation$ 
Horizontal=�5y      $��Right=�6.   .+�5z      �� -
   5         ��Name$ btnOk��5z      $��OK��5{      $��75��5~      $��6,0,0,0q�6   ��5}      �� * SaveAndCloseAppointmentCommand+5~      �� 5�      -   5        $ 	btnCancel��5�      $��Cancel��5�      $��75��5�   :   $��6,0,0,0q�6H   ��6   �� *CancelEditingCommand+6H   5�      5�      5�      