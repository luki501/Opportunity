   M S B A M L   `   `   `  ����F  BOpportunity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Opportunity.ViewOpportunity.View  5   	   (    "Opportunity.View.PasswordBoxWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cie:http://schemas.microsoft.com/expression/2010/interactivity  5   	   h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Ai9http://schemas.microsoft.com/expression/2010/interactions  5   	   *localclr-namespace:Opportunity.View  5   	     � Title$  Dane logowania��5   	   $	��175��6    $	��250��6-    � 
ResizeMode$ NoResize=�69    � DataContext 5
   	   ��    Locator �� Source# [   �� Path$ PasswordBox� � 	Resources 5      0   *Opportunity.View.BoolToVisibilityConverter  BoolToVisibilityConverter8   2Opportunity.View.BoolOppositeToVisibilityConverter % !BoolOppositeToVisibilityConverter �� 
TargetType �� Property �� Value��%�  &	       &	      'G� &     '�� �     '�� i    '�� �    '�� �      5   
   6*     5   
   62   �� 5   
   " G�6   .�5      ��  � ��$ 20��6'   �� 5       � ��$ 3q�6'   �� 5       � ��$ 12(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$	 100��6&   �� 5       � ��$ 20��6'   �� 5       � ��$ 3q�6'   �� 5       � ��$ 12(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$ 12(�6)   �� 5       � ��$ Center=�62   �� 5       � ��$ 3q�6'   5      �� 5   
   " ��6   .�5       ��  � ��$ 5q�6'   �� 5!       � ��$ 80��6)   �� 5"       � ��$	 100��6)   �� 5#       � ��$ 32��6'   �� 5$       � �� �#cebbad6+   5%      �� 5&   
   " ��6   .�5'      ��  � ��$ 20��6&   �� 5(       � ��$ 20��6'   �� 5)       � ��$ 3q�6'   5*      5+      .��5,      � 	 � ColumnDefinitions	 5-   
   �� 5.      �� 5/      
 � RowDefinitions
 51   
   �� 52      $
��auto �6   �� 53      $
��auto �6   �� 54      $
��auto �6   �� 55      $
��auto �6   �� 56      .+�58   
   �� $��0��6   $��0��6$   $��Login��61   �� 59   
   $��0��6   $��1��6$   $��Stare hasło��61   y�6D   �� *NowyUzytkownik+ �� 	Converter# [ �� 5:   
   $��0��6   $��2��6$   $��Hasło��61   �� 5;   
   $��0��6   $��3��6$   $��Powtórz hasło��61   �� 5<   
   $��1��6   $��0��6$   ��61   �� *Login+G� 5=   
   -   5        $��NetworkPasswordOld��5=   3   $��1��6   $��1��6&   +   %Opportunity.View.PasswordBoxAssistant   BindPassword .    BoundPassword 6|   �� $ PasswordOld� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�y�6�   �� *NowyUzytkownik+# [ G� 5>   
   -   5        $��NetworkPassword��5>   3   $��1��6   $��2��6&    .  6y   �� $ Password�$ TwoWay=�$ PropertyChanged=�G� 5?   
   -   5        $��NetworkPassword2��5?   3   $��1��6   $��3��6&    .  6z   �� $ 	Password2�$ TwoWay=�$ PropertyChanged=��� 5@   
   $��1��6   $��4��6!   ��6.   �� *ZapiszHasloCommand+.  (System.Windows.Interactivity.Interaction   	Behaviors 5A      "   Opportunity.View.ZamknijOkno  5B      .��5D      ��  �� Orientation$ 
Horizontal=�6   .+�5E      �� $2��,/Opportunity;component/Model/Images/disk.png��6   �� 5F      $��
Zatwierdź��6   5G      5H      5I      5J      