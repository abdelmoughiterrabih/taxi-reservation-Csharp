   M S B A M L   `   `   `  ����G  Cgestion-taxi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:gestion_taxigestion_taxi  5   	       gestion_taxi.Reservation   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:gestion_taxi     5   	     � Title$  reservation��5   	   $	��505��6   $	��865��6*   .��5	      �� $��	0,0,701,0q�6   ���	#FEFFF8E2.+�5
   
   �� $��Réservation Taxi��6    �� Left$	 559��6+   
 �� Top$ -1��6=   $��Center=�6M   $��Center=�6q   $
��Bold"�6�   $��24(�6�   ���	#FEFFF8E26�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	DataGrid1��5      $	��334��6&   $	��497��63   $	 296��6?   $ 25��6Q   6a   �� 5   
   -   5         ��Name$ 
ClientList��5      $ 40��6'   $ 60��68   $	��182��6H    �� Text$ Choisir un Client...��6T   $��27��6p   $
��Bold"�6�   $��14(�6�    �� 
IsEditable$
 True��6�    �� 
IsReadOnly$
 True��6�   ��5      �� 5       �� EndPoint$ 0.5,1&�6&   	 �� 
StartPoint$	 0.5,0&�67   .��5      � 
 � Color$
 Black��6#   � 5      $	
 Red��6#    � Offset$ 1Q�6/   5      5      �� 5   
   -   5        $ TaxiList��5      $ 40��6%   $	 118��66   $	��182��6G   $ Choisir un Taxi Dispo...��6S   $��27��6s   $
��Bold"�6�   $��14(�6�   $
 True��6�   $
 True��6�   �� 5   
   -   5         ��Name$ PrixText��5      $ 40��6$   $	 167��65   $
��Prix��6F    �� TextWrapping$
 Wrap=�6R   $	��182��6f   $��25��6r   $��Center=�6~   $��14(�6�   $
��Bold"�6�   $	��Top=�6�   6�   �� 5   
   -   5        $ 	HeureText��5      $ 40��6%   $	 269��66   $��Heure:Minute��6G   $
 Wrap=�6[   $	��182��6o   $��25��6{   $��14(�6�   $
��Bold"�6�   6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DateText��5      $ 40��6'   $	 217��68   $��24��6I   $	��182��6U   �� 5   
   -   5         ��Name$ 
AjouterBtn��5      $��Ajouter��6%   $	 322��67   $	 390��6I   $��35��6Z   $	��101��6f   
����������	#FF0F0F0F
�������$��14(�6�   $
��Bold"�6�   6�   	� 5   
   -   5         	�Name$ Complete��5      $��	Complète��6(   $	 160��6;   $	 344��6M   $��Center=�6z   $	��Top=�6�   	� 5   
   -	   5        $ 
Incomplete��5      $��Incomplète��6*   $	 160��6?   $	 365��6Q   6b   �� 5   
   $��Etat de réservation:��6   $ 40��6/   $	 333��6@   �� 5   
   -
   5        $ 	RetourBtn��5      $��<Retour��6$   $ 10��69   $ 6��6J   $��27��6Y   $��63��6e   
����������	#FF0F0F0F
�������$��14(�6�   $
��Bold"�6�   6�   �� 5   
   -   5        $ ModifierBtn��5      $��Modifier��6&   $	 494��69   $	 390��6K   $��35��6\   $	��101��6h   
����������	#FF0F0F0F
�������$��14(�6�   $
��Bold"�6�   6�   �� 5   
   -   5        $ SupprimerBtn��5      $��	Supprimer��6'   $	 692��6;   $	 390��6M   $��35��6^   $	��101��6j   
����������	#FF0F0F0F
�������$��14(�6�   $
��Bold"�6�   6�   5      5       