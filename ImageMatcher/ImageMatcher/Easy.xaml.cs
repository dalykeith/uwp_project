using System;
using System.Collections.Generic;
using System.Threading;
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
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ImageMatcher
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Easy : Page
    {
        //varibles
        public Random rnd = new Random();
        public Boolean firstClick = true;
        public string img1, img2, butName;
        public int moves = 15;
        public int eScore = 0;
        public int Score = 0;
        public TextBox txtBox;
        public int time = 31;
        public DispatcherTimer Timer;


        //two list used to know which picture has been clicked first
        List<Button> butList = new List<Button>()
        {
        };
        List<Image> imgList = new List<Image>()
        {
        };

        //Main Method 
        public Easy()
        {
            this.InitializeComponent();
            score.Visibility = Visibility.Collapsed;
            eScoreTxt.Visibility = Visibility.Collapsed;
            randomPic();
            level();



        }
        //Countdown timer used on the hard level of the game 
        private void countdownTimer(object sender, object e)
        {

            if (time > 0)
            {
                if (time <= 10)
                {
                    time--;
                    countdown.Text = string.Format("{1}", time / 60, time % 60);
                }
                else
                {
                    time--;
                    countdown.Text = string.Format("{1}", time / 60, time % 60);
                }
            }
            else
            {
                Timer.Stop();

            }


        }


        //to determind which screen to open and which leve is being played 
        public void level()
        {
            //switch statment switches on the which buton is pressed in the main page
            //level is a global vairble saved in the App.xaml.cs
            switch (App.level)
            {
                case 1:
                    //makes the text block visible when called 
                    eScoreTxt.Visibility = Visibility.Visible;
                    //adds text to the text block 
                    eScoreTxt.Text = eScore.ToString();
                    //creates and adds an image 
                    ImageBrush brush1 = new ImageBrush();
                    brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///Images/cooltext174877648874930.png", UriKind.Absolute));
                    image12.Source = brush1.ImageSource;
                    break;
                case 2:
                    score.Visibility = Visibility.Visible;
                    score.Text = moves.ToString();
                    ImageBrush brush2 = new ImageBrush();
                    brush2.ImageSource = new BitmapImage(new Uri("ms-appx:///Images/cooltext174907777842293.png", UriKind.Absolute));
                    image13.Source = brush2.ImageSource;
                    break;
                case 3:
                    //creates and starts the timer 
                    Timer = new DispatcherTimer();
                    Timer.Interval = new TimeSpan(0, 0, 1);
                    Timer.Tick += countdownTimer;
                    Timer.Start();
                    ImageBrush brush3 = new ImageBrush();
                    brush3.ImageSource = new BitmapImage(new Uri("ms-appx:///Images/cooltext175048491304424.png", UriKind.Absolute));
                    image14.Source = brush3.ImageSource;

                    break;

            }



        }
        //game over method used to determine if youve won or lost the game 
        public async void gameOver()
        {
            //sets the rules depending on which level youve picked
            switch (App.level)
            {
                case 1:
                    //once score hits 6 means youve won 
                    if (Score == 6)
                    {
                        //await is used so that the screen doesnt change to quikly and the player cant see the screen 
                        await Task.Delay(500);
                        App.easyLevel = eScore;
                        this.Frame.Navigate(typeof(rp), null);
                    }
                    break;
                case 2:
                    if (Score == 6)
                    {
                        await Task.Delay(500);
                        App.mediumLevel = moves;
                        this.Frame.Navigate(typeof(rp), null);
                    }
                    //if youve no more moves left youve lost so its changes the Game over varible in app.xaml to true 
                    if (moves == 0)
                    {
                        await Task.Delay(500);
                        App.gameOver = true;
                        this.Frame.Navigate(typeof(rp), null);
                    }

                    break;
                case 3:
                    if (Score == 6)
                    {
                        await Task.Delay(500);
                        App.hardLevel = time;
                        this.Frame.Navigate(typeof(rp), null);
                    }
                    if (time == 0)
                    {
                        App.gameOver = true;
                        this.Frame.Navigate(typeof(rp), null);
                    }

                    break;

            }
        }
        // This is the main gameplay method used in the game 
        public async void gameplay(Button but, Image img)
        {
            //once this method is called means a button has been pressed the button then get collapssed and the image is made visible 
            img.Visibility = Visibility.Visible;
            but.Visibility = Visibility.Collapsed;
            //if its the first clicked then there is no need to do any check for a similar image 
            if (firstClick)
            {
                // the button the image are both added to list so that when you click the next image it will remember where you clicked 
                img1 = ((BitmapImage)img.Source).UriSource.ToString();
                butList.Add(but);
                imgList.Add(img);
                firstClick = false;
            }
            else
            {
                //get the second image clicked
                img2 = ((BitmapImage)img.Source).UriSource.ToString();
                //checks to see if both images are the same 
                if (img1 == img2)
                {
                    //disables the rest of the buttons so that you cant have more than two buttons clicked 
                    disableButtons();
                    img.Visibility = Visibility.Visible;
                    but.Visibility = Visibility.Collapsed;
                    butList[0].Visibility = Visibility.Collapsed;
                    imgList[0].Visibility = Visibility.Visible;
                    //removes the button and image from there lists so that the next one can be added
                    imgList.RemoveAt(0);
                    butList.RemoveAt(0);
                    firstClick = true;
                    moves--;
                    eScore++;
                    eScoreTxt.Text = eScore.ToString();
                    score.Text = moves.ToString();
                    Score++;
                    //calls game over method to check if the game over credentials have been met 
                    gameOver();
                    //enables buttons for next moves 
                    enableButtons();


                }

                else
                {
                    disableButtons();
                    await Task.Delay(1000);
                    img.Visibility = Visibility.Collapsed;
                    but.Visibility = Visibility.Visible;
                    butList[0].Visibility = Visibility.Visible;
                    imgList[0].Visibility = Visibility.Collapsed;
                    butList.RemoveAt(0);
                    imgList.RemoveAt(0);
                    firstClick = true;
                    moves--;
                    eScore++;
                    eScoreTxt.Text = eScore.ToString();
                    score.Text = moves.ToString();
                    enableButtons();
                    gameOver();

                }

            }

        }

        public void randomPic()
        {
            List<string> pics1 = new List<string>()
            {
                "ms-appx:///Images/ball.png",
                "ms-appx:///Images/bat.png",
                "ms-appx:///Images/card.png",
                "ms-appx:///Images/controller.png",
                "ms-appx:///Images/dice.png",
                "ms-appx:///Images/paper.png",

                "ms-appx:///Images/ball.png",
                "ms-appx:///Images/bat.png",
                "ms-appx:///Images/card.png",
                "ms-appx:///Images/controller.png",
                "ms-appx:///Images/dice.png",
                "ms-appx:///Images/paper.png"

            };

            List<Image> imgs = new List<Image>()
            {
                image0, image1, image2, image3, image4,
                image5, image6, image7, image8, image9,
                image10, image11
            };
            for (int i = 11; i >= 0; i--)
            {

                int randomNumber = rnd.Next(0, pics1.Count);

                ImageBrush brush1 = new ImageBrush();
                brush1.ImageSource = new BitmapImage(new Uri(pics1[randomNumber], UriKind.Absolute));

                imgs[i].Source = brush1.ImageSource;
                pics1.RemoveAt(randomNumber);
                imgs[i].Visibility = Visibility.Collapsed;
            }
        }

        public void disableButtons()
        {
            List<Button> buttons = new List<Button>()
            {
                button, button1, button2, button3, button4,
                button5, button6, button7, button8, button9,
                button10, button11
            };

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].IsEnabled = false;

            }
        }

        public void enableButtons()
        {
            List<Button> buttons = new List<Button>()
            {
                button, button1, button2, button3, button4,
                button5, button6, button7, button8, button9,
                button10, button11
            };

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].IsEnabled = true;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            gameplay(button, image0);

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            gameplay(button1, image1);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            gameplay(button4, image4);

        }



        private void button2_Click(object sender, RoutedEventArgs e)
        {
            gameplay(button2, image2);

        }


        private void button3_Click(object sender, RoutedEventArgs e)
        {
            gameplay(button3, image3);

        }



        private void button5_Click(object sender, RoutedEventArgs e)
        {
            gameplay(button5, image5);

        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            gameplay(button6, image6);
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            gameplay(button7, image7);
        }
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            gameplay(button8, image8);


        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            gameplay(button9, image9);


        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            gameplay(button10, image10);

        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            gameplay(button11, image11);

        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);
        }






    }
}
