   M S B A M L   `   `   `  ����D  @ipc-hanam, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ipc_hanam	ipc_hanam  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5          ipc_hanam.TrendsPage   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Y UAFS.HMILibrary.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=013123dbcc830ca1%hmihttp://schemas.afs.com/2019  5      #localclr-namespace:ipc_hanam  5      >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	      .��5      ��   �� Padding$  8,0,0,0q�6    &  "SystemControlBackgroundMediumBrush#���   6   ._�5   
   ��   TabControlPivotStyle#���  6   .@�5      �� $��Energy Yields��6   .��5      �� #���  6   .@�5      �� $
��Days��6   .��5      �� $��16q�5      
�������$��1q�5      ._�5      �  � RowDefinitions 5   "   �� 5   &   $
��Auto �64   �� 5   &   $��* �64   .+�5   "   � $��0��5   %   $��70��5   %   $��Stretch=�5   %   ��5    %   ��  �� ElementName$ 
dayToolBar�� �� Path$ 
Background�.+�5!   &   #  AFS.HMILibrary.Wpf.CommandBar  -   5          Name$ 
dayToolBar��5"   )   $��70��5#   )   $
��Left=�5$   )   $��Center=�5%   )   $��Center=�5&   )      DefaultLabelPosition$ Bottom=�5'   )   $��LeftToRight=�5(   )      Content 5)   *   �� 5*   .   $��	16,0,16,0q�68   $��Center=�6K      PrimaryCommands. 5-   *   /  )AFS.HMILibrary.Wpf.AppBarElementContainer  $��Center=�6E   .��5.   .   �� $��8,0,0,0q�5/   1   $��Center=�50   1    �� Orientation$ Vertical=�51   1   .+�52   2   �� -   5        	 ��Name$	 _cobDayEnergyDataSource��53   5   $	��300��54   5   $	��600��55   5   $��8,0,0,0q�56   5   &   AFS.HMILibrary.Wpf.ControlHelper
   CornerRadius$
 0��57   5    �� DisplayMemberPath$ DisplayName��58   5   �� 59   2   $��0,2,0,0q�5:   5   $��Center=�5;   5   $��Center=�5<   5   $��Data Source��5=   5   5>   /   5?   +     5B   *   $��Center=�6E   .��5C   .   �� $��8,0,0,0q�5D   1   $��Center=�5E   1   $ Vertical=�5F   1   .+�5G   2   (  "System.Windows.Controls.DatePicker  -   5          Name$ _dtpDayEnergy��5H   5   $	��130��5I   5   $	��300��5J   5   $��8,0,0,0q�5K   5   $
 0��5L   5   "   ipc_hanam.DatePickerCalendar   IsMonthYear$
 True��5M   5   $   ipc_hanam.DatePickerDateFormat   
DateFormat$ MM/yyyy��5N   5   �� 5O   2   $��0,2,0,0q�5P   5   $��Center=�5Q   5   $��Center=�5R   5   $��Select Month��5S   5   5T   /   5U   +   %  AFS.HMILibrary.Wpf.AppBarButton  5X   *   -   5          Name$ _btnRefreshDayEnergy��5Y   -   $��8,0,0,0q�5Z   -      Icon-  'AFS.HMILibrary.Wpf.IconElementConverter$ Refresh 5[   -      	IsCompact$ False��5\   -      Label$ Refresh��5]   -      LabelPosition$ Default=�5^   -   (	  "AFS.HMILibrary.Wpf.AppBarSeparator	  5_   *     5a   *   -   5        $ _btnPrintDayEnergy��5b   -   $ Print 5c   -   $ False��5d   -   $ Export��5e   -   $ Default=�5f   -   5h   '   5i   #   �� 5k   "   $��1��6-   .@�5l   &   �� $��Trend��6.   .��5m   *   #
  LiveCharts.Wpf.CartesianChart
  -   5         
 Name$ _chartDayEnergy��5n   -   $��1��5o   -   
������ &   LiveCharts.Wpf.Charts.Base.Chart   DisableAnimations$
 True��5q   -      LegendLocation$	 Top=�5r   -      
ScrollMode$
 None=�5s   -      Zoom$
 None=�5t   -      AxisX 5v   2     LiveCharts.Wpf.Axis  -   5          Name$ xTimeDay��5w   5      Title$	 Day��5x   5      FontSize$ 18Q�5y   5      
Foreground
 �   �   LabelsRotation$ 0Q�5{   5      MinValue$ 1Q�5|   5       	Separator  5}   6     LiveCharts.Wpf.Separator  5~   :   !   Step$! 1Q�6H   "   Stroke
" �����6Q   #   Sections# 5�   :      LiveCharts.Wpf.AxisSection  -   5        $  Name$$ xDayAxisSection��5�   =   %   	DataLabel$
% True��5�   =   &   DataLabelForeground
& �����'   Fill
' ���� (   SectionOffset$
( -0.5Q�5�   =   )   SectionWidth$) 1Q�5�   =   *   Stroke* �#ff5722+   StrokeThickness$+ 0Q�5�   =   5�   3   ,   AxisY, 5�   2     $ Energy (kWh)��5�   5   $ 16Q�5�   5   
 � � �$ 0Q�5�   5   -   Position$- 
LeftBottom=�5�   5   .   
ShowLabels$
. True��5�   5     5�   6     5�   :   
" �����6H   5�   3   /   Series/ 5�   2   !  LiveCharts.Wpf.ColumnSeries  -	   5        0  Name$0 colSeriesDay��5�   5     LiveCharts.Wpf.Series1   Title$"1 Energy Yields Each Day (kWh)��5�   5   2   
DataLabels$2 False��5�   5   3   FontSize$3 16Q�5�   5   4   Fill4 5�   6   �� 5�   :   5 �� 
StartPoint$	5 1,0&�6N   6 �� EndPoint$	6 0,0&�6_   .��5�   >   � 7 � Color$7 Green��6K   � 5�   >   8 � Offset$8 1Q�6K   $7 Green��6V   � 5�   >   $	8 0.5Q�6K   $7 Green��6X   5�   ;   5�   3   5�   +   5�   '   �� 5�   &   $
��Data��6.   .��5�   *   &   System.Windows.Controls.DataGrid  -
   5        9  Name$9 _dgDayEnergy��5�   -   :   AutoGenerateColumns$: False��5�   -   
������ ;   CanUserAddRows$; False��5�   -   <   CanUserDeleteRows$< False��5�   -   =   GridLinesVisibility$	= All=�5�   -   >   
IsReadOnly$
> True��5�   -   ?   MinRowHeight$? 42Q�5�   -   @   RowHeaderWidth$@ 30Q�5�   -   A   	RowHeight$A 42Q�5�   -   B   ColumnsB 5�   .   0  *System.Windows.Controls.DataGridTextColumn  5�   2   ,  &System.Windows.Controls.DataGridColumnC   Header$C DateTime��6{   1  +System.Windows.Controls.DataGridBoundColumnD   BindingD 6E   �� *
DateTime+E �� StringFormat$E 
dd/MM/yyyy��6{     5�   2   $C Energy��6u   D 6E   �� *Energy+$
E 0.##��6u   5�   +   5�   '   5�   #   5�      5�      5�      �� 5�      $��Months��6   .��5�      �� $��16q�5�      
�������$��1q�5�      ._�5�      �  5�   "   �� 5�   &   $
��Auto �64   �� 5�   &   $��* �64   .+�5�   "   � $��0��5�   %   $��70��5�   %   $��Stretch=�5�   %   ��5�   %   �� $ 
dayToolBar��$ 
Background�.+�5�   &     -   5        $ monthToolBar��5�   )   $
��Left=�5�   )   $��Center=�5�   )   $��Center=�5�   )   $ Bottom=�5�   )   $��LeftToRight=�5�   )    5�   *   �� 5�   .   $��	16,0,16,0q�68   $��Center=�6K   . 5�   *     $��Center=�6E   .��5�   .   �� $��8,0,0,0q�5�   1   $��Center=�5�   1   $ Vertical=�5�   1   .+�5�   2   �� -   5        $	 _cobMonthEnergyDataSource��5�   5   $	��300��5�   5   $	��600��5�   5   $��8,0,0,0q�5�   5   $
 0��5�   5   $ DisplayName��5�   5   �� 5�   2   $��0,2,0,0q�5�   5   $��Center=�5�   5   $��Center=�5�   5   $��Data Source��5�   5   5�   /   5�   +     5�   *   $��Center=�6E   .��5�   .   �� $��8,0,0,0q�5�   1   $��Center=�5�   1   $ Vertical=�5�   1   .+�5�   2     -   5        $ _dtpMonthEnergy��5�   5   $	��130��5�   5   $	��300��5�   5   $��8,0,0,0q�5�   5   $
 0��5�   5   $
 yyyy��5�   5   *   $ipc_hanam.DateTimePickerYearCalenderF   IsYear$
F True��5�   5   �� 5�   2   $��0,2,0,0q�5   5   $��Center=�5  5   $��Center=�5  5   $��Select Year��5  5   5  /   5  +     5  *   -   5        $ _btnRefreshMonthEnergy��5	  -   $��8,0,0,0q�5
  -   $ Refresh 5  -   $ False��5  -   $ Refresh��5  -   $ Default=�5  -   	  5  *     5  *   -   5        $ _btnPrintMonthEnergy��5  -   $ Print 5  -   $ False��5  -   $ Export��5  -   $ Default=�5  -   5  '   5  #   �� 5  "   $��1��6-   .@�5  &   �� $��Trend��6.   .��5  *   
  -   5        $ _chartMonthEnergy��5  -   $��1��5  -   
������ $
 True��5!  -   $	 Top=�5"  -   $
 None=�5#  -   $
 None=�5$  -    5&  2     -   5        $ 
xTimeMonth��5'  5   $	 Day��5(  5   $ 18Q�5)  5   
 �   �$ 0Q�5+  5   $ 1Q�5,  5     5-  6     5.  :   $! 1Q�6H   
" �����6Q   # 51  :     -   5        $$ xMonthAxisSection��52  =   $
% True��53  =   
& �����
' ���� $
( -0.5Q�56  =   $) 1Q�57  =   * �#ff5722$+ 0Q�59  =   5;  3   , 5>  2     $ Energy (kWh)��5?  5   $ 16Q�5@  5   
 � � �$ 0Q�5B  5   $- 
LeftBottom=�5C  5   $
. True��5D  5     5E  6     5F  :   
" �����6H   5H  3   / 5K  2     -   5        $0 colSeriesMonth��5L  5   $$1 Energy Yields Each Month (kWh)��5M  5   $2 False��5N  5   $3 16Q�5O  5   4 5P  6   �� 5Q  :   $	5 1,0&�6N   $	6 0,0&�6_   .��5R  >   � $7 Green��6K   � 5S  >   $8 1Q�6K   $7 Green��6V   � 5T  >   $	8 0.5Q�6K   $7 Green��6X   5U  ;   5W  3   5Y  +   5Z  '   �� 5[  &   $
��Data��6.   .��5\  *     -   5        $9 _dgMonthEnergy��5]  -   $: False��5^  -   
������ $; False��5`  -   $< False��5a  -   $	= All=�5b  -   $
> True��5c  -   $? 42Q�5d  -   $@ 30Q�5e  -   $A 42Q�5f  -   B 5g  .     5h  2   $C DateTime��6x   D 6E   �� *
DateTime+$E MM/yyyy��6x     5i  2   $C Energy��6u   D 6E   �� *Energy+$
E 0.##��6u   5k  +   5l  '   5m  #   5n     5o     5q     5r     5s     �� 5t     $��Sensor And Power��6   .��5u     �� $��16q�5v     
�������$��1q�5x     ._�5y     �  5z     �� 5{     $
��Auto �6,   �� 5|     $��* �6,   .+�5     � $��0��5�     $��70��5�     $��Stretch=�5�     ��5�     �� $ 
dayToolBar��$ 
Background�.+�5�       $
��Left=�5�  !   $��Center=�5�  !   $��Center=�5�  !   $ Bottom=�5�  !   $��LeftToRight=�5�  !    5�  "   �� 5�  &   $��	16,0,16,0q�60   $��Center=�6C   . 5�  "     $��Center=�6=   .��5�  &   �� $��8,0,0,0q�5�  )   $��Center=�5�  )   $ Vertical=�5�  )   .+�5�  *     -   5        $ _dtpFromSensor��5�  -   $	��130��5�  -   $	��300��5�  -   $��8,0,0,0q�5�  -   $
 0��5�  -   �� 5�  *   $��0,2,0,0q�5�  -   $��Center=�5�  -   $��Center=�5�  -   $��	From Time��5�  -   5�  '   5�  #     5�  "   $��Center=�6=   .��5�  &   �� $��8,0,0,0q�5�  )   $��Center=�5�  )   $ Vertical=�5�  )   .+�5�  *     -   5        $ _dtpToSensor��5�  -   $	��130��5�  -   $	��300��5�  -   $��8,0,0,0q�5�  -   $
 0��5�  -   �� 5�  *   $��0,2,0,0q�5�  -   $��Center=�5�  -   $��Center=�5�  -   $��To Time��5�  -   5�  '   5�  #     5�  "   -   5        $ _btnRefreshSensor��5�  %   $��8,0,0,0q�5�  %   $ Refresh 5�  %   $ False��5�  %   $ Refresh��5�  %   $ Default=�5�  %   	  5�  "     5�  "   -   5        $ _btnPrintSensor��5�  %   $ Print 5�  %   $ False��5�  %   $ Export��5�  %   $ Default=�5�  %   5�     5�     �� 5�     $��1��6%   .@�5�     �� $��Trend��6&   .��5�  "   
  -   5        $ trend��5�  %   $��1��5�  %   
������ $
 True��5�  %   G   	Hoverable$
G True��5�  %   $	 Top=�5�  %   $
 None=�5�  %   $ X=�5�  %    5�  *     -   5        $ xTime��5�  -   $
 Time��5�  -   $ 14Q�5�  -   
 �   �$	 -90Q�5�  -     5�  .     5�  2   
" �����6@   # 5�  2     -   5        $$ sectionTime��5�  5   $
% True��5�  5   
& �����H   DisableAnimations$
H True��5�  5   
' ���� $
( -0.5Q�5�  5   $) 1Q�5�  5   * �#ff5722$+ 1Q�5�  5   $y�Visible=�5�  5   5�  +   , 5  *     -   5        $ yPower��5  -   $ 
Power (kW)��5  -   I   DisableAnimations$
I True��5  -   $ 18Q�5  -   
 �  ��$
. True��5  -     5	  .     5
  2   
" �����6@   # 5  2     -   5        $$ 	sectionY1��5  5   J K� ZIndex$J 99��5  5   $
% True��5  5   
& �����$
H True��5  5   * �#ff5722$+ 1Q�5  5   $y�Visible=�5  5   5  +     5  *   -   5        $ yRadiant��5  -   $ Radiation (W/m2)��5  -   $ 18Q�5  -   
 � � �$ 0Q�5  -   $- RightTop=�5  -   $
. True��5  -     5   .     5!  2   
" ���� $y�Hidden=�6U   # 5$  2     -   5        $$ 	sectionY2��5%  5   $��Right=�5&  5   $J 1��5'  5   $
% True��5(  5   
& �����$
H True��5*  5   * �#ff5722$+ 1Q�5,  5   $y�Visible=�5-  5   5/  +   51  #   52     �� 53     $
��Data��6&   .��54  "     -    5        $9 	_dgSensor��55  %   $
: True��56  %   
������ $; False��58  %   $< False��59  %   $	= All=�5:  %   $
> True��5;  %   $? 42Q�5<  %   $@ 30Q�5=  %   $A 42Q�5>  %   5?     5@     5A     5B     5C     5D     5E     5F     