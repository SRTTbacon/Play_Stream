using System;
using System.IO;
using System.Windows;
using WMPLib;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace Play_Stream
{
    public partial class MainWindow : Window
    {
        WindowsMediaPlayer Player = new WindowsMediaPlayer();
        List<string> _liststrFiles;         //音楽ファイル一覧
        int _nCurrentIndex = -1;
        System.Windows.Forms.Timer mama = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer mama2 = new System.Windows.Forms.Timer();
        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists(Path.GetTempPath() + "/SRTTbacon_Path.dat"))
            {

            }
            else
            {
                StreamWriter h = File.CreateText(Path.GetTempPath() + "/SRTTbacon_Path.dat");
                h.WriteLine("C:/Windows/Help");
                h.Close();
            }
            MouseLeftButtonDown += (sender, e) => { DragMove(); };
            StreamReader st = new StreamReader(Path.GetTempPath() + "/SRTTbacon_Path.dat");
            string read = st.ReadLine();
            st.Close();
            _liststrFiles = EnumFiles(read);
            mama.Interval = 300;          //300msecごとに処理を実行
            mama.Tick += delegate
            {
                if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
                {

                }
                else
                {
                    button5.Focus();
                }
                if (slider1.Value == 0)
                {
                    slider1.Value = 1;
                }
                textBlock4.Text = Player.URL.ToString();
            };
            mama.Start();
            mama2.Interval = 500;
            mama2.Tick += delegate
            {
                double ret1 = Math.Ceiling(Player.controls.currentPosition);
                jk2 = (int)ret1;
                if (jk2 == jk - 1)
                {
                    if (checkBox.IsChecked == true)
                    {
                        Play();
                    }
                    else
                    {
                        Player.controls.stop();
                        Player.controls.currentPosition = 0;
                        Player.controls.play();
                    }
                }
            };
            mama2.Start();
            Play();
            Player.settings.volume = 50;
            slider.Minimum = 0;
            slider.Maximum = 100;           //音量はゼロから100
            slider.Value = 50;
            slider.Value = Player.settings.volume;
            slider.ValueChanged += delegate
            {
                Player.settings.volume = (int)slider.Value;
            };
            slider1.ValueChanged += delegate
            {
                if (slider1.IsFocused)
                {
                    Player.controls.pause();
                    Player.controls.currentPosition = slider1.Value / 100;
                }
                else
                {
                    Player.controls.play();
                }
            };
            slider1.Value = Player.controls.currentPosition;
            slider1.Minimum = 0;
            _timer.Interval = 100;          //50msecごとに処理を実行
            _timer.Tick += delegate
            {
                //以下を追加
                if (Player.playState == WMPPlayState.wmppsPlaying || Player.playState == WMPPlayState.wmppsPaused)
                {
                    slider1.Maximum = (int)(Player.controls.currentItem.duration * 100);
                    //シークバーにフォーカスがないときだけ再生位置を表示する
                    if (slider1.IsFocused == false)
                    {
                        try
                        {
                            //シークバーに再生位置を表示
                            //曲変更タイミングによっては例外出る可能性ある
                            slider1.Value = (int)(Player.controls.currentPosition * 100);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    if (textBlock.Text == "00:00")
                    {
                        slider1.Value = 0;
                    }
                    //再生時間の表示
                    textBlock.Text = Player.controls.currentPositionString;
                    textBlock1.Text = Player.currentMedia.durationString;
                    double ret1 = Math.Ceiling(Player.currentMedia.duration);
                    jk = (int)ret1;
                    textBlock2.Text = Player.settings.volume.ToString();
                    if (checkBox.IsChecked == true)
                    {
                        Player.settings.setMode("loop", false);
                    }
                    if (checkBox.IsChecked == false)
                    {
                        Player.settings.setMode("loop", true);
                    }
                }
            };
            _timer.Start();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("終了しますか？", "本当に？", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
            if (result == MessageBoxResult.No)
            {
                
            }
        }
        Encoding enc = Encoding.GetEncoding("Shift_JIS");
        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter aaaa = new StreamWriter(Path.GetTempPath() + "SRTTbacon_Path.dat", false, enc);
                aaaa.WriteLine(f.SelectedPath);
                aaaa.Close();
                _liststrFiles = EnumFiles(f.SelectedPath);      //再生したいmp3保存フォルダを指定
                //以下を変更
                Play();
            }
            else
            {

            }
        }
        int jk2 = 1919;
        int jk = 1919;
        void Play()
        {
            if (_liststrFiles.Count == 0)
                return;
            _nCurrentIndex = GetNextIndex();
            Player.URL = _liststrFiles[_nCurrentIndex];           //最初の再生曲をセット
            Player.controls.play();
            if (checkBox.IsChecked == true)
            {
                Player.settings.setMode("loop", false);
            }
            else if (checkBox.IsChecked == false)
            {
                Player.settings.setMode("loop", true);
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

                //同じ曲は連続で演奏しない
                if (nIndex == _nCurrentIndex)
                    continue;

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
                //見つかったファイルの拡張子を取り出し
                string strExt = Path.GetExtension(strFile).ToLower();
                if (strExt == "")
                    continue;
                if (strExt != ".mp3" && strExt != ".m4a" && strExt != ".wav" &&  strExt != ".aac" && strExt != ".flac" && strExt != ".mp4" && strExt != ".avi")
                    continue;

                ret.Add(strFile);
            }

            return ret;
        }
        
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            h = Player.controls.currentPosition;
            Player.controls.stop();
        }
        double h;
        private void button7_Click(object sender, RoutedEventArgs e)
        {
            Player.controls.stop();
            Player.controls.play();
            Player.controls.currentPosition = h;
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            Player.controls.currentPosition = Player.controls.currentPosition - 10;
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            Player.controls.currentPosition = Player.controls.currentPosition + 10;
        }
        string path = (Path.GetTempPath() + "/Music_SRTTbacon.dat");
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Title = "再生ファイルを選択してください。";
            f.Multiselect = false;
            f.Filter = "再生ファイル(*.mp3;*.wav;*.flac;*.aac;*.m4a;*.mp4;*.avi)|*.mp3;*.wav;*.flac;*.aac;*.m4a;*.mp4;*.avi";
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Player.URL = f.FileName;
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

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(path))
            {
                StreamReader gf = new StreamReader(path, enc);
                string magic = gf.ReadLine();
                gf.Close();
                {
                    Player.URL = magic;
                }
            }
            else
            {

            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Player.settings.volume = 100;
            Player.controls.stop();
            Page.DX_Player f = new Page.DX_Player();
            f.Show();
            Close();
            Player.controls.stop();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            Play();
        }
    }
}