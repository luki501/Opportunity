   M S B A M L   `   `   `  ����F  BOpportunity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Opportunity.ViewOpportunity.View  5   	   ,    &Opportunity.View.PracownikEdycjaWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Opportunity.View     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cie:http://schemas.microsoft.com/expression/2010/interactivity  5   	   h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Ai9http://schemas.microsoft.com/expression/2010/interactions  5	   	     � Title$  Dodaj pracownika��5   	   $	��400��6"   $	��400��6/    � DataContext 5
   	   ��    Locator �� Source# [   �� Path$ PracownikEdycja� � 	Resources 5      ,   &Opportunity.View.MultiBindingConverter  MultiBindingConverter0   *Opportunity.View.BoolToVisibilityConverter  BoolToVisibilityConverter8   2Opportunity.View.BoolOppositeToVisibilityConverter % !BoolOppositeToVisibilityConverter  tbNormal �� 
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
   .��59      �� -   5        	 ��Name$	 tabPracownik��59      ��5:   
   �� 5;      " ��6   
 �� Triggers
 5<      �� 5=       �� Binding 6"   �� *PracownikZapisany+ �� Value True6H   .a�5>      ��  �@��SelectedIndex$ 1��6:   5?      5A      .@�5C   
   �� $
��Dane��6   .��5D      �  � ColumnDefinitions 5E      �� 5F      $��1* �6'   �� 5G      $��1* �6'    � RowDefinitions 5I      �� 5J      $
��auto �6$   �� 5K      $
��auto �6$   �� 5L      $
��auto �6$   �� 5M      $
��auto �6$   �� 5N      $
��auto �6$   �� 5O      $
��auto �6$   �� 5P      $
��auto �6$   �� 5Q      $
��auto �6$   .+�5S      �� $��Nazwa��6   $��0��6)   #��[ 66   �� 5T      $��Nazwisko��6   $��1��6,   #��[ 69   �� 5U      $��Imię��6   $��2��6(   #��[ 65   �� 5V      $��Data urodzenia��6   $��3��62   #��[ 6?   �� 5W      $��Uwagi��6   $��4��6)   #��[ 66   �� 5X      -   5         ��Name$ tbNumer��5X      $��1��6+   $��0��6;   #��[ 6m   ��6H   �� *NowyPracownik.Numer+6m   �� 5Y      $��1��6   $��1��6*   #��[ 6_   ��67   �� *NowyPracownik.Nazwisko+6_   �� 5Z      $��1��6   $��2��6*   #��[ 6[   ��67   �� *NowyPracownik.Imie+6[     5[      $��1��6   $��3��6-   #��[ 6o      SelectedDate 6:   �� *NowyPracownik.DataUrodzenia+6o   �� 5\      $��1��6   $��4��6*    �� TextWrapping$
 Wrap=�67    �� AcceptsReturn . #��[ 6�   $	��100��6�   ��6`   �� *NowyPracownik.Uwagi+6�   �� 5]      -   5         ��Name$ cbBrygadzista��5]      $��0��62   $��5��6B   $��Brygadzista��6O   #��[ 6�    d� 	IsChecked 6e   �� *NowyPracownik.Brygadzista+6�   �� 5c      $��6��6   #��[ 6&   $
��Left=�6J    5d      "   Opportunity.View.ZamknijOkno  5e      .��5g      ��  �� Orientation$ 
Horizontal=�6!   .+�5h      �� $4��./Opportunity;component/Model/Images/cancel.png��6    #��[
 6X   �� 5i      $��Anuluj��6$   #��[ 62   5j      5k      �� 5l      $��6��6   $��1��6&   #��[ 66   $��Right=�6Y   ��6u   �� *ZapiszPracownikaCommand+ 5m      )   #Opportunity.View.PasswordEdycjaOpen  5n         NowyUzytkownik . 63   .��5p      �� $ 
Horizontal=�6!   .+�5q      �� $2��,/Opportunity;component/Model/Images/disk.png��6    #��[
 6V   �� 5r      $��
Zatwierdź��6$   #��[ 65   5s      5t      5u      5v      �� 5w   
   $��	Dokumenty��6   y�6%   �� *PracownikZapisany+ �� 	Converter# [ .��5x      �  5y      �� 5z      $��1* �6'   �� 5{      $��1* �6'    5}      �� 5~      $
��auto �6$   �� 5      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   .+�5�      �� $
��Typ:��6   #��[ 6(   $��2��6K   �� 5�      -   5         ��Name$ cbNowyDokumentTyp��5�      $��2��66   $��0��6F   #��[	 6S   ��6u   �� *ListaTypowDokumentow+ �� SelectedItem 6�   �� *WybranyTypDokumentu+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5�      �� 5�      $��1��6   $��Numer:��6)   #��[ 67   �� 5�      -   5        $ txtNowyDokNumer��5�   �   $��1��6   $��2��6'   #��[ 6�   ��67   �� *NowyDokument.Numer+$ TwoWay=�$ 	LostFocus=�6�   �� 5�      $��2��6   $��Data wystawienia��6)   #��[ 6A     5�      -   5          Name$ dpNowyDokDataWyst��5�   �   $��2��6   $��2��6*   #��[ 6�    6:   �� *NowyDokument.DataWystawienia+$ TwoWay=�$ PropertyChanged=�6�   �� 5�      $��3��6   $��Data ważności��6)   #��[ 6>     5�      -   5        $ dpNowyDokDataWazn��5�   �   $��3��6   $��2��6*   #��[ 6�    6:   �� *NowyDokument.DataWaznosci+$ TwoWay=�$ PropertyChanged=�6�   �� 5�      $��4��6   $��Uwagi��6)   #��[ 66   �� 5�      -   5        $ txtNowyDokUwagi��5�   �   $��4��6   $��2��6'   #��[ 6�   $��50��6�   ��67   �� *NowyDokument.Uwagi+$ TwoWay=�$ 	LostFocus=�6�   �� 5�      $��5��6   #��[ 6&   $
��Left=�6I    5�        5�      .��5�      �� $ 
Horizontal=�6!   .+�5�      �� $4��./Opportunity;component/Model/Images/cancel.png��6    #��[
 6X   �� 5�      $��Anuluj��6$   #��[ 62   5�      5�      �� 5�      $��2��6   $��5��6)   #��[ 66   $��Right=�6Y   ��5�      �� *DodajDokumentCommand+{�6B   �� *PracownikZapisany+   Triggers 5�      /	  )System.Windows.Interactivity.EventTrigger	  5�       	  	EventName$ Click��6*   .
  (System.Windows.Interactivity.TriggerBase 
  Actions. 5�      B  <Microsoft.Expression.Interactivity.Core.ChangePropertyAction      PropertyName$  SelectedIndex��6l   !   Value$! -1��6�   8  2System.Windows.Interactivity.TargetedTriggerAction"   TargetObject" 65   �� # �� ElementName$# cbNowyDokumentTyp��6�   5�      .��5�      �� $ 
Horizontal=�6!   .+�5�      �� $1��+/Opportunity;component/Model/Images/add.png��6    #��[
 6U   �� 5�      $��Dodaj��6$   #��[ 61   5�      5�      5�      5�      5�      5�      