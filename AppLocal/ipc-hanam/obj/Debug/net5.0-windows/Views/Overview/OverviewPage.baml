   M S B A M L   `   `   `  ����D  @ipc-hanam, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ipc_hanam	ipc_hanam  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5          ipc_hanam.OverviewPage   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Y UAFS.HMILibrary.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=013123dbcc830ca1%hmihttp://schemas.afs.com/2019  5      #localclr-namespace:ipc_hanam  5      >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	      .��5      ��   �� Padding$  
24,0,24,24q�6    &  "SystemControlBackgroundMediumBrush#���   6"   ._�5   
   ��   TabControlPivotStyle#���  6   .@�5      �� $��General��6   .��5      �  � ColumnDefinitions 5      �� 5      $	��586 �6+   �� 5      $	��586 �6+   �� 5      $	��640 �6+    � RowDefinitions 5      �� 5      $
��Auto �6(   �� 5      $	��620 �6(   .+�5          ipc_hanam.EnergyYieldsView  $��12q�6-   &    ipc_hanam.EnviromentBenefitsView  5      $��1��63   $��12q�6C   $   ipc_hanam.EnviromentSensorView  5      $��2��61   $��12q�6A      ipc_hanam.EnergyFlowView  5      $��1��5       $��2��5!      $��12,12,12,24q�5"      $   ipc_hanam.BasicInformationView  5#      $��1��5$      $��2��5%      $��12,12,12,24q�5&      5'      5(      �� 5)      $��Energy Yields��6   .��5*      �� #���  6   .@�5+      �� $��Hours��6   .��5,      #  LiveCharts.Wpf.CartesianChart  -   5          Name$ dayChart��5-      $��1��5.      
������ &   LiveCharts.Wpf.Charts.Base.Chart   DisableAnimations$
 True��50         LegendLocation$	 Top=�51         
ScrollMode$
 None=�52         Zoom$
 None=�53         AxisX 55   "     LiveCharts.Wpf.Axis  -   5        	  Name$	 xTimeDay��56   %   
   Title$

 Hour��57   %      FontSize$ 18Q�58   %      
Foreground
 �   �   LabelsRotation$ 0Q�5:   %      MaxValue$ 24Q�5;   %      MinValue$ 0Q�5<   %      	Separator 5=   &   	  LiveCharts.Wpf.Separator	  5>   *    	  Step$ 1Q�68    	  Stroke
 �����6A      Sections 5A   *    
  LiveCharts.Wpf.AxisSection
  -   5         
 Name$ xDayAxisSection��5B   -    
  	DataLabel$
 True��5C   -    
  DataLabelForeground
 ����� 
  Fill
 ����  
  SectionOffset$
 -0.5Q�5F   -    
  SectionWidth$ 1Q�5G   -    
  Stroke �#ff5722 
  StrokeThickness$ 0Q�5I   -   5K   #      AxisY 5N   "     $
 Energy (kWh)��5O   %   $ 16Q�5P   %   
 � � �$ 0Q�5R   %      Position$ 
LeftBottom=�5S   %      
ShowLabels$
 True��5T   %    5U   &   	  5V   *   
 �����68   5X   #      Series 5[   "   !  LiveCharts.Wpf.ColumnSeries  -   5           Name$  colSeriesDay��5\   %     LiveCharts.Wpf.Series!   Title$'! !Energy Production Each Hour (kWh)��5]   %   "   
DataLabels$" False��5^   %   #   FontSize$# 16Q�5_   %   $   Fill$ 5`   &   �� 5a   *   % �� 
StartPoint$	% 1,0&�6>   & �� EndPoint$	& 0,0&�6O   .��5b   .   � ' � Color$' Green��6;   � 5c   .   ( � Offset$( 1Q�6;   $' Green��6F   � 5d   .   $	( 0.5Q�6;   $' Green��6H   5e   +   5g   #   5i      5j      �� 5k      $
��Days��6   .��5l        -   5        $ 
monthChart��5m      $��1��5n      
������ $
 True��5p      $	 Top=�5q      $
 None=�5r      $
 None=�5s       5u   "     -   5        $	 
xTimeMonth��5v   %   $	
 Day��5w   %   $ 18Q�5x   %   
 �   �$ 0Q�5z   %   $ 1Q�5{   %    5|   &   	  5}   *   $ 1Q�68   
 �����6A    5�   *   
  -   5        $ xMonthAxisSection��5�   -   $
 True��5�   -   
 �����
 ���� $
 -0.5Q�5�   -   $ 1Q�5�   -    �#ff5722$ 0Q�5�   -   5�   #    5�   "     $
 Energy (kWh)��5�   %   $ 16Q�5�   %   
 � � �$ 0Q�5�   %   $ 
LeftBottom=�5�   %   $
 True��5�   %    5�   &   	  5�   *   
 �����68   5�   #    5�   "     -   5        $  colSeriesMonth��5�   %   $&!  Energy Production Each Day (kWh)��5�   %   $" False��5�   %   $# 16Q�5�   %   $ 5�   &   �� 5�   *   $	% 1,0&�6>   $	& 0,0&�6O   .��5�   .   � $' Green��6;   � 5�   .   $( 1Q�6;   $' Green��6F   � 5�   .   $	( 0.5Q�6;   $' Green��6H   5�   +   5�   #   5�      5�      �� 5�      $��Months��6   .��5�        -	   5        $ 	yearChart��5�      $��1��5�      
������ $
 True��5�      
���   �$	 Top=�5�      $
 None=�5�      $
 None=�5�       5�   "     -
   5        $	 	xTimeYear��5�   %   $
 Month��5�   %   $ 18Q�5�   %   
 �   �$ 0Q�5�   %   $ 1Q�5�   %    5�   &   	  5�   *   $ 1Q�68   
 �����6A    5�   *   
  -   5        $ xYearAxisSection��5�   -   $
 True��5�   -   
 �����
 ���� $
 -0.5Q�5�   -   $ 1Q�5�   -    �#ff5722$ 0Q�5�   -   5�   #    5�   "     $
 Energy (kWh)��5�   %   $ 16Q�5�   %   
 � � �$ 0Q�5�   %   $ 
LeftBottom=�5�   %   $
 True��5�   %    5�   &   	  5�   *   
 �����68   5�   #    5�   "     -   5        $  colSeriesYear��5�   %   $(! "Energy Production Each Month (kWh)��5�   %   $" False��5�   %   $# 16Q�5�   %   $ 5�   &   �� 5�   *   $	% 1,0&�6>   $	& 0,0&�6O   .��5�   .   � $' Green��6;   � 5�   .   $( 1Q�6;   $' Green��6F   � 5�   .   $	( 0.5Q�6;   $' Green��6H   5�   +   5�   #   5�      5�      5�      5�      5�      5�      5�      