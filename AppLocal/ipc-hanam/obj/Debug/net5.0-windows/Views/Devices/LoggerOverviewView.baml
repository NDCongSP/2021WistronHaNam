   M S B A M L   `   `   `  ����D  @ipc-hanam, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ipc_hanam	ipc_hanam  5      "    ipc_hanam.LoggerOverviewView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Y UAFS.HMILibrary.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=013123dbcc830ca1%hmihttp://schemas.afs.com/2019  5      #localclr-namespace:ipc_hanam  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      .��5      *  $AFS.HMILibrary.Wpf.ThemeShadowChrome  $��Stretch=�5   	   $��Stretch=�5   	       Depth$  32Q�5   	      IsShadowEnabled$
 True��5   	   ._�5   
   !  AFS.HMILibrary.Wpf.HMIPanel  -   5          Name$ 
_wrapPanel��5       �� Padding$ 24,12,12,12q�5       '  #SystemControlBackgroundAltHighBrush#���   5      ._�5      �  � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $��* �6$   .+�5      �� $��24(�5      $
��Bold"�5      ��5      �� *Logger.DisplayName+ �� FallbackValue$  ��5      �� 5      $��1��5       $��24q�5!      $��Stretch=�5"       �� Orientation$ Vertical=�5#      .+�5$      � $��0,16,0,0q�6    � ColumnDefinitions 5%      �� 5&      $
��Auto �6/   �� 5'      $
��Auto �6/   .+�5)      )  #AFS.HMILibrary.Wpf.SimpleStackPanel     Orientation$ Vertical=�6/   	   Spacing$	 12Q�6F   .+�5*      �� $��Model:��5+   !   $��16(�5,   !   
 J� Opacity$	
 0.8Q�5-   !   �� 5.      $��
Device Id:��5/   !   $��16(�50   !   $	
 0.8Q�51   !   �� 52      $��Status:��53   !   $��16(�54   !   $	
 0.8Q�55   !   �� 56      $��Inverters Runnings:��57   !   $��16(�58   !   $	
 0.8Q�59   !   �� 5;      $��Input Power:��5<   !   $��16(�5=   !   $	
 0.8Q�5>   !   �� 5?      $��Active Power:��5@   !   $��16(�5A   !   $	
 0.8Q�5B   !   �� 5C      $��Total Energy Yields��5D   !   $��16(�5E   !   $	
 0.8Q�5F   !   �� 5G      $��Energy Yields To Day:��5H   !   $��16(�5I   !   $	
 0.8Q�5J   !   5K        5M      $��1��5N      $��12,0,0,0q�5O      $ Vertical=�5P      $	 12Q�5Q      .+�5R      �� $��16(�5T   !   $
 1Q�5U   !   ��5S   !   �� *Logger.Model+$  ��5U   !   �� 5V      $��16(�5X   !   $
 1Q�5Y   !   ��5W   !   �� *Logger.DeviceId+$  ��5Y   !   �� 5Z      $��16(�5\   !   $
 1Q�5]   !   ��5[   !   �� *Logger.Status+$  ��5]   !   �� 5^      $ 
Horizontal=�6)   .+�5_   "   �� $��16(�5a   %   $
 1Q�5b   %   ��5`   %   �� *Logger.InverterRunnings+$  ��5b   %   �� 5c   "   $	�� / ��6(   $��16(�66   �� 5d   "   $��16(�5f   %   $
 1Q�5g   %   ��5e   %   �� *Logger.Inverters.Count+$  ��5g   %   5h      �� 5i      $ 
Horizontal=�6)   .+�5j   "   !  AFS.HMILibrary.Wpf.HMILabel  $��0q�5k   %   $�� ��5l   %     UnitConverter   	Converter# [ 5m   %      ConverterParameter$ kW��5n   %   $��16(�5o   %      TagPath$ /input_power��5p   %   5q      �� 5r      $ 
Horizontal=�6)   .+�5s   "     -   5          Name$ _lbActivePower��5t   %   $��0q�5u   %   $�� ��5v   %   # [ 5w   %   $ kW��5x   %   $��16(�5y   %   $ /active_power��5z   %   5{      �� 5|      $ 
Horizontal=�6)   .+�5}   "     $��0q�5~   %   $�� ��5   %   # [ 5�   %   $	 kWh��5�   %   $��16(�5�   %   $ /total_energy_yields��5�   %   5�      �� 5�      $ 
Horizontal=�6)   .+�5�   "     $��0q�5�   %   $�� ��5�   %   # [ 5�   %   $	 kWh��5�   %   $��16(�5�   %   $ /daily_energy_yields��5�   %   5�      5�      5�      5�      5�      5�      5�      5�      