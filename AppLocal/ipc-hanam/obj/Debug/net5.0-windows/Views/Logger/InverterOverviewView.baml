   M S B A M L   `   `   `  ����D  @ipc-hanam, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ipc_hanam	ipc_hanam  5      $    ipc_hanam.InverterOverviewView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Y UAFS.HMILibrary.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=013123dbcc830ca1%hmihttp://schemas.afs.com/2019  5      #localclr-namespace:ipc_hanam  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      .��5      *  $AFS.HMILibrary.Wpf.ThemeShadowChrome  $��Stretch=�5   	   $��Stretch=�5   	       Depth$  32Q�5   	      IsShadowEnabled$
 True��5   	   ._�5   
   !  AFS.HMILibrary.Wpf.HMIPanel  -   5          Name$ _panel��5       �� Padding$ 24,12,12,12q�5       '  #SystemControlBackgroundAltHighBrush#���   5      ._�5      �  � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $��* �6$   .+�5      �� $��24(�5      $
��Bold"�5      ��5      �� *Inverter.DisplayName+ �� FallbackValue$  ��5      �� 5      $��1��5       $��24q�5!      $��Stretch=�5"       �� Orientation$ Vertical=�5#      .+�5$      � $��0,16,0,0q�6    � ColumnDefinitions 5%      �� 5&      $
��Auto �6/   �� 5'      $
��Auto �6/   .+�5)      )  #AFS.HMILibrary.Wpf.SimpleStackPanel     Orientation$ Vertical=�6/   	   Spacing$	 12Q�6F   .+�5*      �� $��Model:��5+   !   $��16(�5,   !   
 J� Opacity$	
 0.8Q�5-   !   �� 5.      $��
Device Id:��5/   !   $��16(�50   !   $	
 0.8Q�51   !   �� 52      $��Status:��53   !   $��16(�54   !   $	
 0.8Q�55   !   �� 56      $��Temperature:��57   !   $��16(�58   !   $	
 0.8Q�59   !   �� 5:      $��
Frequency:��5;   !   $��16(�5<   !   $	
 0.8Q�5=   !   �� 5>      $��Power Factor:��5?   !   $��16(�5@   !   $	
 0.8Q�5A   !   �� 5B      $��Active Power:��5C   !   $��16(�5D   !   $	
 0.8Q�5E   !   �� 5F      $��Total Energy Yields��5G   !   $��16(�5H   !   $	
 0.8Q�5I   !   �� 5J      $��Energy Yields To Day:��5K   !   $��16(�5L   !   $	
 0.8Q�5M   !   5N        5P      $��1��5Q      $��12,0,0,0q�5R      $ Vertical=�5S      $	 12Q�5T      .+�5U      �� $��16(�5W   !   $
 1Q�5X   !   ��5V   !   �� *Inverter.Model+$  ��5X   !   �� 5Y      $��16(�5[   !   $
 1Q�5\   !   ��5Z   !   �� *Inverter.DeviceId+$  ��5\   !   �� 5]      $��16(�5_   !   $
 1Q�5`   !   ��5^   !   �� *Inverter.Status+$  ��5`   !   �� 5a      $ 
Horizontal=�6)   .+�5b   "   !  AFS.HMILibrary.Wpf.HMILabel  $��0q�5c   %   $�� ��5d   %     UnitConverter   	Converter# [ 5e   %      ConverterParameter$	 °C��5f   %   $��16(�5g   %      TagPath$ /temperature��5h   %   5i      �� 5k      $ 
Horizontal=�6)   .+�5l   "     $��0q�5m   %   $�� ��5n   %   # [ 5o   %   $ Hz��5p   %   $��16(�5q   %   $ /grid_frequency��5r   %   5s      �� 5t      $ 
Horizontal=�6)   .+�5u   "     $��0q�5v   %   $�� ��5w   %   $��16(�5x   %   $ /power_factor��5y   %   5z      �� 5{      $ 
Horizontal=�6)   .+�5|   "     $��0q�5}   %   $�� ��5~   %   # [ 5   %   $ kW��5�   %   $��16(�5�   %   $ /active_power��5�   %   5�      �� 5�      $ 
Horizontal=�6)   .+�5�   "     $��0q�5�   %   $�� ��5�   %   # [ 5�   %   $	 kWh��5�   %   $��16(�5�   %   $ /total_energy_yields��5�   %   5�      �� 5�      $ 
Horizontal=�6)   .+�5�   "     $��0q�5�   %   $�� ��5�   %   # [ 5�   %   $	 kWh��5�   %   $��16(�5�   %   $ /daily_energy_yields��5�   %   5�      5�      5�      5�      5�      5�      5�      5�      