   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	   #    WpfApp1.VacationRequestWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$$  Заявка на отпуск��5   	   $	��300��6"   $	��400��6/    � WindowStartupLocation$ CenterOwner=�5	   	    � 
ResizeMode$ NoResize=�6-   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $+��%Дата начала отпуска:��6   $��0��6/   $
��Bold"�6<   $��Center=�6N   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��StartDatePicker��5      $��0��6,   $��	150,0,0,0q�69   �� 5   
   $1��+Дата окончания отпуска:��6   $��1��62   $
��Bold"�6?   $��Center=�6Q   $��0,10,0,0q�6l     5   
   -   5        $��EndDatePicker��5      $��1��6*   $��
150,10,0,0q�67   �� 5   
   $��Комментарий:��6   $��2��6'   $��0,10,0,0q�64   $
��Bold"�6F   �� 5   
   -   5        $��CommentTextBox��5      $��3��6(    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�6J   $��0,5,0,0q�5      $	��100��6#    �� TextWrapping$
 Wrap=�60   �� 5   
    �� Orientation$ 
Horizontal=�6   $��4��6.   $��Right=�6;   $��0,15,0,0q�6W   .+�5      �� -   5        $��Отправить��5      $	��100��6)   $��0,0,10,0q�65   ���#007ACC
�������$
��Bold"�6�   �� 5      -   5        $��Отмена��5      $��80��6&   61   5       5!      5"      