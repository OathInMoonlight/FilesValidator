   M S B A M L   `   `   `  ����I  EFilesValidator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:FilesValidatorFilesValidator  5      0    *FilesValidator.CompareFiles_processingPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:FilesValidator     5        N� Title$!  CompareFiles_processingPage��5	      .��5
      �� $��5q�6   
�������._�5   
   � $��15q�6    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      �� 5      $
��auto �6    .+�5      �� -   5        $��status_textBlock��5   %   $��0��6   $$��正在对比两个校验文件��6=   $��Microsoft YaHei UI Light)�6O   $��16(�6u   � 5      -   5        $��progressBar��5   '   $��1��6   $��16��6:   $
��0 20q�6F    � Value$ 0Q�6T   �� 5      $��2��6   .@�5      �� $��校验一致的文件��6   $��Microsoft YaHei UI Light)�6+   $��14(�6Q    �� 
IsSelected$
 True��6_   .��5      �� -   5        $��showPath_textBox��5      $��0q�66    �� 
IsReadOnly$
 True��6J    �� TextWrapping$
 Wrap=�6\   " �� VerticalScrollBarVisibility$
 Auto=�6p   5      �� 5      $��丢失的文件��6   $��Microsoft YaHei UI Light)�6)   $��14(�6O   .��5      �� -   5        $��lostFiles_textBox��5      $��0q�67   $
 True��6K   $
 Wrap=�6]   $
 Auto=�6q   5      �� 5      $��发生变更的文件��6   $��Microsoft YaHei UI Light)�6+   $��14(�6Q   .��5      �� -   5        $��changedFiles_textBox��5      $��0q�6:   $
 True��6N   $
 Wrap=�6`   $
 Auto=�6t   5      �� 5      $��发生移动的文件��6   $��Microsoft YaHei UI Light)�6+   $��14(�6Q   .��5       �� -   5        $��movedFiles_textBox��5       $��0q�68   $
 True��6L   $
 Wrap=�6^   $
 Auto=�6r   5!      �� 5"      $��新增的文件��6   $��Microsoft YaHei UI Light)�6)   $��14(�6O   .��5#      �� -   5        $��newFiles_textBox��5#      $��0q�66   $
 True��6J   $
 Wrap=�6\   $
 Auto=�6p   5$      5%      � 5&      $��3��6   $
��0 20q�6     � ColumnDefinitions 5'      �� 5(      �� 5)      .+�5,      �� -   5        $��0��5,      $��中止��6)   $��Microsoft YaHei UI Light)�66   $��16(�6\   $	��150��6j   6v   �� 5-      -	   5        $��1��5-      $��完成��6)   $��Microsoft YaHei UI Light)�66   $��16(�6\   $	��150��6j   6v   5.      5/      50      51      