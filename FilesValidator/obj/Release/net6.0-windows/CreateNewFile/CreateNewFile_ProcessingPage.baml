   M S B A M L   `   `   `  ���� I  EFilesValidator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:FilesValidatorFilesValidator  1    +FilesValidator.CreateNewFile_ProcessingPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .localclr-namespace:FilesValidator       N� Title$"  CreateNewFile_ProcessingPage��.���� $��5q�
�������._�� $��15q� � RowDefinitions �� $
��auto ��� $
��auto ��� �� $
��auto �.+��� -   $��status_textBlock��$��0��$!��正在创建校验信息...��$��Microsoft YaHei UI Light)�$��16(�� -   $��progressBar��$��1��$��16��$
��0 20q� � Value$ 0Q��� -   $��showPath_textBox��$��2�� �� 
IsReadOnly$
 True�� �� TextWrapping$
 Wrap=�" �� VerticalScrollBarVisibility$
 Auto=�� $��3��$��0 20 0 0q� � ColumnDefinitions �� �� .+��� -   $��0��$��中止��$��Microsoft YaHei UI Light)�$��16(�$	��150���� -   $��1��$��完成��$��Microsoft YaHei UI Light)�$��16(�$	��150��