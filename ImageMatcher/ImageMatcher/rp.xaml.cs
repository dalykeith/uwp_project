using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ImageMatcher
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class rp : Page
    {
        public rp()
        {
            this.InitializeComponent();
            level();
        }



        public void level()
        {
            switch (App.level)
            {
                case 1:
                    ImageBrush brush1 = new ImageBrush();
                    //completed 
                    brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///Images/cooltext174981939091066.png", UriKind.Absolute));
                    image1.Source = brush1.ImageSource;
                    ImageBrush brush2 = new ImageBrush();
                    //moves taken
                    brush2.ImageSource = new BitmapImage(new Uri("ms-appx:///Images/cooltext174877648874930.png", UriKind.Absolute));
                    image2.Source = brush2.ImageSource;
                    textBlock.Text = App.easyLevel.ToString();

                    break;
                case 2:
                    if (App.gameOver == false)
                    {
                        ImageBrush brush3 = new ImageBrush();
                        //completed
                        brush3.ImageSource = new BitmapImage(new Uri("ms-appx:///Images/cooltext174981939091066.png", UriKind.Absolute));
                        image1.Source = brush3.ImageSource;
                        ImageBrush brush4 = new ImageBrush();
                        //moves left
                        brush4.ImageSource = new BitmapImage(new Uri("ms-appx:///Images/cooltext174907777842293.png", UriKind.Absolute));
                        image2.Source = brush4.ImageSource;
                        textBlock.Text = App.mediumLevel.ToString();
                    }
                    else if (App.gameOver == true)
                    {
                        ImageBrush brush5 = new ImageBrush();
                        //game over
                        brush5.ImageSource = new BitmapImage(new Uri("ms-appx:///Images/cooltext175048263066794.png", UriKind.Absolute));
                        image1.Source = brush5.ImageSource;
                    }

                    break;
                case 3:
                    if (App.gameOver == false)
                    {
                        ImageBrush brush6 = new ImageBrush();
                        //completed
                        brush6.ImageSource = new BitmapImage(new Uri("ms-appx:///Images/cooltext174981939091066.png", UriKind.Absolute));
                        image1.Source = brush6.ImageSource;
                        ImageBrush brush7 = new ImageBrush();
                        //time left
                        brush7.ImageSource = new BitmapImage(new Uri("ms-appx:///Images/cooltext175048491304424.png", UriKind.Absolute));
                        image2.Source = brush7.ImageSource;
                        textBlock.Text = App.hardLevel.ToString();
                    }
                    else if (App.gameOver == true)
                    {
                        ImageBrush brush8 = new ImageBrush();
                        //times up
                        brush8.ImageSource = new BitmapImage(new Uri("ms-appx:///Images/cooltext175048350622307.png", UriKind.Absolute));
                        image1.Source = brush8.ImageSource;
                    }

                    break;

            }



        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);
            App.gameOver = false;
        }
    }
}
