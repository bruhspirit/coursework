using Avalonia.Styling;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using static System.Net.Mime.MediaTypeNames;

namespace CompilerLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string condition = "Ожидание";
        private string lang = "rus";
        private string filename = "";

        private void CreateFileDialog(object sender, RoutedEventArgs e)
        {
            CloseFileWindow closeFileWindow = new CloseFileWindow();
            if (filename != "")
            {
                if (closeFileWindow.ShowDialog() == true)
                {
                    using (StreamWriter writer = new StreamWriter(filename, false))
                    {
                        writer.WriteLine(Input.Text);
                    }
                }
                else if (closeFileWindow.IsCanceled) { }
                else
                {
                    return;
                }
            }

            // Configure save file dialog box
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();




            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                filename = dialog.FileName;

                FileStream fs = File.Create(filename);

                if (!Input.IsEnabled)
                {
                    Input.IsEnabled = true;
                    Input.Text = "";
                }
                else
                {
                    Input.Text = "";
                }
                fs.Close();

            }
            SaveAsOption.IsEnabled = true;
            SaveButton.IsEnabled = true;
            SaveOption.IsEnabled = true;
            RunButton.IsEnabled = true;
            RunOption.IsEnabled = true;
            CloseFileOption.IsEnabled = true;
            EditOption.IsEnabled = true;
            CancelButton.IsEnabled = true;
            RepeatButton.IsEnabled = true;
            CopyButton.IsEnabled = true;
            CutButton.IsEnabled = true;
            PasteButton.IsEnabled = true;
            if (lang == "rus")
            {
                condition = "Редактирование";
                Condition.Content = condition;
            }
            if (lang == "eng")
            {
                condition = "Editing";
                Condition.Content = condition;
            }
            
            Input.Text = "";


        }
        private void SaveAsFileDialog(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                filename = dialog.FileName;

                FileStream fs = File.Create(filename);
                fs.Close();

                using (StreamWriter writer = new StreamWriter(filename, false))
                {
                    writer.WriteLine(Input.Text);
                }
                MessageBox.Show("Данные сохранены в " + filename);
            }
        }

        private void SaveFileDialog(object sender, RoutedEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(filename, false))
            {
                writer.WriteLine(Input.Text);
            }

            MessageBox.Show("Данные сохранены в " + filename);
        }

        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {
            CloseFileWindow closeFileWindow = new CloseFileWindow();
            if (filename != "")
            {
                if (closeFileWindow.ShowDialog() == true)
                {
                    using (StreamWriter writer = new StreamWriter(filename, false))
                    {
                        writer.WriteLine(Input.Text);
                        writer.Close();
                    }
                }
                else if (closeFileWindow.IsCanceled) {}
                else
                {
                    return;
                }
            }
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                filename = dialog.FileName;

                if (!Input.IsEnabled)
                {
                    Input.IsEnabled = true;
                    Input.Text = "";
                }

                using (StreamReader reader = new StreamReader(filename))
                {
                    string text = reader.ReadToEnd();
                    Input.Text = text;
                }

                SaveAsOption.IsEnabled = true;
                SaveButton.IsEnabled = true;
                SaveOption.IsEnabled = true;
                RunButton.IsEnabled = true;
                RunOption.IsEnabled = true;
                CloseFileOption.IsEnabled = true;
                EditOption.IsEnabled = true;
                CancelButton.IsEnabled = true;
                RepeatButton.IsEnabled = true;
                CopyButton.IsEnabled = true;
                CutButton.IsEnabled = true;
                PasteButton.IsEnabled = true;
                if (lang == "rus")
                {
                    condition = "Редактирование";
                    Condition.Content = condition;
                }
                if (lang == "eng")
                {
                    condition = "Editing";
                    Condition.Content = condition;
                }
            }
        }



        private void CloseFile(object sender, RoutedEventArgs e)
        {
            CloseFileWindow closeFileWindow = new CloseFileWindow();

            if (closeFileWindow.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(filename, false))
                {
                    writer.WriteLine(Input.Text);
                }
            }
            else if (closeFileWindow.IsCanceled) { return; }
            else
            {
                return;
            }
            Input.IsEnabled = false;
            SaveAsOption.IsEnabled = false;
            SaveButton.IsEnabled = false;
            SaveOption.IsEnabled = false;
            RunButton.IsEnabled = false;
            RunOption.IsEnabled = false;
            CloseFileOption.IsEnabled = false;
            EditOption.IsEnabled = false;
            CancelButton.IsEnabled = false;
            RepeatButton.IsEnabled = false;
            CopyButton.IsEnabled = false;
            CutButton.IsEnabled = false;
            PasteButton.IsEnabled = false;
            filename = "";
            if (lang == "rus")
            {
                condition = "Ожидание";
                Condition.Content = condition;
            }
            if (lang == "eng")
            {
                condition = "Waiting";
                Condition.Content = condition;
            }
        }
        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (lang == "rus")
            {
                condition = "Выход";
                Condition.Content = condition;
            }
            if (lang == "eng")
            {
                condition = "Exit";
                Condition.Content = condition;
            }

            if (filename != "")
            {
                CloseFileWindow closeFileWindow = new CloseFileWindow();

                if (closeFileWindow.ShowDialog() == true)
                {
                    using (StreamWriter writer = new StreamWriter(filename, false))
                    {
                        writer.WriteLine(Input.Text);
                    }
                    Process.GetCurrentProcess().Kill();
                }
                else if (closeFileWindow.IsCanceled) {
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    if (lang == "rus")
                    {
                        condition = "Редактирование";
                        Condition.Content = condition;
                    }
                    if (lang == "eng")
                    {
                        condition = "Editing";
                        Condition.Content = condition;
                    }
                    e.Cancel = true;
                    return;
                }         
            }
            else
            {
                Process.GetCurrentProcess().Kill();
            }
            
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            Input.Undo();
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            Input.Redo();
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            Input.Copy();
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            Input.Paste();
        }

        private void Cut(object sender, RoutedEventArgs e)
        {
            Input.Cut();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Input.Cut();
            Clipboard.Clear();
        }

        private void SelectAll(object sender, RoutedEventArgs e)
        {
            Input.SelectAll();
        }

        private void OutputFont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OutputFont.Text == "")
            {
                Output.FontSize = 14;
            }
            else
            {
                Output.FontSize = Convert.ToInt32(OutputFont.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", ""));
            }
            
        }
        private void InputFont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InputFont.Text == "")
            {
                Input.FontSize = 14;
            }
            else
            { 
                Input.FontSize = Convert.ToInt32(InputFont.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", ""));
            }
        }

        private void SwitchToRussian(object sender, RoutedEventArgs e)
        {
            string[] rus = {"Файл","Создать","Открыть","Сохранить","Сохранить как","Выход", "Правка","Отменить", "Повторить", 
                "Вырезать", "Копировать", "Вставить", "Удалить", "Выделить все", 
                "Текст", "Постановка задачи", "Грамматика", "Классификация грамматики",
                "Метод анализа", "Диагностика и нейтрализация ошибок", "Текстовый пример", "Список литературы", "Исходный код программы", "Справка"
                , "Вызов справки", "О программе", "Настройки", "Язык", "Пуск"};
            FileOption.Header = rus[0];
            CreateOption.Header = rus[1];
            OpenOption.Header = rus[2];
            SaveOption.Header = rus[3];
            SaveAsOption.Header = rus[4];
            CloseFileOption.Header = rus[5];
            EditOption.Header = rus[6];
            UndoOption.Header = rus[7];
            RedoOption.Header = rus[8];
            CutOption.Header = rus[9];
            CopyOption.Header = rus[10];
            PasteOption.Header = rus[11];
            DeleteOption.Header = rus[12];
            SelectAllOption.Header = rus[13];
            TextOption.Header = rus[14];
            Formulation.Header = rus[15];
            Grammatic.Header = rus[16];
            GrammaticClass.Header = rus[17];
            AnalysMethod.Header = rus[18];
            Troubleshooter.Header = rus[19];
            Example.Header = rus[20];
            Literature.Header = rus[21];
            SourceCode.Header = rus[22];
            Help.Header = rus[23];
            HelpOption.Header = rus[24];
            AboutProgram.Header = rus[25];
            Settings.Header = rus[26];
            Language.Header = rus[27];
            RunOption.Header = rus[28];
            CreateFileButton.ToolTip = "Создать файл";
            OpenFileButton.ToolTip = "Открыть файл";
            SaveButton.ToolTip = "Сохранить файл";
            CancelButton.ToolTip = "Отмена изменений";
            RepeatButton.ToolTip = "Повтор последнего изменения";
            CopyButton.ToolTip = "Копировать";
            CutButton.ToolTip = "Вырезать";
            PasteButton.ToolTip = "Вставить";
            RunButton.ToolTip = "Пуск";
            HelpButton.ToolTip = "Справка";
            AboutProgramButton.ToolTip = "О программе";
            InputFont.ToolTip = "Размер шрифта в окне редактирования";
            OutputFont.ToolTip = "Размер шрифта в окне вывода";
            
            lang = "rus";
            if (condition == "Waiting")
            {
                condition = "Ожидание";
                Condition.Content = condition;
            }
            if (condition == "Exit")
            {
                condition = "Выход";
                Condition.Content = condition;
            }
            if (condition == "Editing")
            {
                condition = "Редактирование";
                Condition.Content = condition;
            }
        }
        private void SwitchToEnglish(object sender, RoutedEventArgs e)
        {
            string[] eng = {"File","Create","Open","Save","Save as","Exit", "Edit","Undo", "Redo",
             "Cut", "Copy", "Paste", "Delete", "Select all",
             "Text", "Problem statement", "Grammar", "Grammar classification",
             "Analysis method", "Diagnosis and error neutralization", "Text example", "List of literature", "Source code of the program", "Help"
             , "Call for help", "About the program", "Settings", "Language", "Start"};
            FileOption.Header = eng[0];
            CreateOption.Header = eng[1];
            OpenOption.Header = eng[2];
            SaveOption.Header = eng[3];
            SaveAsOption.Header = eng[4];
            CloseFileOption.Header = eng[5];
            EditOption.Header = eng[6];
            UndoOption.Header = eng[7];
            RedoOption.Header = eng[8];
            CutOption.Header = eng[9];
            CopyOption.Header = eng[10];
            PasteOption.Header = eng[11];
            DeleteOption.Header = eng[12];
            SelectAllOption.Header = eng[13];
            TextOption.Header = eng[14];
            Formulation.Header = eng[15];
            Grammatic.Header = eng[16];
            GrammaticClass.Header = eng[17];
            AnalysMethod.Header = eng[18];
            Troubleshooter.Header = eng[19];
            Example.Header = eng[20];
            Literature.Header = eng[21];
            SourceCode.Header = eng[22];
            Help.Header = eng[23];
            HelpOption.Header = eng[24];
            AboutProgram.Header = eng[25];
            Settings.Header = eng[26];
            Language.Header = eng[27];
            RunOption.Header = eng[28];
            CreateFileButton.ToolTip = "Create file";
            OpenFileButton.ToolTip = "Open file";
            SaveButton.ToolTip = "Save file";
            CancelButton.ToolTip = "Undo";
            RepeatButton.ToolTip = "Redo";
            CopyButton.ToolTip = "Copy";
            CutButton.ToolTip = "Cut";
            PasteButton.ToolTip = "Paste";
            RunButton.ToolTip = "Run";
            HelpButton.ToolTip = "Help";
            AboutProgramButton.ToolTip = "About program";
            InputFont.ToolTip = "Editor font-size";
            OutputFont.ToolTip = "Output font-size";     
            lang = "eng";
            if (condition == "Ожидание")
            {
                condition = "Waiting";
                Condition.Content = condition;
            }
            if (condition == "Выход")
            {
                condition = "Exit";
                Condition.Content = condition;
            }
            if (condition == "Редактирование")
            {
                condition = "Editing";
                Condition.Content = condition;
            }
        }

        private void CallHelp(object sender, RoutedEventArgs e)
        {
            //Use no more than one assignment when you test this code.
            //string target = "ftp://ftp.microsoft.com";
            //string target = "C:\\Program Files\\Microsoft Visual Studio\\INSTALL.HTM";
            try
            {
                System.Diagnostics.Process.Start("C:\\Users\\myndc\\OneDrive\\Desktop\\123\\CompilerLab-main\\CompilerLab\\help.html");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }

        }

        private void About(object sender, RoutedEventArgs e)
        {
            //Use no more than one assignment when you test this code.
            //string target = "ftp://ftp.microsoft.com";
            //string target = "C:\\Program Files\\Microsoft Visual Studio\\INSTALL.HTM";
            try
            {
                System.Diagnostics.Process.Start("C:\\Users\\myndc\\OneDrive\\Desktop\\123\\CompilerLab-main\\CompilerLab\\About.html");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }

        }

        private void Run(object sender, RoutedEventArgs e)
        {
            Output.Items.Clear();
            int strings_counter = 0;
            List<string> strings = new List<string>();
            string word_buffer = "";
            for (int i = 0; i < Input.Text.Length; i++)
            {
                if (Input.Text[i] == '\n')
                {
                    strings_counter++;
                    CheckCodes(word_buffer, strings_counter, i-1, true);
                    word_buffer = "";                 
                    CheckCodes('\n' + "", strings_counter, i, true);           
                    continue;
                }
                if (((((i + 1) < Input.Text.Length)  && Input.Text[i + 1] == '\r') || i == Input.Text.Length - 1) && Input.Text[i] == '\n')
                {
                    word_buffer += Input.Text[i]; 
                    if (word_buffer != "")
                        CheckCodes(word_buffer, strings_counter, i, true);
                    word_buffer = "";
                    continue;
                }
                if (Input.Text[i] == '\r')
                    continue;

                if (i == Input.Text.Length - 1)
                {
                    CheckCodes(word_buffer, strings_counter, i, true);
                }

                if (Input.Text[i] == ' ')
                {
                    if (word_buffer != "")
                        CheckCodes(word_buffer, strings_counter, i-1, false);
                    CheckCodes(" ", strings_counter, i, false);
                    word_buffer = "";
                    continue;
                }
                else
                {         
                    word_buffer += Input.Text[i];
                }
            }
        }

        public class OutputItem
        {
            public string Code { get; set; }
            public string Type { get; set; }
            public string Lexem { get; set; }
            public string Symbol { get; set; }
            public string String { get; set; }
        }

        private bool IdCheck(string str)
        {
            bool isEnglish = true;

            foreach (char c in str)
            {
                if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9')))
                {
                    isEnglish = false;
                    break;
                }
            }
            return isEnglish;
        }
        private void CheckCodes(string word, int strings_count, int sym, bool end)
        {
            int code = 0;
            string type = "";
            string symbol = "";
            if (word == "")
                return;
            else if (word == "int")
            {
                code = 1;
                type = "Ключевое слово";
            }
            else if (word == "bool")
            {
                code = 2;
                type = "Ключевое слово";
            }
            else if (word == "char")
            {
                code = 3;
                type = "Ключевое слово";
            }
            else if (word == "string")
            {
                code = 4;
                type = "Ключевое слово";
            }  
            else if (word == "true")
            {
                code = 6;
                type = "Ключевое слово";
            }
            else if (word == "false")
            {
                code = 7;
                type = "Ключевое слово";
            }
            else if (word == " ")
            {
                code = 8;
                type = "Разделитель";
            }
            else if (word.Contains('\n') | (word.Length == 1 && (word[0] == '\n')))
            {
                code = 9;
                type = "Переход на новую строку";
            }
            else if (word[0] == '\r' && word.Length == 1)
                return;
            else if (word == "=")
            {
                code = 10;
                type = "Оператор присваивания";
            }
            else if (word == "[")
            {
                code = 11;
                type = "Объявление блока инициализации";
            }
            else if (word == "]")
            {
                code = 12;
                type = "Конец блока инициализации";
            }
            else if (Int32.TryParse(word, out int number))
            {
                code = 13;
                type = "Целое число";
            }
            else if (Double.TryParse(word, out double number2))
            {
                code = 14;
                type = "Вещественное число";
            }
            else if (word[0] == '\"' && word.Last() == '\"')
            {
                code = 15;
                type = "Строка";
            }
            else if (word[0] == '\'' && word.Last() == '\'' && word.Length == 3)
            {
                code = 16;
                type = "Символ";
            }
            else if (IdCheck(word))
            {
                code = 5;
                type = "Идентификатор";
            }
            else
            {
                code = 19;
                type = "Ошибка: недопустимые символы";
            }
            if (strings_count == 0)
                strings_count = 1;
            string f = "";
            if (word.Length > sym)
                symbol = "C 0 " + "до " + sym;
            else
                symbol = "C " + (sym - word.Length + 1) + " до" + (sym-1);
           
            if (code == 8 | code == 9 | code == 10 | code == 11 | code == 12)
                symbol = sym + "";
            Output.Items.Add(new OutputItem { Code = "" + code, Type = type, Lexem = word, Symbol = symbol, String = "" + strings_count });

        }
    }
}
