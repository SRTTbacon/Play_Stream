   M S B A M L   `   `   `  ���� [  WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms  F BPlay_Stream, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Play_StreamPlay_Stream     Play_Stream.Page.DX_Player   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     UwinForms@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Play_Stream     -     � WindowStartupLocation$  CenterScreen=� � Title$ Play_Stream��$��378.474��$��582.684�� � AllowsTransparency . ���	#50100064 � WindowStyle$
 None=� � 
ResizeMode$ CanResizeWithGrip=�.��� -   $
��grid��$��0,0,10,0q� � RowDefinitions �� �� $	��200 �.+�o� -    o�Name$
 menu�����	#90104076$
��Left=�$	��243��$��
186,95,0,0q�$	��Top=�$	��236��$��2��.@��� -    ��Name$ listView��
������ 
����� �$	��267��$	��225��$��	0,-22,0,0q� �� View �� .P��� �� -   	 ��Name$	 	textBlock�����	#90104076$
��Wrap=�$��20(�$	��243��$	��226���� -   $	 
textBlock6��$
��Left=�$��	5,127,0,0q�$
��Wrap=�$	��Top=�
�������$��22(�$��Kalam)�$
��Bold"�$��1���� $
�V1.0���� �� $�Created By SRTTbacon���� -   
 ��Name$
 Pitch_label��$��44100Hz��$
��Left=�$��
263,60,0,0q�$	��Top=�
�������$��20(�$	��100��$��35���� -    ��Name$ slider��$
��Left=�$��
174,42,0,0q�$	��Top=�$	��259�� �� TickFrequency$ 0Q� � SmallChange$ 0Q� � LargeChange$ 0Q��� -	   $
 label��$��	ピッチ��$��272,0,243,0q�$	��Top=�
������� J� RenderTransformOrigin$ 0.5,0.5&�$��20(�$
��Bold"�$��37���� -
   $ slider1��$
��Left=�$��
10,104,0,0q�$	��Top=�$	��171���� -   $
 label1��$��音量��$
��Left=�$��	71,70,0,0q�$	��Top=�
�������$ 0.5,0.5&�$��20(�$
��Bold"�$��50��$��34���� -    ��Name$ button��$��フォルダ選択��$
��Left=�$��441,104,0,0q�$	��Top=�$	��113��$ 0.479,0.559&�$��20(�$
��Bold"��� -   $ button1��$��ピッチを戻す��$
��Left=�$��246,165,0,0q�$	��Top=�$	��117��$��1��$��20(�$
��Bold"��� -   $
 label2��$��75��$
��Left=�$��
77,122,0,0q�$	��Top=�
�������$ 0.5,0.5&�$��20(�$
��Bold"�$��64��$��34���� -   $ slider2��$
��Left=�$��	10,10,0,0q�$��1��$	��Top=�$	��171���� -   $
 label4��$��位置��$
��Left=�$��
71,156,0,0q�$	��Top=�
�������$ 0.5,0.5&�$��20(�$
��Bold"�$��50��$��2��$��32���� -   $
 label5��$��0��$
��Left=�$��	71,28,0,0q�$	��Top=�
�������$ 0.5,0.5&�$��20(�$
��Bold"�$��78��$��1��$��34���� -   $ button2��$��スキップ��$
��Left=�$��441,147,0,0q�$	��Top=�$	��113��$��20(�$
��Bold"��� -    ��Name$ checkBox��$��CheckBox��$
��Left=�$��	20,96,0,0q�$��1��$	��Top=�$��19���� -   $
 Check_label��$��ランダム再生��$
��Left=�$��	39,84,0,0q�$��1��
�������$	��Top=�$��20(��� -   $ Back_B��$��戻る��$
��Left=�$��
20,159,0,0q�$��1��$	��Top=�$	��116��$��20(�$
��Bold"��� -   $ Close_B��$��終了��$
��Left=�$��441,160,0,0q�$��1��$	��Top=�$	��113��$��20(�$
��Bold"��� -   $ Minus_B��$	��-10��$
��Left=�$��
26,163,0,0q�$	��Top=�$��40��$��2��$��16(��� -   $ button3��$	��+10��$
��Left=�$��126,163,0,0q�$	��Top=�$��40��$ 0.5,0.5&�$��2��$��25��$��16(��� -   $ button4��$��ファイル選択��$
��Left=�$��
441,13,0,0q�$	��Top=�$	��113��$��31��$��20(�$
��Bold"��� -    ��Name$ textBox��$
��Left=�$��23��$��	29,18,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��120��$��16(��� -   $ Pitch_B��$��確定��$
��Left=�$��	46,42,0,0q�$	��Top=�$��75��$��18(�$
��Bold"��� -   $ button5��$��ファイルテスト��$
��Left=�$��
441,62,0,0q�$��1��$	��Top=�$	��113��$��49��$��20(�$
��Bold"��� -   $ Stop_B��$��停止��$
��Left=�$��	10,60,0,0q�$��1��$	��Top=�$��75��$��20(�$
��Bold"��� -   $ Play_B��$��再生��$
��Left=�$��	91,60,0,0q�$��1��$	��Top=�$��75��$��20(�$
��Bold"�