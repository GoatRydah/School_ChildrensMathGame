   M S B A M L   `   `   `  ����F  BAssignment5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Assignment5Assignment5  5   	       Assignment5.GameWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Assignment5     5   	   -   5          � Title$  
GameWindow��5   	   $	��450��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�6N    � 
ResizeMode$ NoResize=�6s   .��5	      � ��5
   
   �� 5       �� ImageSource$ /Images/back2.jpg��6    � ColumnDefinitions 5   
   �� 5      $
��389* �6   �� 5      $��7* �6   .+�5   
   �� ���	#FFDCB882$
��Left=�6%   $	��146��6@   $��	16,19,0,0q�6M   ���	#FFD67F00$	��Top=�6s   $	��229��6�   �� 5   
   -   5         ��Name$ CurrentProblemLabel��5      $��Problem��6-   $
��Left=�6?   $��	26,82,0,0q�6Z   $	��Top=�6m   $��Kristen ITC)�6�    J� RenderTransformOrigin$ 	0.517,1.5&�6�   $
��Bold"�6�   $��18(�6�   �� 5   
   -   5         ��Name$ ProblemAnswerTextBox��5      $
��Left=�60   $��23��6K   $��
117,87,0,0q�6W    �� TextWrapping$
 Wrap=�6k   $	��Top=�6   $��94��6�   $��Kristen ITC)�6�   	 �� 	MaxLength$	 3��6�   �� 5   
   -   5        
 ��Name$
 SubmitAnswerButton��5      $��Submit��6-   $
��Left=�6>   $��
87,124,0,0q�6Y   $	��Top=�6m   $��76��6�   $��Kristen ITC)�6�   ���	#FFE09323$
��Bold"�6�   $ 
0.184,3.45&�6�   $��26��6  �� 5   
   -   5        $ FeedbackLabel��5      $��Feedback��6'   $
��Left=�6:   $��	19,51,0,0q�6U   $	��Top=�6h   $��Kristen ITC)�6�   �� 5   
   -   5        $ ProblemNumberLabel��5      $��Problem Number: ��6,   $
��Left=�6G   $��	16,23,0,0q�6b   $	��Top=�6u   $��Kristen ITC)�6�   ���	#FFB06E0D6�   �� 5   
   -   5        $
 
ExitButton��5      $
��Exit��6%   $
��Left=�64   $��
19,380,0,0q�6O   $	��Top=�6c   $��75��6{   $��Kristen ITC)�6�   ���	#FFE09323$��28��6�   $��14(�6�   �� 5   
   $
��Left=�6   $	��100��6+   $��501,173,0,0q�68   $	��Top=�6M   $	��100��6e   $ 	0.54,0.54&�6q   $��Images/alligator.png��6�    J� OpacityMask 6�   Z� �� 5   
   -   5        $ 
TimerLabel��5      $��Timer��6$   $
��Left=�64   $��
160,23,0,0q�6O   $	��Top=�6c   $��Kristen ITC)�6{   �� 5   
   -	   5         ��Name$ AnimalImage��5      $
��Left=�6%   $	��100��6@   $��123,210,0,0q�6M   $	��Top=�6b   $	��100��6z   $ 	0.54,0.54&�6�   $�� ��6�    6�   Z� �� 5   
   -
   5        $ Feedback2Label��5      $�� ��6(   $
��Left=�63   $��248,348,0,0q�6N   $	��Top=�6c   ���	#FF005DFF$��Kristen ITC)�6�   $��14(�6�   5      5      