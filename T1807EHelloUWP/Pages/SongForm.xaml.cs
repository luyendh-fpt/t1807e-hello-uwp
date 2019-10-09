using System;
using System.Collections.Generic;
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
    public sealed partial class SongForm : Page
    {
        private ISongService songService;
        //private Song currentSong;
        public SongForm()
        {
            this.InitializeComponent();
            //currentSong = new Song();
            songService = new SongService();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Song song = new Song()
            {
                name = Name.Text,
                singer =  Single.Text,
                thumbnail = Thumbnail.Text,
                link = Link.Text
            };
            Dictionary<String, String> errors = song.Validate();
            if (errors.Count == 0)
            {
                //songService.PostSongFree(song);

            }
            else
            {
                if (errors.ContainsKey("name"))
                {
                    NameMessage.Text = errors["name"];
                    NameMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    //NameMessage.Text = errors["name"];
                    NameMessage.Visibility = Visibility.Collapsed;
                }
                // pop up error message
            }
            //var song = new Song();
        }
    }

}
