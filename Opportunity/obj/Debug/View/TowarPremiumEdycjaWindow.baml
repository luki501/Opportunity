   M S B A M L   `   `   `  ����F  BOpportunity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Opportunity.ViewOpportunity.View  5   	   /    )Opportunity.View.TowarPremiumEdycjaWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Opportunity.View     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cie:http://schemas.microsoft.com/expression/2010/interactivity  5   	   h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Ai9http://schemas.microsoft.com/expression/2010/interactions  5	   	     � Title$  Dodaj towar��5   	   $	��400��6   $	��400��6*    � DataContext 5
   	   ��    Locator �� Source# [   �� Path$ TowarEdycja� � 	Resources 5      ,   &Opportunity.View.MultiBindingConverter  MultiBindingConverter0   *Opportunity.View.BoolToVisibilityConverter  BoolToVisibilityConverter8   2Opportunity.View.BoolOppositeToVisibilityConverter % !BoolOppositeToVisibilityConverter  tbNormal �� 
TargetType �� Property �� Value  	txtNormal  	btnNormal  btnSmall  	chbNormal 	 dpNormal(  "System.Windows.Controls.DatePicker 
 cbNormal  imgButtonNormal  imgButtonSmall  txtButtonNormal  txtButtonSmall��%  &	       &	      &	 &     &	 9     &	 �     &	 �     &	 b    &	 @    &		 �    &	
 b    &	 C    &	 �    &	 a    &	 �      5   
   6&     5   
   6*     5   
   62   �� 5   
   " ��6!   .�5      ��  � ��$ 3q�6'   5      �� 5   
   " ��6"   .�5      ��  � ��$ Center=�62   5      �� 5   
   " ��6"   .�5      ��  � ��$ 80��6)   �� 5       � ��$ 10q�6'   �� 5       � �� �#cebbad6+   5      �� 5   
   " ��6!   .�5      ��  � ��$ 12(�6)   �� 5       � ��$ 3q�6'   �� 5       � ��$ 24��6'   �� 5       � �� �#cebbad6+   �� 5        � ��$	 100��6)   5!      �� 5"   
   " ��6"   .�5#      ��  � ��$ 12(�6)   �� 5$       � ��$ Center=�62   �� 5%       � ��$ 3q�6'   5&      �� 5'   
   "  6!   .�5(      ��  � ��$ 20��6'   �� 5)       � ��$ 3q�6'   �� 5*       � ��$ 10(�6)   5+      �� 5,   
   " ��6!   .�5-      ��  � ��$ 12(�6)   �� 5.       � ��$ Center=�62   �� 5/       � ��$ 3q�6'   �� 50       � ��$ 24��6'   �� 51       � �� �#cebbad6+   52      �� 53   
   " ��6(   .�54      ��  � ��$ 20��6&   �� 55       � ��$ 20��6'   �� 56       � ��$ 3q�6'   57      �� 58   
   " ��6'   .�59      ��  � ��$ 16��6&   �� 5:       � ��$ 16��6'   �� 5;       � ��$ 2q�6'   5<      �� 5=   
   " ��6(   .�5>      ��  � ��$ 14(�6)   �� 5?       � ��$ Center=�62   �� 5@       � ��$ 2q�6'   5A      �� 5B   
   " ��6'   .�5C      ��  � ��$ 10(�6)   �� 5D       � ��$ Center=�62   �� 5E       � ��$ 2q�6'   5F      5G      .  (System.Windows.Interactivity.Interaction   	Behaviors 5H      $   Opportunity.View.ZamykanieOkna  5I   
   .��5K      �� -   5        	 ��Name$	 tabTowar��5K      ��5L   
   �� 5M      " ��6   
 �� Triggers
 5N      �� 5O       �� Binding 6"   �� *IsTowarZapisany+ �� Value True6F   .a�5P      ��  �@��SelectedIndex$ 1��6:   5Q      5S      .@�5U   
   �� $
��Dane��6   .��5V      �  � ColumnDefinitions 5W      �� 5X      �� 5Y       � RowDefinitions 5[      �� 5\      $
��auto �6$   �� 5]      $
��auto �6$   �� 5^      $
��auto �6$   �� 5_      $
��auto �6$   �� 5`      $
��auto �6$   �� 5a      $
��auto �6$   �� 5b      $
��auto �6$   .+�5d      �� $	��Typ��6   $��0��6'   #��[ 64   �� 5e      $��Marka��6   $��1��6)   #��[ 66   �� 5f      $��Model��6   $��2��6)   #��[ 66   �� 5g      $	��EAN��6   $��3��6'   #��[ 64   �� 5h      $��Uwagi��6   $��4��6)   #��[ 66   �� 5i      $��1��6   $��0��6+   #��[
 6b    �� DisplayMemberPath$ Nazwa��6�    �� SelectedValuePath$ Id��6�   ��68   �� *ListaTypowTowarow+ �� SelectedValue 6�   �� *WybranyTypId+�� 5j      $��1��6   $��1��6+   #��[
 6b   $ Nazwa��6�   $ Id��6�   ��68   �� *ListaMarekTowarow+ 6�   �� *WybranaMarkaId+�� 5k      $��1��6   $��2��6*   #��[ 6X   ��67   �� *NowyTowar.Model+6X   �� 5l      $��1��6   $��3��6*   #��[ 6V   ��67   �� *NowyTowar.Ean+6V   �� 5m      $��1��6   $��4��6*    �� TextWrapping$
 Wrap=�67    �� AcceptsReturn . #��[ 6�   $	��100��6�   ��6`   �� *NowyTowar.Uwagi+6�   �� 5n      $��6��6   #��[ 6&   ��6I   �� *ZapiszTowarCommand+.��5o      ��  �� Orientation$ 
Horizontal=�6!   .+�5p      �� $4��./Opportunity;component/Model/Images/cancel.png��6    #��[ 6X   �� 5q      $��Anuluj��6$   #��[ 62   5r      5s      �� 5t      $��6��6   $��1��6&   #��[ 66   ��6Y   �� *ZapiszTowarCommand+.��5u      �� $ 
Horizontal=�6!   .+�5v      �� $2��,/Opportunity;component/Model/Images/disk.png��6    #��[ 6V   �� 5w      $��
Zatwierdź��6$   #��[ 65   5x      5y      5z      5{      �� 5|   
   $��Pliki��6   {�6!   �� *IsTowarZapisany+.��5}      �  5~      �� 5      �� 5�       5�      �� 5�      $��5* �6$   �� 5�      $��1* �6$   �� 5�      $��1* �6$   .+�5�      �� $��2��6   .+�5�      �� #��[ 6    5�          Opportunity.View.DodajPlik  5�         Field$ path_zdjecie��6.   .��5�      �� $ 
Horizontal=�6%   .+�5�      �� $1��+/Opportunity;component/Model/Images/add.png��6$   #��[ 6Y   �� 5�      $��Zdjęcie��6(   #��[ 67   5�      5�      �� 5�      #��[ 6    5�        5�      $ path_gwarancja��6.   .��5�      �� $ 
Horizontal=�6%   .+�5�      �� $1��+/Opportunity;component/Model/Images/add.png��6$   #��[ 6Y   �� 5�      $��	Gwarancja��6(   #��[ 69   5�      5�      5�      �� 5�      $��1��6   #��[ 6&    5�      "   Opportunity.View.ZamknijOkno  5�      .��5�      �� $ 
Horizontal=�6!   .+�5�      �� $4��./Opportunity;component/Model/Images/cancel.png��6    #��[ 6X   �� 5�      $��Anuluj��6$   #��[ 62   5�      5�      �� 5�      $��1��6   $��1��6&   #��[ 66   ��6Y   �� *ZapiszTowarCommand+   Triggers 5�      /	  )System.Windows.Interactivity.EventTrigger	  5�       	  	EventName$ Click��6*   .
  (System.Windows.Interactivity.TriggerBase 
  Actions. 5�      B  <Microsoft.Expression.Interactivity.Core.ChangePropertyAction     PropertyName$ SelectedIndex��6c      Value$ 2��6�   8  2System.Windows.Interactivity.TargetedTriggerAction   TargetObject 65   ��  �� ElementName$ tabTowar��6�   5�      .��5�      �� $ 
Horizontal=�6!   .+�5�      �� $9��3/Opportunity;component/Model/Images/arrow_right.png��6    #��[ 6]   �� 5�      $��Dalej��6$   #��[ 61   5�      5�      5�      5�      �� 5�   
   $��	Przyjecie��6   {�6%   �� *IsTowarZapisany+.��5�      �  5�      �� 5�      �� 5�       5�      �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   .+�5�      �� $��Ilość��6   $��0��6)   #��[ 66   y�6Y   �� *	Premium+ �� 	Converter# [ �� 5�      $
��Data��6   $��1��6(   #��[ 65   �� 5�      $��Numer faktury��6   $��2��61   #��[ 6>   �� 5�      $��Uwagi��6   $��3��6)   #��[ 66   �� 5�      $��1��6   $��0��6*   #��[ 6\   ��67   �� *NowePrzyjecie.Ilosc+y�6~   �� *	Premium+# [   5�      $��1��6   $��1��6-   #��[	 6o      SelectedDate 6:   �� *NowePrzyjecie.DataPrzyjecia+6o   �� 5�      $��1��6   $��2��6*   #��[ 6c   ��67   �� *NowePrzyjecie.NumerFaktury+6c   �� 5�      $��1��6   $��3��6*   $
 Wrap=�67    . #��[ 6�   $	��100��6�   ��6`   �� *NowePrzyjecie.Uwagi+6�   �� 5�      $��5��6   #��[ 6&    5�        5�      .��5�      �� $ 
Horizontal=�6!   .+�5�      �� $4��./Opportunity;component/Model/Images/cancel.png��6    #��[ 6X   �� 5�      $��Anuluj��6$   #��[ 62   5�      5�      �� 5�      $��5��6   $��1��6&   #��[ 66   ��6Y   �� *DodajPrzyjecieCommand+{�6�   �� *IsTowarDoPrzyjecia+.��5�      �� $ 
Horizontal=�6!   .+�5�      �� $2��,/Opportunity;component/Model/Images/disk.png��6    #��[ 6V   �� 5�      $��
Zatwierdź��6$   #��[ 65   5�      5�      5�      5�      5�      5�      