using System;
using System.IO;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab2_1_Kulazhin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Constructor with check if SquareRootCut_jpg.jpg was found.
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                // I've stolen it from StackOverflow.com.
                RootImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\SquareRootCut_jpg.jpg"));
            }
            catch (FileNotFoundException)
            {
                Lab2_1_Kulazhin.ErrorResources.LostRootImage notFound = new();
                notFound.Show();
            }
        }

        // Task #1
        // Calculate result of power-th root.
        private void GetResult_Button_Click(object sender, RoutedEventArgs e)
        {
            double rootedValue = 0.0;
            double power = 0.0;

            try
            {
                power = GetValue(Power);
                rootedValue = GetValue(PoweredValue);
                double accuracy = GetValue(Accuracy);
                CheckForOverflow(power, rootedValue, accuracy);

                double result = NewtonMethod.GetRoot(rootedValue, power, accuracy);
                Result.Text = result.ToString();
            }

            catch (FormatException exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            catch (ArgumentException exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            catch (OverflowException exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MathPowResult.Text = Math.Pow(rootedValue, 1.0 / power).ToString();
        }

        // Clear area.
        private void ClearArea_Click(object sender, RoutedEventArgs e)
        {
            Power.Text = string.Empty;
            PoweredValue.Text = string.Empty;
            Result.Text = string.Empty;
            Accuracy.Text = "set accuracy (i.e. 4)";
            MathPowResult.Text = "Math.Pow() result";
        }

        // To keep notes in Accuracy TextBox.
        private void Accuracy_GotFocus(object sender, RoutedEventArgs e)
        {
            Accuracy.Text = string.Empty;
            Accuracy.LostFocus += (sender, e) => { if (Accuracy.Text == string.Empty) { Accuracy.Text = "set accuracy (e.g. 4)"; } };
        }

        // Get data from text boxes.
        private static double GetValue(TextBox box)
        {
            double result = double.Parse(box.Text);

            if (box.Name == "Accuracy")
            {
                if (result < 0)
                    throw new FormatException("Accuracy must be greater than 0!");
            }

            return result;
        }

        // Check input data for overflow.
        private void CheckForOverflow(double power, double rootedValue, double accuracy)
        {
            if (power >= long.MaxValue || rootedValue >= long.MaxValue || accuracy >= long.MaxValue
                || power <= long.MinValue || rootedValue <= long.MinValue || accuracy <= long.MinValue)
                throw new OverflowException("Too big value!");
        }

        // Task #2
        private void InputDecimal_GotFocus(object sender, RoutedEventArgs e)
        {
            InputDecimal.Text = string.Empty;
            InputDecimal.LostFocus += (sender, e) => { if (InputDecimal.Text == string.Empty) { InputDecimal.Text = "Print decimal here"; } };
        }

        // Convert decimal to binary.
        private void GetInterpretation_Task2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int decimalValue = checked((int)GetValue(InputDecimal));
                OutputBinary.Text = BinaryConverter.ConvertToBinary(decimalValue);
            }

            catch (FormatException exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            catch (OverflowException exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        // Clear area.
        private void ClearArea_Task2_Click(object sender, RoutedEventArgs e)
        {
            InputDecimal.Text = "Print decimal here";
            OutputBinary.Text = "Binary interpretation";
        }
    }
}
