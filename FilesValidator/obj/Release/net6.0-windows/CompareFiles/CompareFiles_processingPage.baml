   M S B A M L   `   `   `  ���� I  EFilesValidator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:FilesValidatorFilesValidator  0    *FilesValidator.CompareFiles_processingPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .localclr-namespace:FilesValidator       N� Title$!  CompareFiles_processingPage��.���� $��5q�
�������._�� $��15q� � RowDefinitions �� $
��auto ��� $
��auto ��� �� $
��auto �.+��� -   $��status_textBlock��$��0��$$��正在对比两个校验文件��$��Microsoft YaHei UI Light)�$��16(�� -   $��progressBar��$��1��$��16��$
��0 20q� � Value$ 0Q��� $��2��.@��� $��校验一致的文件��$��Microsoft YaHei UI Light)�$��14(� �� 
IsSelected$
 True��.���� -   $��showPath_textBox��$��0q� �� 
IsReadOnly$
 True�� �� TextWrapping$
 Wrap=�" �� VerticalScrollBarVisibility$
 Auto=��� $��丢失的文件��$��Microsoft YaHei UI Light)�$��14(�.���� -   $��lostFiles_textBox��$��0q�$
 True��$
 Wrap=�$
 Auto=��� $��发生变更的文件��$��Microsoft YaHei UI Light)�$��14(�.���� -   $��changedFiles_textBox��$��0q�$
 True��$
 Wrap=�$
 Auto=��� $��发生移动的文件��$��Microsoft YaHei UI Light)�$��14(�.���� -   $��movedFiles_textBox��$��0q�$
 True��$
 Wrap=�$
 Auto=��� $��新增的文件��$��Microsoft YaHei UI Light)�$��14(�.���� -   $��newFiles_textBox��$��0q�$
 True��$
 Wrap=�$
 Auto=�� $��3��$
��0 20q� � ColumnDefinitions �� �� .+��� -   $��0��$��中止��$��Microsoft YaHei UI Light)�$��16(�$	��150���� -	   $��1��$��完成��$��Microsoft YaHei UI Light)�$��16(�$	��150��