   M S B A M L   `   `   `  ����  FlashCardApp*    $FlashCardApp.Views.ChooseTopicWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Choose Topic��5   	   $	��422��6   $	��554��6+    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.   .��5      �� $��20q�6   .+�5   
   �� $��Select a Topic to Review:��6   $��16(�65   $��0,0,0,10q�6C   � 5	   
   .+�5
      �� -   5         ��Name$ 	SearchBox��5
      $	��300��6)   $��0,0,0,10q�65   5      �� 5      -   5         ��Name$ SearchPlaceholder��5      $��Search topic...��5      
�������$��5,0,0,10q�5      $��Center=�5       J� IsHitTestVisible$ False��5      $y�Visible=�5      5      �� 5   
   -   5         ��Name$ TopicListBox��5       �� DisplayMemberPath$ Title��6(   $	��239��6B   �� 5   
    �� Orientation$ 
Horizontal=�6   $��Right=�6.   $��0,10,0,0q�6J   .+�5      �� -   5        $��OK��5      $��80��6"   $��5q�6-   68   �� 5      -   5        $��Cancel��5      $��80��6&   $��5q�61   6<   5      5      5      