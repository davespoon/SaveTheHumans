using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SaveTheHumans
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random _random = new Random();
        DispatcherTimer _enemyTimer = new DispatcherTimer();
        DispatcherTimer _targetTimer = new DispatcherTimer();
        private bool _humanIsCaptured = false;

        public MainWindow()
        {
            InitializeComponent();

            _enemyTimer.Tick += EnemyTimer_Tick;
            _enemyTimer.Interval = TimeSpan.FromSeconds(2);

            _targetTimer.Tick += TargetTimer_Tick;
            _targetTimer.Interval += TimeSpan.FromSeconds(0.1);
        }

        private void EnemyTimer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TargetTimer_Tick(object sender, EventArgs e)
        {
            ProgressBar.Value += 1;
            if (ProgressBar.Value >= ProgressBar.Maximum) EndTheGame();
        }

        private void EndTheGame()
        {
            if (!PlayArea.Children.Contains(GameOverText))
            {
                _enemyTimer.Stop();
                _targetTimer.Stop();
                _humanIsCaptured = false;
                StartButton.Visibility = Visibility.Visible;
                PlayArea.Children.Add(GameOverText);
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            AddEnemy();
        }

        private void AddEnemy()
        {
            ContentControl enemy = new ContentControl();
            enemy.Template = Resources["EnemyTemplate"] as ControlTemplate;
            AnimateEnemy(enemy, 0, PlayArea.ActualWidth - 100, "(Canvas.Left)");
            AnimateEnemy(enemy, _random.Next((int) PlayArea.ActualHeight - 100),
                _random.Next((int) PlayArea.ActualHeight - 100), "(Canvas.Top)");
            PlayArea.Children.Add(enemy);
        }

        private void AnimateEnemy(ContentControl enemy, double from, double to, string propertyToAnimate)
        {
            Storyboard storyboard = new Storyboard() {AutoReverse = true, RepeatBehavior = RepeatBehavior.Forever};
            DoubleAnimation animation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = new Duration(TimeSpan.FromSeconds(_random.Next(4, 6)))
            };
            Storyboard.SetTarget(animation, enemy);
            Storyboard.SetTargetProperty(animation, new PropertyPath(propertyToAnimate));
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }
    }
}