   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	        WpfApp1.EmployeeListWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$)  #Список сотрудников��5      $	��500��6+   $	��750��68    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ CanMinimize=�6.   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��0,0,0,10q�6;   .+�5      �� $#��Фильтр по имени:��6   $��Center=�6/   $
��Bold"�6J   �� 5      -   5        $��FilterTextBox��5      $	��200��6+   $��10,0,0,0q�67   $��Center=�6I   6d   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��EmployeesDataGrid��5      $��1��6,      AutoGenerateColumns .     
IsReadOnly .    CanUserSortColumns .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 50 6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *
EmployeeId+6P     5      $	 ФИО��6%   $
 * 6O    62   �� *
FullName+6O     5      $	 Должность��6%   $	
 150 6Z    68   �� *PositionTitle+6Z     5      $	 
Отдел��6%   $	
 150 6W    64   �� *DepartmentName+6W     5      $	 Телефон��6%   $	
 120 6P    66   �� *Phone+   
Visibility 5   %   �� *PhoneVisibility+  5      $	 Email��6%   $	
 180 6N    64   �� *Email+ 5   %   �� *EmailVisibility+  5       $	 Дата приема��6%   $	
 120 6]    6:   �� *HireDateString+ 5!   %   �� *HireDateVisibility+  5"      $	 Зарплата��6%   $	
 100 6X    67   �� *SalaryString+ 5#   %   �� *SalaryVisibility+5%      �� 5'   
   $ 
Horizontal=�6   $��2��6.   $��Right=�6;   $��0,10,0,0q�6W   .+�5(      �� -   5        $3��-Создать заявку на отпуск��5(      $	��180��68   $��0,0,10,0q�6D   ���#28a745
�������$
��Bold"�5)   =   �� 5*      -   5        $ ��ViewVacationRequestsButton��5*      $5��/Просмотр заявок на отпуск��67   $	��180��6[   $��0,0,10,0q�6g   ���#17a2b8
�������$
��Bold"�5,   =   $y�	Collapsed=�6O   �� 5-      -   5        $��
Выйти��5-      $��80��6%   ���#dc3545
�������$
��Bold"�5.   =   5/      50      51      