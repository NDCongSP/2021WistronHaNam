   M S B A M L   `   `   `  ����D  @ipc-hanam, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ipc_hanam	ipc_hanam  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5      &     ipc_hanam.InverterPowerTrendView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Y UAFS.HMILibrary.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=013123dbcc830ca1%hmihttp://schemas.afs.com/2019  5      #localclr-namespace:ipc_hanam  5      >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	      .��5      *  $AFS.HMILibrary.Wpf.ThemeShadowChrome  $��Stretch=�5   	   $��Stretch=�5   	       Depth$  32Q�5   	      IsShadowEnabled$
 True��5   	   ._�5   
   ��  �� Padding$ 24,12,12,12q�6    '  #SystemControlBackgroundAltHighBrush#���   6'   ._�5      �  � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $��* �6$   .+�5      �� -   5         ��Name$ _lbTitle��5      $��Power Trend��5      $��24(�5      $
��Bold"�5      � 5      $��1��6   .+�5      �� -   5         ��Name$ 	container��5      $ 
12,12,12,0q�60   ._�5       �� $ 6,0,6,0q�6!   
������ ._�5!      �  5"   "   �� 5#   &   $��20 �64   �� 5$   &   $��* �64   .+�5&   "   �� $��0��5'   %   $��Center=�5(   %   $��Center=�5)   %    �� Orientation$ 
Horizontal=�5*   %   .+�5+   &   �� $��20��5,   )   $��20��5-   )   
���  ��5.   )   �� 5/   &   -   5        $ lbPowerTrend��50   )   $��8,0,0,0q�51   )   $��0q�52   )   $��Center=�53   )   $��Power (kWh)��54   )   $��16(�55   )   $��SemiBold"�56   )   
���  ��57   )   �� 58   &   $��20��59   )   $��20��5:   )   $��30,0,0,0q�5;   )   $��Center=�5<   )   
��� � �5=   )   �� 5>   &   -   5        $ lbRadiantTrend��5?   )   $��8,0,0,0q�5@   )   $��0q�5A   )   $��Center=�5B   )   $��Center=�5C   )   $��Radiant (W/m2)��5D   )   $��16(�5E   )   $��SemiBold"�5F   )   
��� � �5G   )   5H   #   #  LiveCharts.Wpf.CartesianChart  5I   "   -   5          Name$ trend��5J   %   $��1��5K   %   
������ &   LiveCharts.Wpf.Charts.Base.Chart   DisableAnimations$
 True��5M   %   	   	Hoverable$
	 True��5N   %   
   
ScrollMode$

 None=�5O   %      Zoom$ X=�5P   %      AxisX 5R   *     LiveCharts.Wpf.Axis  -   5          Name$ xTime��5S   -      Title$
 Time��5T   -      FontSize$ 14Q�5U   -      
Foreground
 �   �   LabelsRotation$	 -90Q�5W   -      	Separator 5X   .     LiveCharts.Wpf.Separator  5Y   2      Stroke
 �����6@      Sections 5\   2      LiveCharts.Wpf.AxisSection  -   5          Name$ sectionTime��5]   5      	DataLabel$
 True��5^   5      DataLabelForeground
 �����   DisableAnimations$
 True��5`   5      Fill
 ����    SectionOffset$
 -0.5Q�5b   5      SectionWidth$ 1Q�5c   5      Stroke �#ff5722   StrokeThickness$ 1Q�5e   5   $y�Visible=�5f   5   5h   +      AxisY 5k   *     -   5        $ yPower��5l   -   $ 
Power (kW)��5m   -      DisableAnimations$
 True��5n   -   $ 18Q�5o   -   
 �  ��    
ShowLabels$
  True��5q   -    5r   .     5s   2   
 �����6@    5v   2     -	   5        $ 	sectionY1��5w   5   ! K� ZIndex$! 99��5x   5   $
 True��5y   5   
 �����$
 True��5{   5    �#ff5722$ 1Q�5}   5   $y�Visible=�5~   5   5�   +     5�   *   -
   5        $ yRadiant��5�   -   $ Radiation (W/m2)��5�   -   $ 18Q�5�   -   
 � � �"   MinValue$" 0Q�5�   -   #   Position$# RightTop=�5�   -   $
  True��5�   -    5�   .     5�   2   
 ���� $y�Hidden=�6U    5�   2     -   5        $ 	sectionY2��5�   5   $��Right=�5�   5   $! 1��5�   5   $
 True��5�   5   
 �����$
 True��5�   5    �#ff5722$ 1Q�5�   5   $y�Visible=�5�   5   5�   +   5�   #   5�      5�      5�      5�      5�      5�      5�      5�      