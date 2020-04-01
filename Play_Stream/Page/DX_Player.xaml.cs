using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DxLibDLL;
using System.Windows;
using System.Text;
using WMPLib;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace Play_Stream.Page
{
    public partial class DX_Player : Window
    {
        WindowsMediaPlayer Player = new WindowsMediaPlayer();
        List<string> _liststrFiles;         //音楽ファイル一覧
        int _nCurrentIndex = -1;
        Timer JK = new Timer();
        int SRTTbacon = 1919;
        bool OK_Play = false;
        public DX_Player()
        {
            string s = Environment.CommandLine;
            FileInfo fi = new FileInfo(s.Replace("\"", ""));
            string startup_path = fi.Directory.FullName;
            InitializeComponent();
            DX.SetAlwaysRunFlag(DX.TRUE);               // 非アクティブ時も処理続行
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);        // 描画先をバックバッファへ設定
            DX.SetUseFPUPreserveFlag(DX.TRUE);          // FPUの精度を落とさない
            DX.SetWaitVSyncFlag(DX.FALSE);              // VSync同期を無効
            DX.SetOutApplicationLogValidFlag(DX.FALSE); // ログ出力停止
            DX.SetDoubleStartValidFlag(DX.TRUE);        // 多重起動を許可
            DX.SetMouseDispFlag(DX.TRUE);
            DX.SetUseDXArchiveFlag(DX.TRUE);
            DX.SetUserWindow(Handle);
            DX.SetWindowVisibleFlag(DX.FALSE);
            if (DX.DxLib_Init() < 0)
            {
                System.Windows.MessageBox.Show("初期化エラー");
                return;
            }
            if (File.Exists(Path.GetTempPath() + "/SRTTbacon_Path.dat"))
            {

            }
            else
            {
                StreamWriter h = File.CreateText(Path.GetTempPath() + "/SRTTbacon_Path.dat");
                h.WriteLine("C:/Windows/Help");
                h.Close();
            }
            StreamReader st = new StreamReader(Path.GetTempPath() + "/SRTTbacon_Path.dat");
            string read = st.ReadLine();
            st.Close();
            _liststrFiles = EnumFiles(read);
            JK.Interval = 300;
            JK.Tick += delegate
            {
                if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
                {

                }
                else
                {
                    Focus();
                }
                if (SRTTbacon -1 == slider2.Value)
                {
                    if (checkBox.IsChecked == true)
                    {
                        Play();
                        OK();
                    }
                    else if (checkBox.IsChecked == false)
                    {

                    }
                }
                if (textBlock.Text.Contains("/TestMusic/BGM_01.mp3"))
                {
                    double ret1 = Math.Ceiling((double)DX.GetSoundTotalTime(SHandle) / 1000);
                    slider2.Maximum = (int)ret1 /2;
                }
                else if (textBlock.Text == "")
                {

                }
                else
                {
                    slider2.Maximum = (int)(Player.currentMedia.duration);
                    double ret1 = Math.Ceiling(Player.currentMedia.duration);
                    SRTTbacon = (int)ret1;
                }
            };
            JK.Start();
            slider.Maximum = 20000;
            slider.Value = 10000;
            slider.ValueChanged += delegate
            {
                if (slider.IsFocused)
                {
                    int GH = 54100;
                    DX.SetFrequencySoundMem(GH - (int)slider.Value, SHandle);
                    //約分(そのまま出力すると小数点が何個もついちゃうから)
                    double ret1 = Math.Ceiling(slider.Value);
                    Pitch_label.Content = GH - ret1 + "Hz";
                }
                else
                {
                    DX.PlaySoundMem(SHandle, DX.DX_PLAYTYPE_LOOP);
                }
            };
            slider.Minimum = 0;
            slider1.Minimum = 0;
            slider1.Maximum = 255;
            slider1.Value = 175;
            slider1.ValueChanged += delegate
            {
                DX.ChangeVolumeSoundMem((int)slider1.Value, SHandle);
                double ret1 = Math.Ceiling(slider1.Value);
                label2.Content = ret1;
            };
            slider2.Minimum = 0;
            slider2.ValueChanged += delegate
            {
                if (slider2.IsFocused)
                {
                    if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
                    {
                        OK_Play = false;
                        DX.StopSoundMem(SHandle);
                        DX.SetSoundCurrentTime((int)slider2.Value * 1000, SHandle);
                        OK_Play = true;
                    }
                }
                else
                {

                }
            };
            _timer.Interval = 100;
            _timer.Tick += delegate
            {
                Player.controls.pause();
                label5.Content = slider2.Value;
                slider2.Value = DX.GetSoundCurrentTime(SHandle) / 1000;
            };
            _timer.Start();
            Loop();
        }
        async void Loop()
        {
            while (true)
            {
                if (OK_Play == true)
                {
                    DX.PlaySoundMem(SHandle, DX.DX_PLAYTYPE_LOOP, DX.FALSE);
                    OK_Play = false;
                }
                await System.Threading.Tasks.Task.Delay(200);
            }
        }
        void BCBbacon()
        {
            Focus();
        }
        //ハンドルの指定(していないとマウスカーソルがどっか行く)
        public IntPtr Handle
        {
            get
            {
                var helper = new System.Windows.Interop.WindowInteropHelper(this);
                return helper.Handle;
            }
        }
        Timer _timer = new Timer();
        int SHandle;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ロード(起動)時に発生するイベント
            string s = Environment.CommandLine;
            FileInfo fi = new FileInfo(s.Replace("\"", ""));
            string startup_path = fi.Directory.FullName;
            if (File.Exists(Path.GetTempPath() + "/SRTTbacon_Path_02.dat"))
            {
                listView.Items.Clear();
                Num = 0;
                StreamReader str = new StreamReader(Path.GetTempPath() + "/SRTTbacon_Path_02.dat");
                string read = str.ReadLine();
                str.Close();
                if (read == "C:/Windows/Help")
                {

                }
                else
                {
                    DX.StopSoundMem(SHandle);
                    _liststrFiles = EnumFiles(read);      //再生したいmp3保存フォルダを指定
                    Play();
                    DX.ChangeVolumeSoundMem(190, SHandle);
                    while (true)
                    {
                        if (Num == _liststrFiles.Count)
                        {
                            OK();
                            break;
                        }
                        else
                        {
                            listView.Items.Add(_liststrFiles[Num]);
                            Num = Num + 1;
                        }
                    }
                }
            }
            else
            {
                listView.Items.Clear();
                textBlock.Text = "";
            }
        }
        int Num = 0;
        Encoding enc = Encoding.GetEncoding("Shift_JIS");
        private void button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                listView.Items.Clear();
                Num = 0;
                DX.StopSoundMem(SHandle);
                _liststrFiles = EnumFiles(f.SelectedPath);      //再生したいmp3保存フォルダを指定
                Play();
                StreamWriter stw = File.CreateText(Path.GetTempPath() + "/SRTTbacon_Path_02.dat");
                stw.WriteLine(f.SelectedPath);
                stw.Close();
                while (true)
                {
                    if (Num == _liststrFiles.Count)
                    {
                        OK();
                        break;
                    }
                    else
                    {
                        listView.Items.Add(_liststrFiles[Num]);
                        Num = Num + 1;
                    }
                }
            }
            else
            {

            }
        }
        int select_index = 0;

        void Play()
        {
            select_index = 0;
            if (_liststrFiles.Count == 0)
                return;
            _nCurrentIndex = GetNextIndex();
            DX.StopSoundMem(SHandle);
            DX.DeleteSoundMem(SHandle);
            SHandle = DX.LoadSoundMem(_liststrFiles[_nCurrentIndex], DX.DX_PLAYTYPE_LOOP);
            Player.URL = _liststrFiles[_nCurrentIndex];
            DX.PlaySoundMem(SHandle, DX.DX_PLAYTYPE_LOOP);
            textBlock.Text = _liststrFiles[_nCurrentIndex];
            slider2.Maximum = DX.GetSoundTotalTime(SHandle);
            double ret2 = Math.Ceiling(slider.Value);
            slider.Value = ret2 + 1;
            DX.SetFrequencySoundMem(54100 - (int)slider.Value, SHandle);
            double ret1 = Math.Ceiling(slider1.Value);
            slider1.Value = ret1 - 0.01;
            DX.ChangeVolumeSoundMem((int)slider1.Value, SHandle);
        }
        void OK()
        {
            while (true)
            {
                if (_liststrFiles[_nCurrentIndex] == listView.Items[select_index].ToString())
                {
                    listView.SelectedIndex = select_index;
                    listView.Focus();
                    break;
                }
                else
                {
                    select_index = select_index + 1;
                }
            }
        }
        int GetNextIndex()
        {
            if (_liststrFiles.Count == 0)
                return -1;

            int nIndex;
            Random rnd = new Random(Environment.TickCount);

            while (true)
            {
                //ランダムで曲を決定
                nIndex = rnd.Next(0, _liststrFiles.Count);

                break;
            }

            return nIndex;
        }
        List<string> EnumFiles(string strFolder)
        {
            List<string> ret = new List<string>();

            //指定フォルダ以下の全子フォルダから全ファイルを抜き出す
            IEnumerable<string> listFiles = Directory.EnumerateFiles(strFolder, "*.*", SearchOption.AllDirectories);

            foreach (string strFile in listFiles)
            {
                //見つかったファイルの拡張子を取り出す
                string strExt = Path.GetExtension(strFile).ToLower();
                if (strExt == "")
                    continue;
                if (strExt != ".mp3" && strExt != ".m4a" && strExt != ".wav" && strExt != ".aac" && strExt != ".flac" && strExt != ".mp4" && strExt != ".avi")
                    continue;

                ret.Add(strFile);
            }
            return ret;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Pitch_label.Content = "44100Hz";
            DX.SetFrequencySoundMem(44100, SHandle);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (textBlock.Text == "")
            {
                textBlock.Text = "フォルダが選択されていません。";
            }
            else if (textBlock.Text == "フォルダが選択されていません。")
            {
                textBlock.Text = "フォルダが選択されていません。";
            }
            else
            {
                Play();
                OK();
            }
        }

        private void Back_B_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter st = File.CreateText(Path.GetTempPath() + "/SRTTbacon_Path.dat");
            st.WriteLine("C:/Windows/Help");
            st.Close();
            MainWindow f = new MainWindow();
            f.Show();
            DX.StopSoundMem(SHandle);
            Close();
        }
        Timer _timer_End = new Timer();
        private void Close_B_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("終了しますか？", "確認", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _timer_End.Interval = 30;
                _timer_End.Tick += delegate
                {
                    double Opa = Opacity;
                    Opacity = Opa - 0.05;
                    if (Opacity <= 0)
                    {
                        _timer_End.Stop();
                        DX.StopSoundMem(SHandle);
                        DX.DxLib_End();
                        Close();
                    }
                };
                _timer_End.Start();
            }
            if (result == MessageBoxResult.No)
            {

            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Focus();
        }

        private void Minus_B_Click(object sender, RoutedEventArgs e)
        {
            double ret1 = Math.Ceiling((double)label5.Content);
            if (ret1 <= 10)
            {
                DX.StopSoundMem(SHandle);
                DX.SetSoundCurrentTime(0, SHandle);
                DX.PlaySoundMem(SHandle, DX.DX_PLAYTYPE_LOOP, DX.FALSE);
            }
            else
            {
                DX.StopSoundMem(SHandle);
                DX.SetSoundCurrentTime((int)ret1 * 1000 - 10000, SHandle);
                DX.PlaySoundMem(SHandle, DX.DX_PLAYTYPE_LOOP, DX.FALSE);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            DX.StopSoundMem(SHandle);
            DX.SetSoundCurrentTime(DX.GetSoundCurrentTime(SHandle) + 10000, SHandle);
            DX.PlaySoundMem(SHandle, DX.DX_PLAYTYPE_LOOP, DX.FALSE);
        }
        string path = (Path.GetTempPath() + "/Music_SRTTbacon.dat");
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Title = "再生ファイルを選択してください。";
            f.Multiselect = false;
            f.Filter = "再生ファイル(*.mp3;*.wav;*.flac;*.aac;*.m4a;*.mp4;*.avi)|*.mp3;*.wav;*.flac;*.aac;*.m4a;*.mp4;*.avi";
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DX.StopSoundMem(SHandle);
                DX.DeleteSoundMem(SHandle);
                SHandle = DX.LoadSoundMem(f.FileName, DX.DX_PLAYTYPE_LOOP);
                Player.URL = f.FileName;
                DX.PlaySoundMem(SHandle, DX.DX_PLAYTYPE_LOOP);
                textBlock.Text = f.FileName;
                slider2.Maximum = DX.GetSoundTotalTime(SHandle);
                double ret2 = Math.Ceiling(slider.Value);
                slider.Value = ret2 + 1;
                DX.SetFrequencySoundMem(54100 - (int)slider.Value, SHandle);
                double ret1 = Math.Ceiling(slider1.Value);
                slider1.Value = ret1 - 0.01;
                DX.ChangeVolumeSoundMem((int)slider1.Value, SHandle);
                listView.SelectedItem = null;
                if (File.Exists(path))
                {
                    StreamWriter g = new StreamWriter(path, false, enc);
                    g.WriteLine(f.FileName);
                    g.Close();
                }
                else
                {
                    StreamWriter sw = File.CreateText(path);
                    sw.Close();
                    {
                        StreamWriter g = new StreamWriter(path, false, enc);
                        g.WriteLine(f.FileName);
                        g.Close();
                    }
                }
            }
        }
        private void uxMynumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            if (textBox.Text.Length > 0 && e.Text == "-")
            {
                e.Handled = true;
                return;
            }
            var text = textBox.Text + e.Text;
            e.Handled = regex.IsMatch(text);
        }

        private void Pitch_B_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text == "")
            {
                System.Windows.MessageBox.Show("ピッチの数値を入力してください。\n通常44100Hz");
            }
            else
            {
                int Hz = int.Parse(textBox.Text);
                DX.SetFrequencySoundMem(Hz, SHandle);
                Pitch_label.Content = Hz.ToString() + "Hz";
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            string s = Environment.CommandLine;
            FileInfo fi = new FileInfo(s.Replace("\"", ""));
            string startup_path = fi.Directory.FullName;
            DX.SetDXArchiveKeyString("SRTTbacon_Code_C");
            DX.SetDXArchiveExtension("dat");
            DX.StopSoundMem(SHandle);
            SHandle = DX.LoadSoundMem(startup_path + "/TestMusic/BGM_01.mp3", DX.DX_PLAYTYPE_LOOP);
            DX.SetFrequencySoundMem(54100 - (int)slider.Value, SHandle);
            DX.PlaySoundMem(SHandle, DX.DX_PLAYTYPE_LOOP);
            textBlock.Text = startup_path + "/TestMusic/BGM_01.mp3";
            slider2.Maximum = DX.GetSoundTotalTime(SHandle);
            slider.Value = slider.Value + 1;
            double ret1 = Math.Ceiling(slider1.Value);
            slider1.Value = ret1;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void Stop_B_Click(object sender, RoutedEventArgs e)
        {
            DX.StopSoundMem(SHandle);
        }

        private void Play_B_Click(object sender, RoutedEventArgs e)
        {
            int Hz = int.Parse(Pitch_label.Content.ToString().Replace("Hz", ""));
            int Time_Now = int.Parse(label5.Content.ToString());
            DX.StopSoundMem(SHandle);
            DX.SetSoundCurrentTime(Time_Now * 1000, SHandle);
            DX.PlaySoundMem(SHandle, DX.DX_PLAYTYPE_LOOP, DX.FALSE);
        }
        Timer Timer_01 = new Timer();
        private void listView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                DX.StopSoundMem(SHandle);
                DX.DeleteSoundMem(SHandle);
                SHandle = DX.LoadSoundMem(listView.SelectedItem.ToString(), DX.DX_PLAYTYPE_LOOP);
                Player.URL = listView.SelectedItem.ToString();
                DX.PlaySoundMem(SHandle, DX.DX_PLAYTYPE_LOOP);
                textBlock.Text = listView.SelectedItem.ToString();
                slider2.Maximum = DX.GetSoundTotalTime(SHandle);
                double ret2 = Math.Ceiling(slider.Value);
                slider.Value = ret2 + 1;
                DX.SetFrequencySoundMem(54100 - (int)slider.Value, SHandle);
                double ret1 = Math.Ceiling(slider1.Value);
                slider1.Value = ret1 - 0.01;
                DX.ChangeVolumeSoundMem((int)slider1.Value, SHandle);
            }
        }
    }
}
