   M S B A M L   `   `   `  ����S  OMVVMSchedulerApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:MVVMSchedulerApplication.Ucionice!MVVMSchedulerApplication.Ucionice  5   	   C&clr-namespace:MVVMSchedulerApplicationMVVMSchedulerApplication  5
   	   6    0MVVMSchedulerApplication.Ucionice.IzmenaUcionice   5   	   a ]DevExpress.Xpf.Core.v18.1, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:MVVMSchedulerApplication.Ucionice     5   	   j fDevExpress.Xpf.LayoutControl.v18.1, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  5   
   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5   
   .l&clr-namespace:MVVMSchedulerApplication  5
   	     � Icon$  ../util/edit1.png��5   	   +   %MVVMSchedulerApplication.HelpProvider   HelpKey$ index��5   	    � Title$ EDIT CLASSROOM��5   	   $��17(�6    $	��600��6.   $	��400��6;    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$ ApplicationCommands.Help��5      6<   .��5      >� .��5   
   � .+�5      0  *DevExpress.Xpf.LayoutControl.LayoutControl  .  (DevExpress.Xpf.LayoutControl.LayoutGroup   Orientation$ Vertical=�6!   .+�5        .+�5        -   5          Name$ layoutGroup��5   '   $ Vertical=�6<   $	��Top=�6S   .+�5      -  'DevExpress.Xpf.LayoutControl.LayoutItem  -   5        $��id_edit��5   <      
IsRequired .    LabelPosition$	 Top=�6K   5        5       . 	   Label$	 Description:��6<   $	 Top=�6Q   
   Content.
 5      �� -   5        $��des_edit��5   &   $	��200��66   $
 name��6E   5        5       . $	 
Projector:��6<   $	 Top=�6O   .
 5      �� -   5        $��projector_edit��5   '    � ToolTip 5   "   �� 5   &   $��5q�60   $
��Wrap=�6;   $	��100��6O   42You can choose whether the school has a projector.5!   '   .@�5#   "   �� .��6/   True65   �� 5$   "   .��6/   False66   5%      5&        5'       . $	 Table:��6<   $	 Top=�6K   .
 5(      �� -   5        $��
table_edit��5(   '    5)   "   �� 5*   &   $��5q�60   $
��Wrap=�6;   $	��100��6O   0.You can choose whether the school has a board.5,   '   .@�5.   "   �� .��6/   True65   �� 5/   "   .��6/   False66   50      51        52       . $	 Smart table:��6<   $	 Top=�6Q   .
 53      �� -   5        $��st_edit��53   '    54   "   �� 55   &   $��5q�60   $
��Wrap=�6;   $	��100��6O   64You can choose whether the school has a smart board.57   '   .@�59   "   �� .��6/   True65   �� 5:   "   .��6/   False66   5;      5<        5>       . $	 Operation system :��6<   $	 Top=�6W   .
 5?      �� -   5        $��os_edit��5?   '    5@   "   �� 5A   &   $��5q�60   $
��Wrap=�6;   $	��100��6O   ?=You can select operating system which installed in classroom.5C   '   .@�5E   "   �� .��6/   	Windows68   �� 5F   "   .��6/   Linux66   �� 5G   "   .��6/   Cross platform6?   �� 5H   "   .��6/   Any64   �� 5I   "   .��6/   Both65   5J      5K        5L       . $	 Number of seats:��6<   $	 Top=�6U   .
 5M      �� -	   5        $��ns_edit��5M   I   $ numSeats��6'   $	��200��6X   5N        5O       . $	 	Software:��6<   $	 Top=�6N   .
 5P      )  #DevExpress.Xpf.Editors.ComboBoxEdit  -
   5        $��	soft_edit��5P   /   +  %DevExpress.Xpf.Editors.LookUpEditBase   ItemsSource 6@   �� *	cbItems+ 5Q   "   �� 5R   &   $��5q�60   $
��Wrap=�6;   $	��100��6O   75You can select software which installed in classroom.5T   '   %  DevExpress.Xpf.Editors.BaseEdit   StyleSettings 5V   "   9	  3DevExpress.Xpf.Editors.CheckedComboBoxStyleSettings	  5W   &   5Y      5Z      5\      5^      �� 5_      -   5        $
��Edit��5_      $
��Bold"�6(   $��Center=�6P   $��Bottom=�6m   $��60��6�   $��30��6�   5a      5b      5c      5d      