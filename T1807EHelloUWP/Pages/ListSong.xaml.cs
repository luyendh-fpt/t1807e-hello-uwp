using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using T1807EHelloUWP.Entity;
using T1807EHelloUWP.Service;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHelloUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListSong : Page
    {
        private ISongService songService;
        private bool _isPlaying;
        private int _currentIndex = 0;
        private ObservableCollection<Song> _songs;

        public ListSong()
        {
            this.InitializeComponent();
            songService = new SongService();
            LoadSongs();
        }

        private void LoadSongs()
        {
            _songs = songService.GetFreeSongs();
            ListViewSong.ItemsSource = _songs;
            _currentIndex = 0;
        }

        private void SelectSong(object sender, TappedRoutedEventArgs e)
        {
            var selectItem = sender as StackPanel;
            MyMediaPlayer.Pause();
            if (selectItem != null)
            {
                if (selectItem.Tag is Song currentSong)
                {
                    _currentIndex = _songs.IndexOf(currentSong); 
                    MyMediaPlayer.Source = new Uri(currentSong.link);
                    Play();
                }
            }
        }

        private void StatusButton_OnClick(object sender, RoutedEventArgs e)
        {
            
            if (_isPlaying)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        private void Play()
        {
            MyMediaPlayer.Source = new Uri(_songs[_currentIndex].link);
            ControlLabel.Text = "Now Playing: " + _songs[_currentIndex].name;
            ListViewSong.SelectedIndex = _currentIndex;
            MyMediaPlayer.Play();
            StatusButton.Icon = new SymbolIcon(Symbol.Pause);
            _isPlaying = true;
        }

        private void Pause()
        {
            ControlLabel.Text = "Pause";
            MyMediaPlayer.Pause();
            StatusButton.Icon = new SymbolIcon(Symbol.Play);
            _isPlaying = false;
        }

        private void PreviousButton_OnClick(object sender, RoutedEventArgs e)
        {
            _currentIndex--;
            if (_currentIndex < 0)
            {
                _currentIndex = _songs.Count - 1;
            } else if (_currentIndex >= _songs.Count)
            {
                _currentIndex = 0;
            }
            Play();
        }

        private void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            _currentIndex++;
            if (_currentIndex >= _songs.Count || _currentIndex < 0)
            {
                _currentIndex = 0;
            }
            Play();
        }
    }
}
