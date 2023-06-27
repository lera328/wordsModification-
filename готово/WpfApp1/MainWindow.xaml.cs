using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {//для тестов ночь — день, друг — враг, коза —волк и кот-пес.
        
       List <string> distionary;
        Helper a;
        public MainWindow()
        {
            InitializeComponent();
            tbStartWord.Focus();
            // Загружаем словарь
            var lines = new HashSet<string>(File.ReadAllLines(@"Distionary\freqrnc2011_s.csv"));
            distionary = new List<string>();
            foreach (var line in lines)
            {
                var word = line.Split(',')[0];
                distionary.Add(word);
            }
            

        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            //засекаем время начала операции
            stopwatch.Start();
            a = new Helper();
            var startWord = tbStartWord.Text.ToLower();//берем слова, все буквы в один регистр
            var endWord = tbEndWord.Text.ToLower();
            string wor = a.Valid(startWord, endWord);
            if (wor == null)
            {
                List<string> chain = a.Find(startWord, endWord, distionary);

                if (chain != null)
                {
                   // lbResult.Content = "РЕЗУЛЬТАТ";
                    tbResult.Text = string.Join(" -> ", chain);
                }
                else
                    tbResult.Text = "Цепочка не найдена";
            }
            else MessageBox.Show(wor);
            stopwatch.Stop();
            lbResult.Content = "РЕЗУЛЬТАТ (Время поиска "+Convert.ToDouble(stopwatch.ElapsedMilliseconds)/1000+" с)";
            tbStartWord.Clear();
            tbEndWord.Clear();
            tbStartWord.Focus();

        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Автор: Шульгина В.И. ПИН-221");
        }

        private void Instructions_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Инструкция:\n1. Введите слово-начало цепи мутации \n2. Введите слово-конец цепи\n3. Нажмите на кнопку и через некоторое время будет сгенерирована цепь");
        }

        private void TbStartWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) tbEndWord.Focus();
        }

        private void TbEndWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) btSearch_Click(sender,e);

        }

       

        private void TbStartWord_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            tbStartWord.Text="";

        }

        private void TbEndWord_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            tbEndWord.Text="";

        }
        //private void Exit_Click(object sender, RoutedEventArgs e)
        //{
        //    Application.Current.Shutdown();
        //}




    }
}
