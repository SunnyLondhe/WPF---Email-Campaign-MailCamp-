   M S B A M L   `   `   `  ����I  EEMail_Campaign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:EMail_Campaign.UserControlEMail_Campaign.UserControl  5      *    $EMail_Campaign.UserControl.Dashboard   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:EMail_Campaign.UserControl     5      .��5	      �   � RowDefinitions  5   
   �� 5      $��* �6   �� 5      $��5 �6   �� 5      $��2* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��5 �6   �� 5      $��* �6   .+�5   
   � .+�5      �� $!��Send Manual Email to Groups��6   $��10q�6<   .��5      � .+�5      ��  �� Orientation$ 
Horizontal=�6!   $	��Top=�6;   $��0,10,0,0q�6S   .+�5      �� $
��Left=�6$   $
��Wrap=�6?   $	��150��6S   $��Select Email Template��6_   $��Center=�6}   �� 5      -   5         ��Name$ cboTemplate��5   #   $	��150��68    �� DisplayMemberPath$ TemplateName��6F    �� SelectedIndex$ 0��6h   5      �� 5      $ 
Horizontal=�6!   $	��Top=�6:   $��0,40,0,0q�6R   .+�5      �� $
��Left=�6$   $
��Wrap=�6?   $	��150��6S   $��Select Contact Group��6_   $��Center=�6|   �� 5      -   5        $ cboConGroup��5   #   $	��150��68   $ 0��6E   5       �� 5!      $ 
Horizontal=�6!   $	��Top=�6:   $��
150,75,0,0q�6R   .+�5"      �� -   5        $��btnSendMail��5"   6   $��
Send Email��6!   $
��Left=�6I   $	��Top=�6e   $��75��6}   6�   5#      5&      5(      5)      �� 5*   
   $��1��6   $��5��6$   $��Stretch=�6/   ���	#FFA2A2A26N   � 5+   
   $��2��6   .+�5,      �� $��Sent Mail Details��6   $��10q�62   .��5-      � .+�5.      �� $
��Left=�6!   $��0,5,0,0q�6=   $	��Top=�6N   $	��180��6f   $
��auto��6r   .+�5/      �� $
��Wrap=�6$   $��
Filter Log��68   �� 50      -   5         ��Name$ tbx_filterSentMailLog��50   "   $��23��6A    �� TextWrapping$
 Wrap=�6M   6b   51      �� 52      -   5        $��lbx_EmailSent��52   1   $��0,50,0,0q�6    �� View 53      �� 54      	 �� AllowsColumnReorder	 . 
 �� ColumnHeaderToolTip$
 Contact List��6B    �� ColumnHeaderContextMenu 57   "   �� 58   &   .@�59   *   m� $��	Ascending��63   m� 5:   *   $��
Descending��63   5;   '   .P�5>   "   �� $��Log Time��61    �� Width$	 150��6C    �� DisplayMemberBinding 6O   ��  �� Path$ logtime��� 5?   "   $
��Name��61   $	 100��6?    6K   �� $ contactname��� 5@   "   $��Email��61   $	 160��6@    6L   �� $ email��� 5A   "   $��Status��61   $	 100��6A    6M   �� $ status��� 5B   "   $��Message��61   $	 200��6B    6N   �� $ message�5E      5G      5I      5K      5N      � 5P   
   $��2��6   $��2��6   .+�5Q      �� $��	Sent Logs��6   $��10q�6*   .��5R      � .+�5S      �� $
��Left=�6!   $��0,5,0,0q�6=   $	��Top=�6N   $	��180��6f   $
��auto��6r   .+�5T      �� $
��Wrap=�6$   $��Filter contact by name��68   �� 5U      -   5        $ tbx_filterSentLogs��5U   "   $��23��6>   $
 Wrap=�6J   6^   5V      �� 5W      -   5        $��lbx_ErrorLog��5W   1   $��0,50,0,0q�6    5X      �� 5Y      	 . $
 Contact List��6B    5\   "   �� 5]   &   .@�5^   *   m� $��	Ascending��63   m� 5_   *   $��
Descending��63   5`   '   .P�5c   "   �� $��	 Log Time��61   $	 130��6D    6P   �� $ logtime��� 5d   "   $�� Error Type��61   $	 100��6F    6R   �� $ 	ErrorType��� 5e   "   $�� Group Name��61   $	 100��6F    6R   �� $ 	groupname��� 5f   "   $�� Template Name��61   $	 100��6I    6U   �� $ templateName��� 5g   "   $�� Discription��61   $	 130��6G    6S   �� $ logDiscription�5j      5l      5n      5p      5q      �� 5r   
   $��2��6   $��1��6'   $��Stretch=�64   ���	#FFA2A2A26R   �� 5s   
   $��1��6   $��2��6'   $��Stretch=�64   ���	#FFA2A2A26R   �� 5t   
   $��1��6   $��5��6'   $��Stretch=�61   ���	#FFA2A2A26O   � 5u   
   $��2��6   .+�5v      �� $��Schedule Email��6   $��10q�6/   .��5w      � .+�5x      �� $ 
Horizontal=�6!   $	��Top=�6;   $��0,10,0,0q�6S   .+�5y      �� $
��Left=�6$   $
��Wrap=�6?   $	��150��6S   $��	Task Name��6_   $��Center=�6q   �� 5z      -   5        $ txtTaskName��5z   "   $��MailChampSchedule��67   $
��Left=�6P   $��22��6k   $
 Wrap=�6w   $	��200��6�   5{      �� 5|      $ 
Horizontal=�6!   $	��Top=�6:   $��0,40,0,0q�6R   .+�5}      �� $
��Left=�6$   $
��Wrap=�6?   $	��150��6S   $��Trigger��6_   $��Center=�6o   �� 5~      -	   5        $ cboTaskTrigger��5~   #   $	��100��6;   $ 0��6G   .@�5      �� .��6+   ONCE61   �� 5�      .��6+   DAILY62   5�      5�      �� 5�      $ 
Horizontal=�6!   $	��Top=�6:   $��0,70,0,0q�6R   .+�5�      �� $
��Left=�6$   $
��Wrap=�6?   $	��150��6S   $
��Date��6_   $��Center=�6l   �� 5�      -
   5        $ txtSchDatePick��5�   "   $
��Left=�6:   $��22��6U   $
 Wrap=�6a   $	��100��6u   �� 5�      $
��Wrap=�6%   $	��150��69   $��HH:MM:SS��6E   $��Right=�6V   $��5,0,0,0q�6r   5�      �� 5�      $ 
Horizontal=�6!   $	��Top=�6:   $��	0,100,0,0q�6R   .+�5�      �� $
��Left=�6$   $
��Wrap=�6?   $	��150��6S   $��Start��6_   $��Center=�6m   �� 5�      -   5        $ txtStartTime��5�   "   $
��Left=�68   $��22��6S   $
 Wrap=�6_   $	��100��6s   �� 5�      $
��Wrap=�6%   $	��150��69   $��HH:MM:SS��6E   $��Right=�6V   $��5,0,0,0q�6r   5�      �� 5�      $ 
Horizontal=�6!   $	��Top=�6;   $��	0,130,0,0q�6S   .+�5�      �� $
��Left=�6$   $
��Wrap=�6?   $	��150��6S   $��Select Email Template��6_   $��Center=�6}   �� 5�      -   5        $ cboSchTemplate��5�   #   $	��150��6;   $ TemplateName��6I   $ 0��6k   5�      �� 5�      $ 
Horizontal=�6!   $	��Top=�6:   $��	0,170,0,0q�6R   .+�5�      �� $
��Left=�6$   $
��Wrap=�6?   $	��150��6S   $��Select Contact Group��6_   $��Center=�6|   �� 5�      -   5        $ cboSchConGroup��5�   #   $	��150��6;   $ 0��6H   5�      �� 5�      $ 
Horizontal=�6!   $	��Top=�6:   $��150,200,0,0q�6R   .+�5�      �� -   5        $��btnSchedule��5�   4   $��Schedule��6!   $
��Left=�6G   $	��Top=�6c   $��75��6{   6�   �� 5�      -   5        $��btnWinSchedular��5�   O   $��Open Win Schedular��6!   $��5,0,0,0q�6>   $
��Left=�6f   $	��Top=�6�   $	��120��6�   6�   5�      5�      5�      5�      5�      5�      