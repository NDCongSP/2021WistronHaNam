   M S B A M L   `   `   `  ����D  @ipc-hanam, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ipc_hanam	ipc_hanam  5          ipc_hanam.LoggerPage   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Y UAFS.HMILibrary.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=013123dbcc830ca1%hmihttp://schemas.afs.com/2019  5      #localclr-namespace:ipc_hanam  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      .��5      ��   �� Padding$  8,0,0,0q�6    &  "SystemControlBackgroundMediumBrush#���   6   ._�5   
   ��   TabControlPivotStyle#���  6   .@�5      �� $��	Inverters��6   .��5      '  !AFS.HMILibrary.Wpf.ScrollViewerEx  .��5      � -   5         �Name$ 
_wrapPanel��5        � Orientation$ 
Horizontal=�64   5      5      �� 5      $��Strings��6   .��5      (�  (� Stretch$
 Fill=�6   .��5      #   ipc_hanam.InverterStringsView  -   5          Name$ _invStringView��5      $	��800��5      $	��450��5      5      5      �� 5      $��Power Meters��6   .��5        .��5      � -   5        $ _pmWrapPanel��5       $ 
Horizontal=�66   5      5      �� 5       $��Electric Diagram��6   .��5!      (� .��5"      � $	��768��6   $	��402��6'    � RowDefinitions 5#      �� 5$      $��* �6,   �� 5%      $��* �6,   .+�5'      � $��0��5(      $	��768��5)      $	��201��5*      .+�5+      �� $	��Top=�6$   $"��/Resources/Images/LAYOUT.png��6<   $��382.8��6b   �� 5-      -   5         ��Name$ 	_acbLabel��5.   !   $��
435,94,0,0q�5/   !   $
��Left=�50   !   $	��Top=�51   !   $��Closed��52   !   $��6(�53   !   
���  ��54   !   �� 56      -   5         ��Name$ _kn612��57   !   $��12��58   !   $��8��59   !   $��458,145,0,0q�5:   !   $
��Left=�5;   !   $	��Top=�5<   !   
��� � � � Cursor$
 Hand��5>   !   	 J� Opacity$		 0.3Q�5?   !   �� 5A      -   5        $	 _pm��5B   !   $��12��5C   !   $��8��5D   !   $��481,168,0,0q�5E   !   $
��Left=�5F   !   $	��Top=�5G   !   
��� � �$
 Hand��5I   !   $		 0.3Q�5J   !   5K      )  #AFS.HMILibrary.Wpf.SimpleStackPanel  5M      -   5        
  Name$
 
_mccbStack��5N      $��1��5O      $��Center=�5P         Orientation$ 
Horizontal=�5Q         Spacing$ 8Q�5R      5S      5T      5V      5W      5Y      5Z      