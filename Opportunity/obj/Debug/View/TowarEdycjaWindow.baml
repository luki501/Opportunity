   M S B A M L   `   `   `  ����F  BOpportunity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Opportunity.ViewOpportunity.View  5   	   (    "Opportunity.View.TowarEdycjaWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Opportunity.View     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cie:http://schemas.microsoft.com/expression/2010/interactivity  5   	   h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Ai9http://schemas.microsoft.com/expression/2010/interactions  5	   	     � Title$  Dodaj towar��5   	   $	��400��6   $	��400��6*    � DataContext 5
   	   ��    Locator �� Source# [   �� Path$ TowarEdycja� � 	Resources 5      ,   &Opportunity.View.MultiBindingConverter  MultiBindingConverter0   *Opportunity.View.BoolToVisibilityConverter  BoolToVisibilityConverter8   2Opportunity.View.BoolOppositeToVisibilityConverter % !BoolOppositeToVisibilityConverter  tbNormal �� 
TargetType �� Property �� Value  	txtNormal  	btnNormal  	chbNormal  dpNormal(  "System.Windows.Controls.DatePicker 	 cbNormal 
 imgButtonNormal  txtButtonNormal��%�  &	       &	      &	 &     &	 9     &	 �     &	 �     &	 8    &	 �    &		 Z    &	
 ;    &	 �      5   
   6&     5   
   6*     5   
   62   �� 5   
   " ��6!   .�5      ��  � ��$ 3q�6'   5      �� 5   
   " ��6"   .�5      ��  � ��$ Center=�62   5      �� 5   
   " ��6"   .�5      ��  � ��$ 80��6)   �� 5       � ��$ 10q�6'   5      �� 5   
   " ��6"   .�5      ��  � ��$ 12(�6)   �� 5       � ��$ Center=�62   �� 5       � ��$ 3q�6'   5      �� 5   
   "  6!   .�5       ��  � ��$ 20��6'   �� 5!       � ��$ 3q�6'   �� 5"       � ��$ 10(�6)   5#      �� 5$   
   " ��6!   .�5%      ��  � ��$ 12(�6)   �� 5&       � ��$ Center=�62   �� 5'       � ��$ 3q�6'   �� 5(       � ��$ 24��6'   �� 5)       � �� �#cebbad6+   5*      �� 5+   
   " ��6(   .�5,      ��  � ��$ 20��6&   �� 5-       � ��$ 20��6'   �� 5.       � ��$ 3q�6'   5/      �� 50   
   " ��6(   .�51      ��  � ��$ 14(�6)   �� 52       � ��$ Center=�62   �� 53       � ��$ 2q�6'   54      55      .  (System.Windows.Interactivity.Interaction   	Behaviors 56      $   Opportunity.View.ZamykanieOkna  57   
   .��59      �� -   5        	 ��Name$	 tabTowar��59      ��5:   
   �� 5;      " ��6   
 �� Triggers
 5<      �� 5=       �� Binding 6"   �� *IsTowarZapisany+ �� Value True6F   .a�5>      ��  �@��SelectedIndex$ 1��6:   5?      5A      .@�5C   
   �� $
��Dane��6   .��5D      �  � ColumnDefinitions 5E      �� 5F      �� 5G       � RowDefinitions 5I      �� 5J      $
��auto �6$   �� 5K      $
��auto �6$   �� 5L      $
��auto �6$   �� 5M      $
��auto �6$   �� 5N      $
��auto �6$   �� 5O      $
��auto �6$   �� 5P      $
��auto �6$   �� 5Q      $
��auto �6$   .+�5S      �� $��Nazwa��6   $��0��6)   #��[ 66   �� 5T      $��Marka��6   $��1��6)   #��[ 66   �� 5U      $��Model��6   $��2��6)   #��[ 66   �� 5V      $	��EAN��6   $��3��6'   #��[ 64   �� 5W      $��Uwagi��6   $��4��6)   #��[ 66   �� 5X      $��1��6   $��0��6*   #��[ 67   ��6Y   �� *NowyTowar.Nazwa+�� 5Y      $��1��6   $��1��6+   #��[	 6b    �� DisplayMemberPath$ Nazwa��6�    �� SelectedValuePath$ Id��6�   ��68   �� *ListaMarekTowarow+ �� SelectedValue 6�   �� *WybranaMarkaId+�� 5Z      $��1��6   $��2��6*   #��[ 6X   ��67   �� *NowyTowar.Model+6X   �� 5[      $��1��6   $��3��6*   #��[ 6V   ��67   �� *NowyTowar.Ean+6V   �� 5\      $��1��6   $��4��6*    �� TextWrapping$
 Wrap=�67    �� AcceptsReturn . #��[ 6�   $	��100��6�   ��6`   �� *NowyTowar.Uwagi+6�   �� 5]      $��5��6   #��[ 6&    5^      "   Opportunity.View.ZamknijOkno  5_      .��5a      ��  �� Orientation$ 
Horizontal=�6!   .+�5b      �� $4��./Opportunity;component/Model/Images/cancel.png��6    #��[
 6X   �� 5c      $��Anuluj��6$   #��[ 62   5d      5e      �� 5f      $��5��6   $��1��6&   #��[ 66   ��6Y   �� *ZapiszTowarCommand+.��5g      �� $ 
Horizontal=�6!   .+�5h      �� $2��,/Opportunity;component/Model/Images/disk.png��6    #��[
 6V   �� 5i      $��
Zatwierdź��6$   #��[ 65   5j      5k      5l      5m      �� 5n   
   $��	Przyjecie��6   .��5o      �  5p      �� 5q      �� 5r       5t      �� 5u      $
��auto �6$   �� 5v      $
��auto �6$   �� 5w      $
��auto �6$   �� 5x      $
��auto �6$   �� 5y      $
��auto �6$   �� 5z      $
��auto �6$   �� 5{      $
��auto �6$   �� 5|      $
��auto �6$   .+�5~      �� $��Ilość��6   $��0��6)   #��[ 66   �� 5      $
��Data��6   $��1��6(   #��[ 65   �� 5�      $��Numer faktury��6   $��2��61   #��[ 6>   �� 5�      $��Uwagi��6   $��3��6)   #��[ 66   �� 5�      $��1��6   $��0��6*   #��[ 6\   ��67   �� *NowePrzyjecie.Ilosc+6\     5�      $��1��6   $��1��6-   #��[ 6o      SelectedDate 6:   �� *NowePrzyjecie.DataPrzyjecia+6o   �� 5�      $��1��6   $��2��6*   #��[ 6c   ��67   �� *NowePrzyjecie.NumerFaktury+6c   �� 5�      $��1��6   $��3��6*   $
 Wrap=�67    . #��[ 6�   $	��100��6�   ��6`   �� *NowePrzyjecie.Uwagi+6�   �� 5�      $��5��6   #��[ 6&    5�        5�      .��5�      �� $ 
Horizontal=�6!   .+�5�      �� $4��./Opportunity;component/Model/Images/cancel.png��6    #��[
 6X   �� 5�      $��Anuluj��6$   #��[ 62   5�      5�      �� 5�      $��5��6   $��1��6&   #��[ 66   ��6Y   �� *DodajPrzyjecieCommand+{�6�   �� *IsTowarDoPrzyjecia+.��5�      �� $ 
Horizontal=�6!   .+�5�      �� $2��,/Opportunity;component/Model/Images/disk.png��6    #��[
 6V   �� 5�      $��
Zatwierdź��6$   #��[ 65   5�      5�      5�      5�      5�      5�      