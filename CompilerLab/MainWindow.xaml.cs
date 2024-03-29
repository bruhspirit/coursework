using Avalonia.Styling;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;

namespace CompilerLab
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            createTransitions();
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

            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Document";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
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
                else if (closeFileWindow.IsCanceled) { }
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
                else if (closeFileWindow.IsCanceled)
                {
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

        public class OutputItem
        {
            public string Code { get; set; }
            public string Type { get; set; }
            public string Lexem { get; set; }
            public string Symbol { get; set; }
            public string String { get; set; }
        }

        private void TextChangedEventHandler(object sender, TextChangedEventArgs args)
        {

            /* Output.Items.Clear();
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
                     strings_counter++;
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
                     strings_counter++;
                     word_buffer += Input.Text[i];
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
             }*/
        }

        private bool def(string s, int n)
        {
            string word = "";
            int code = 0;
            for (int j = 0; j < s.Length; j++)
            {
                if (word != "" && s[j] == ' ')
                {
                    code = 19;
                    Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: недопустимый символ", Lexem = word, Symbol = "" + j, String = "" + (n + 1) });
                    break;
                }
                else if (s[j] == '\r')
                {
                    continue;
                }
                else if (word == "" && s[j] == ' ')
                {
                    continue;
                }
                else if (s[j] == '=')
                {
                    return true;
                }
                else
                {
                    if (!((s[j] >= 'a' && s[j] <= 'z') || (s[j] >= 'A' && s[j] <= 'Z') || (s[j] == '_') || (s[j] == '-') || (s[j] >= '0' && s[j] <= '9' && (j != 0 || s[j - 1] != ' '))))
                        word += s[j];
                    else
                    {
                        code = 19;
                        Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: недопустимый символ", Lexem = word, Symbol = "" + j, String = "" + (n + 1) });
                        break;
                    }
                }
            }
            return false;
        }
        private bool assignment(string s, int n)
        {
            return false;
        }

        enum StatmentsEnum
        {
            DEF,
            LISTNAME,
            ASSIGNTMENT,
            ITEMS,
            NUMBER,
            NUMBERREM,
            DECIMAL,
            DECIMALREM,
            STRING
        }

        private Dictionary<string, Dictionary<string, string>> FillDictionaryWithLetters(Dictionary<string, Dictionary<string, string>> tmp, string stm, string next_stm)
        {
            for (int i = 0; i < 26; i++)
            {
                string s = (char)('a' + i) + "";
                tmp[stm].Add(s, next_stm);
            }
            for (int i = 0; i < 26; i++)
            {
                string s = (char)('A' + i) + "";
                tmp[stm].Add(s, next_stm);
            }
            string str = Convert.ToString('_');
            tmp[stm].Add(str, next_stm);
            return tmp;
        }

        private Dictionary<string, Dictionary<string, string>> FillDictionaryWithSymbols(Dictionary<string, Dictionary<string, string>> tmp, string stm, string next_stm)
        {
            for (int i = 0; i < 128; i++)
            {
                string s = (char)i + "";
                tmp[stm].Add(s, next_stm);
            }
            return tmp;
        }

        private Dictionary<string, Dictionary<string, string>> FillDictionaryWithNumbers(Dictionary<string, Dictionary<string, string>> tmp, string stm, string next_stm)
        {
            for (int i = 0; i < 10; i++)
            {
                string s = Convert.ToString(i);
                tmp[stm].Add(s, next_stm);
            }
            return tmp;
        }

        private int string_counter = 0;
        private int char_counter = 0;
        private string[] get_new_char()
        {
            string[] result = new string[3];
            return result;
        }

        string[] stm = {
            "DEF",
            "LISTNAME",
            "ASSIGNTMENT",
            "ITEMS",
            "NUMBER",
            "NUMBERREM",
            "DECIMAL",
            "DECIMALREM",
            "STRING",
            "END"
            };

        private Dictionary<string, Dictionary<string, string>> transitions = new Dictionary<string, Dictionary<string, string>>();

        private void createTransitions()
        {
            foreach (var s in stm)
            {
                transitions[s] = new Dictionary<string, string>();
            }

            transitions = FillDictionaryWithLetters(transitions, "DEF", "LISTNAME");
            transitions = FillDictionaryWithLetters(transitions, "LISTNAME", "LISTNAME");
            transitions = FillDictionaryWithNumbers(transitions, "LISTNAME", "LISTNAME");
            transitions["LISTNAME"].Add("=", "ASSIGNTMENT");
            transitions["ASSIGNTMENT"].Add("[", "ITEMS");
            transitions = FillDictionaryWithNumbers(transitions, "ITEMS", "NUMBERREM");
            transitions["ITEMS"].Add("+", "NUMBER");
            transitions["ITEMS"].Add("-", "NUMBER");
            transitions["ITEMS"].Add("]", "END");
            transitions["ITEMS"].Add("\"", "STRING");
            transitions = FillDictionaryWithNumbers(transitions, "NUMBER", "NUMBERREM");
            transitions["NUMBERREM"].Add(",", "ITEMS");
            transitions["NUMBERREM"].Add("]", "END");
            transitions["NUMBERREM"].Add(".", "DECIMAL");
            transitions = FillDictionaryWithNumbers(transitions, "NUMBERREM", "NUMBERREM");
            transitions = FillDictionaryWithNumbers(transitions, "DECIMAL", "DECIMALREM");
            transitions["DECIMALREM"].Add(",", "ITEMS");
            transitions["DECIMALREM"].Add("]", "END");
            transitions = FillDictionaryWithNumbers(transitions, "DECIMALREM", "DECIMALREM");
            transitions = FillDictionaryWithSymbols(transitions, "STRING", "STRING");
            transitions["STRING"].Add("\"]", "END");
            transitions["STRING"].Add("\",", "ITEMS");
        }

        /*1) DEF->letter LISTNAME
        2) LISTNAME->letter LISTNAME | = ASSIGNTMENT
        3) ASSIGNTMENT-> [ITEMS
        4) ITEMS-> [+| -] NUMBER | " STRING | ]
        5) NUMBER->digit NUMBERREM
        6) NUMBERREM-> , ITEMS | ] | digit NUMBERREM | .DECIMAL
        7) DECIMAL->digit DECIMALREM
        8) DECIMALREM-> , ITEMS | ] | digit DECIMALREM
        9) STRING-> "] | ", ITEMS | symbol STRING*/

        private int Lexer(string word)
        {
            int code = 0;
            string type = "";

            if (word == "int")
            {
                code = 1;
                type = "Ключевое слово";
                return code;
            }
            else if (word == "bool")
            {
                code = 2;
                type = "Ключевое слово";
                return code;
            }
            else if (word == "char")
            {
                code = 3;
                type = "Ключевое слово";
                return code;
            }
            else if (word == "string")
            {
                code = 4;
                type = "Ключевое слово";
                return code;
            }
            else if (word == "true")
            {
                code = 6;
                type = "Ключевое слово";
                return code;
            }
            else if (word == "false")
            {
                code = 7;
                type = "Ключевое слово";
                return code;
            }
            else if (word == " " || word == ";" || word == ",")
            {
                code = 8;
                type = "Разделитель";
                return code;
            }
            else if (word.Contains('\r') && word.Length == 1)
            {
                code = 9;
                type = "Переход на новую строку";
                return code;
            }
            else if (word == "=")
            {
                code = 10;
                type = "Оператор присваивания";
                return code;
            }
            else if (word == "[")
            {
                code = 11;
                type = "Объявление блока инициализации";
                return code;
            }
            else if (word == "]")
            {
                code = 12;
                type = "Конец блока инициализации";
                return code;
            }
            else if (Int32.TryParse(word, out int number))
            {
                code = 13;
                type = "Целое число";
                return code;
            }
            else if (Double.TryParse(word, out double number2))
            {
                code = 14;
                type = "Вещественное число";
                return code;
            }
            else if (word[0] == '\"' && word.Last() == '\"')
            {
                code = 15;
                type = "Строка";
                return code;
            }
            else if (IdCheck(word))
            {
                code = 5;
                type = "Идентификатор";
                return code;
            }
            else
            {
                code = 19;
                type = "Ошибка: недопустимые символы";
                return code;
            }
        }
        private bool IdCheck(string str)
        {
            bool isValidated = true;

            for (int c = 0; c < str.Length; c++)
            {
                if (c == 0)
                {
                    if (!((str[c] >= 'a' && str[c] <= 'z') || (str[c] >= 'A' && str[c] <= 'Z') || (str[c] == '_')))
                    {
                        isValidated = false;
                    }
                }
                else
                {
                    if (!((str[c] >= 'a' && str[c] <= 'z') || (str[c] >= 'A' && str[c] <= 'Z') || (str[c] >= '0' && str[c] <= '9') || (str[c] == '_')))
                    {
                        isValidated = false;
                    }
                }            
            }
            return isValidated;
        }
        private void Run(object sender, EventArgs e)
        {
            Output.Items.Clear();
            string[] allStrings = Input.Text.Split('\n');                    
            
            for (int i = 0; i < allStrings.Length; i++)
            {
                int char_index = 0;
                string lexem = "";
                string corrected_string = "";
                string current_state = "DEF";
                bool errors = false;
                while (true)
                {
                    if (Input.Text != "" && char_index < Input.Text.Length && char_index < allStrings[i].Length && allStrings[i] != "")
                    {
                        if (current_state == "DEF")
                        {
                            if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                            {
                                current_state = transitions[current_state][allStrings[i][char_index] + ""];                     
                                lexem = allStrings[i][char_index] + "";
                                corrected_string += lexem;
                                char_index++;
                                continue;
                            }
                            else if (allStrings[i][char_index] == ' ')
                            {
                                char_index++;
                                continue;
                            }
                            else if (allStrings[i][char_index] == '\r')
                            {
                                break;
                            } 
                            else
                            {
                                int start = char_index;
                                int end = char_index;
                                errors = true;
                                while (true)
                                {
                                    if (char_index < Input.Text.Length && char_index < allStrings[i].Length)
                                    {                                       
                                        if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                                        {
                                            current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                            break;
                                        }
                                        else
                                        {
                                            if (allStrings[i][char_index] == '\r')
                                                break;
                                            lexem += allStrings[i][char_index] + "";
                                            end = char_index;
                                            char_index++;                 
                                        }
                                    }
                                    else
                                        break;
                                }
                                if (start == end)
                                {
                                    Output.Items.Add(new OutputItem { Code = "19", Type = "Ошибка: недопустимый символ при начале объявления идентификатора", Lexem = lexem + "", Symbol = Convert.ToString(start), String = Convert.ToString(i + 1)});
                                }
                                else if (start < end)
                                {
                                    Output.Items.Add(new OutputItem { Code = "19", Type = "Ошибка: недопустимые символы при начале объявления идентификатора", Lexem = lexem + "", Symbol = Convert.ToString(start) + "-" + Convert.ToString(end), String = Convert.ToString(i + 1) });
                                }
                                continue;
                            }
                        }
                        if (current_state == "LISTNAME")
                        {
                            if (Input.Text != "" && char_index < Input.Text.Length && char_index < allStrings[i].Length && allStrings[i] != "")
                            {
                                if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                                {
                                    current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                    if (current_state == "ASSIGNTMENT")
                                    {
                                        string err = lexem;
                                        err = err.TrimEnd();
                                        //MessageBox.Show(corrected_string);
                                        int code = Lexer(corrected_string);
                                        if (code == 5)
                                        {
                                            corrected_string += allStrings[i][char_index];
                                            lexem = "";
                                            char_index++;
                                            continue;
                                        }
                                        else if (code == 1 || code == 2 || code == 3 || code == 4 || code == 6 || code == 7)
                                        {
                                            string err_lexem = lexem;
                                            corrected_string = corrected_string.Substring(0, corrected_string.Length - 1);
                                            Output.Items.Add(new OutputItem { Code = code + "", Type = "Ошибка: в качестве идентификатора использовано ключевое слово", Lexem = err_lexem, Symbol = Convert.ToString(char_index - (err_lexem.Length)) + "-" + Convert.ToString(char_index - 1), String = Convert.ToString(i + 1) });
                                            char_index++;
                                            continue;
                                        }
                                    }
                                    if (current_state == "LISTNAME")
                                    {

                                        lexem += allStrings[i][char_index];
                                        corrected_string += allStrings[i][char_index]; 
                                        if (char_index == allStrings[i].Length - 1)
                                        {
                                            lexem = lexem.TrimEnd();
                                            int code = Lexer(lexem);
                                            if (code == 1 || code == 2 || code == 3 || code == 4 || code == 6 || code == 7)
                                            {
                                                string err_lexem = lexem;
                                                int d = lexem.Length - 1;
                                                //corrected_string = corrected_string.Substring(0, corrected_string.Length - 1);
                                                Output.Items.Add(new OutputItem { Code = code + "", Type = "Ошибка: в качестве идентификатора использовано ключевое слово", Lexem = lexem, Symbol = Convert.ToString(char_index - (lexem.Length - 1)) + "-" + Convert.ToString(char_index), String = Convert.ToString(i + 1) });
                                            }
                                        }
                                        char_index++;
                                        continue;
                                    }                                  
                                }
                                else
                                {
                                    errors = true;
                                    List<int> numbers = new List<int>();
                                    string corrected_lexem = corrected_string;
                                    while (true)
                                    {
                                        if (char_index < Input.Text.Length && char_index < allStrings[i].Length)
                                        {
                                            if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                                            {
                                                if (allStrings[i][char_index] == '=' || char_index == allStrings[i].Length-1)
                                                {
                                                    if (allStrings[i][char_index] == '=')
                                                        current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                    break;
                                                }
                                                lexem += allStrings[i][char_index] + "";
                                                corrected_lexem += allStrings[i][char_index] + "";
                                                corrected_string = corrected_lexem;
                                                char_index++;
                                            }
                                            else if (allStrings[i][char_index] == ' ')
                                            {
                                                lexem = lexem.TrimEnd();
                                                int code = Lexer(corrected_string);
                                                if (code == 1 || code == 2 || code == 3 || code == 4 || code == 6 || code == 7)
                                                {
                                                    string err_lexem = lexem;
                                                    int d = lexem.Length - 1;
                                                    if (lexem.Contains(corrected_string))
                                                        Output.Items.Add(new OutputItem { Code = code + "", Type = "Ошибка: в качестве идентификатора использовано ключевое слово", Lexem = lexem, Symbol = Convert.ToString(char_index  - (corrected_string.Length)) + "-" + Convert.ToString(char_index - 1), String = Convert.ToString(i + 1) });
                                                    //corrected_string = corrected_string.Substring(0, corrected_string.Length - 1);
                                                    //MessageBox.Show(corrected_string);
                                                }
                                                if (code != 5)
                                                {
                                                    corrected_string = corrected_string.Substring(0, corrected_string.Length - 1);
                                                    //MessageBox.Show(corrected_string);                                          
                                                }
                                               
                                                int start = char_index;
                                                int end = char_index;
                                                while (true)
                                                {
                                                    if (char_index < Input.Text.Length && char_index < allStrings[i].Length)
                                                    {
                                                        if (allStrings[i][char_index] == '=')
                                                        {
                                                            break;
                                                        }
                                                        if (allStrings[i][char_index] == '\r')
                                                            break;
                                                        if (allStrings[i][char_index] == '\n')
                                                            break;
                                                        else if (allStrings[i][char_index] == ' ')
                                                        {
                                                            lexem += allStrings[i][char_index];
                                                            char_index++;                           
                                                            continue;
                                                        }
                                                        else
                                                        {
                                                            end = char_index;
                                                            lexem += allStrings[i][char_index];
                                                            char_index++;
                                                        }
                                                    }
                                                    else
                                                        break;                                                                                                
                                                }
                                                if (start != end)
                                                {
                                                    for (int x = start; x <= end; x++)
                                                    {
                                                        numbers.Add(x);
                                                    }
                                                }
                                            }
                                            else
                                            {                                                                                                   
                                                if (allStrings[i][char_index] == '\r')
                                                    break;
                                                lexem += allStrings[i][char_index] + "";
                                                numbers.Add(char_index);
                                                char_index++;
                                            }
                                        }
                                        else
                                            break;
                                    }
                                    
                                    if (numbers.Count > 0)
                                    {
                                        StringBuilder result = new StringBuilder();

                                        int startRange = numbers[0];
                                        int currentRange = numbers[0];

                                        for (int j = 1; j < numbers.Count; j++)
                                        {
                                            if (numbers[j] == currentRange + 1)
                                            {
                                                currentRange = numbers[j];
                                            }
                                            else
                                            {
                                                if (startRange != currentRange)
                                                {
                                                    result.Append(startRange).Append("-").Append(currentRange).Append(";");
                                                }
                                                else
                                                {
                                                    result.Append(currentRange).Append(";");
                                                }

                                                startRange = numbers[j];
                                                currentRange = numbers[j];
                                            }
                                        }

                                        if (startRange != currentRange)
                                        {
                                            result.Append(startRange).Append("-").Append(currentRange);
                                        }
                                        else
                                        {
                                            result.Append(currentRange);
                                        }

                                        Output.Items.Add(new OutputItem { Code = "19", Type = "Ошибка: неизвестные символы после объявления идентификатора", Lexem = lexem, Symbol = Convert.ToString(result), String = Convert.ToString(i + 1) });
                                        //MessageBox.Show(corrected_string);
                                        int code = Lexer(corrected_string);
                                        if (code != 5)
                                        {
                                            corrected_string = corrected_string.Substring(0, corrected_string.Length - 1);
                                            //MessageBox.Show(corrected_string);                                          
                                        }
                                        char_index++;
                                        continue;
                                    }

                                   

                                    if (char_index == allStrings[i].Length - 1)
                                    {                               
                                        lexem = lexem.TrimEnd();
                                        int code = Lexer(corrected_string);
                                        if (code == 1 || code == 2 || code == 3 || code == 4 || code == 6 || code == 7)
                                        {
                                            string err_lexem = lexem;
                                            int d = lexem.Length - 1;
                                            Output.Items.Add(new OutputItem { Code = code + "", Type = "Ошибка: в качестве идентификатора использовано ключевое слово", Lexem = lexem, Symbol = Convert.ToString(char_index - (corrected_string.Length)) + "-" + Convert.ToString(char_index - 1), String = Convert.ToString(i + 1) });
                                            corrected_string = corrected_string.Substring(0, corrected_string.Length - 1);
                                            //MessageBox.Show(corrected_string);
                                        }                                   
                                    }
                                    char_index++;
                                    continue;
                                }
                            }
                            else
                                break;
                               
                        }
                        if (current_state == "ASSIGNTMENT")
                        {
                            corrected_string += "=";
                            lexem = "";
                            
                            if (Input.Text != "" && char_index < Input.Text.Length && char_index < allStrings[i].Length && allStrings[i] != "")
                            {
                                if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                                {
                                    current_state = transitions[current_state][allStrings[i][char_index] + ""];                            
                                }
                                else
                                {
                                    errors = true;
                                    List<int> numbers = new List<int>();
                                    string corrected_lexem = corrected_string;
                                    while (true)
                                    {
                                        if (char_index < Input.Text.Length && char_index < allStrings[i].Length)
                                        {
                                            if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                                            {
                                                if (allStrings[i][char_index] == '[' || char_index == allStrings[i].Length - 1)
                                                {
                                                    if (allStrings[i][char_index] == '[')
                                                        current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                    break;
                                                }
                                                lexem += allStrings[i][char_index] + "";
                                                corrected_lexem += allStrings[i][char_index] + "";
                                                corrected_string = corrected_lexem;
                                                char_index++;
                                            }
                                            else if (allStrings[i][char_index] == ' ')
                                            {
                                                                                             
                                                int start = char_index;
                                                int end = char_index;
                                                while (true)
                                                {
                                                    if (char_index < Input.Text.Length && char_index < allStrings[i].Length)
                                                    {
                                                        if (allStrings[i][char_index] == '[')
                                                        {                                                     
                                                            break;
                                                        }
                                                        if (allStrings[i][char_index] == '\r')
                                                            break;
                                                        if (allStrings[i][char_index] == '\n')
                                                            break;
                                                        else if (allStrings[i][char_index] == ' ')
                                                        {
                                                            lexem += allStrings[i][char_index];
                                                            char_index++;
                                                            continue;
                                                        }
                                                        else
                                                        {
                                                            end = char_index;
                                                            lexem += allStrings[i][char_index];
                                                            char_index++;
                                                        }
                                                    }
                                                    else
                                                        break;
                                                }
                                                if (start != end)
                                                {
                                                    for (int x = start; x <= end; x++)
                                                    {
                                                        numbers.Add(x);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (allStrings[i][char_index] == '\r')
                                                    break;
                                                if (allStrings[i][char_index] == '\n')
                                                    break;
                                                
                                                lexem += allStrings[i][char_index] + "";
                                                numbers.Add(char_index);
                                                char_index++;
                                            }
                                        }
                                        else
                                            break;
                                    }

                                    if (numbers.Count > 0)
                                    {
                                        StringBuilder result = new StringBuilder();

                                        int startRange = numbers[0];
                                        int currentRange = numbers[0];

                                        for (int j = 1; j < numbers.Count; j++)
                                        {
                                            if (numbers[j] == currentRange + 1)
                                            {
                                                currentRange = numbers[j];
                                            }
                                            else
                                            {
                                                if (startRange != currentRange)
                                                {
                                                    result.Append(startRange).Append("-").Append(currentRange).Append(";");
                                                }
                                                else
                                                {
                                                    result.Append(currentRange).Append(";");
                                                }

                                                startRange = numbers[j];
                                                currentRange = numbers[j];
                                            }
                                        }

                                        if (startRange != currentRange)
                                        {
                                            result.Append(startRange).Append("-").Append(currentRange);
                                        }
                                        else
                                        {
                                            result.Append(currentRange);
                                        }

                                        Output.Items.Add(new OutputItem { Code = "19", Type = "Ошибка: неизвестные символы при инициализации списка", Lexem = lexem, Symbol = Convert.ToString(result), String = Convert.ToString(i + 1) });
                                        //MessageBox.Show(corrected_string);
                                        char_index++;
                                        continue;
                                    }
                                    char_index++;
                                    continue;

                                }
                            }
                        }
                        if (current_state == "ITEMS")
                        {
                            //MessageBox.Show("ITEMS");
                            if (!corrected_string.Contains('['))
                                corrected_string += "[";
                            lexem = "";

                            if (Input.Text != "" && char_index < Input.Text.Length && char_index < allStrings[i].Length && allStrings[i] != "")
                            {
                                if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                                {
                                    current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                    corrected_string += allStrings[i][char_index];
                                    char_index++;
                                }
                                else
                                {
                                    errors = true;
                                    List<int> numbers = new List<int>();
                                    string corrected_lexem = corrected_string;
                                    bool flag = false;
                                    while (true)
                                    {                                      
                                        if (char_index < Input.Text.Length && char_index < allStrings[i].Length && !flag)
                                        {
                                            if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                                            {
                                                if (allStrings[i][char_index] == ']' || char_index == allStrings[i].Length - 1)
                                                {
                                                    if (allStrings[i][char_index] == ']')
                                                        current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                    break;
                                                }
                                                
                                                lexem += allStrings[i][char_index] + "";
                                                corrected_lexem += allStrings[i][char_index] + "";
                                                corrected_string = corrected_lexem;
                                                char_index++;
                                            }
                                            else if (allStrings[i][char_index] == ' ')
                                            {

                                                int start = char_index;
                                                int end = char_index;
                                                while (true)
                                                {
                                                    if (char_index < Input.Text.Length && char_index < allStrings[i].Length)
                                                    {
                                                        if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                                                        {
                                                            current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                            corrected_string += allStrings[i][char_index];
                                                            flag = true;
                                                            break;
                                                        }
                                                        if (allStrings[i][char_index] == '\r')
                                                            break;
                                                        if (allStrings[i][char_index] == '\n')
                                                            break;
                                                        if (allStrings[i][char_index] == ']')
                                                        {
                                                            current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                            corrected_string += allStrings[i][char_index];
                                                            flag = true;
                                                            break;
                                                        }
                                                        if (allStrings[i][char_index] == '+')
                                                        {
                                                            current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                            corrected_string += allStrings[i][char_index];
                                                            flag = true;
                                                            break;
                                                        }

                                                        if (allStrings[i][char_index] == '-')
                                                        {
                                                            current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                            flag = true;
                                                            corrected_string += allStrings[i][char_index];
                                                            break;
                                                        }

                                                        if (allStrings[i][char_index] == '\"')
                                                        {
                                                            current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                            corrected_string += allStrings[i][char_index];
                                                            flag = true;
                                                            break;
                                                        }

                                                        else if (allStrings[i][char_index] == ' ')
                                                        {
                                                            lexem += allStrings[i][char_index];
                                                            char_index++;
                                                            continue;
                                                        }
                                                        else
                                                        {
                                                            end = char_index;
                                                            lexem += allStrings[i][char_index];
                                                            char_index++;
                                                        }
                                                    }
                                                    else
                                                        break;
                                                }
                                                if (start != end)
                                                {
                                                    for (int x = start; x <= end; x++)
                                                    {
                                                        numbers.Add(x);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (allStrings[i][char_index] == '\r')
                                                    break;
                                                if (allStrings[i][char_index] == '\n')
                                                    break;
                                                if (allStrings[i][char_index] == ']')
                                                {
                                                    current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                    break;
                                                }
                                                if (allStrings[i][char_index] == '[')
                                                {
                                                    break;
                                                }
                                                if (allStrings[i][char_index] == '+')
                                                {
                                                    current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                    break;
                                                }

                                                if (allStrings[i][char_index] == '-')
                                                {
                                                    current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                    break;
                                                }

                                                if (allStrings[i][char_index] == '\"')
                                                {
                                                    current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                    break;
                                                }
                                                lexem += allStrings[i][char_index] + "";
                                                numbers.Add(char_index);
                                                char_index++;
                                            }
                                        }
                                        else
                                            break;
                                    }

                                    if (numbers.Count > 0)
                                    {
                                        StringBuilder result = new StringBuilder();

                                        int startRange = numbers[0];
                                        int currentRange = numbers[0];

                                        for (int j = 1; j < numbers.Count; j++)
                                        {
                                            if (numbers[j] == currentRange + 1)
                                            {
                                                currentRange = numbers[j];
                                            }
                                            else
                                            {
                                                if (startRange != currentRange)
                                                {
                                                    result.Append(startRange).Append("-").Append(currentRange).Append(";");
                                                }
                                                else
                                                {
                                                    result.Append(currentRange).Append(";");
                                                }

                                                startRange = numbers[j];
                                                currentRange = numbers[j];
                                            }
                                        }

                                        if (startRange != currentRange)
                                        {
                                            result.Append(startRange).Append("-").Append(currentRange);
                                        }
                                        else
                                        {
                                            result.Append(currentRange);
                                        }
                                        lexem = lexem.Trim();
                                        Output.Items.Add(new OutputItem { Code = "19", Type = "Ошибка: неизвестные символы при объявлении элемента списка", Lexem = lexem, Symbol = Convert.ToString(result), String = Convert.ToString(i + 1) });
                                        //MessageBox.Show(corrected_string);
                                        char_index++;
                                        continue;
                                    }
                                    char_index++;
                                    continue;
                                }
                            }

                        }
                        if (current_state == "NUMBER")
                        {
                            if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                            {
                                current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                lexem = allStrings[i][char_index] + "";
                                corrected_string += lexem;
                                char_index++;
                                continue;
                            }
                            else if (allStrings[i][char_index] == ' ')
                            {
                                char_index++;
                                continue;
                            }
                            else if (allStrings[i][char_index] == '\r')
                            {
                                break;
                            }
                            else
                            {
                                errors = true;
                                int start = char_index;
                                int end = char_index;
                                while (true)
                                {
                                    if (char_index < Input.Text.Length && char_index < allStrings[i].Length)
                                    {
                                        if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                                        {
                                            current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                            corrected_string += allStrings[i][char_index];
                                            break;
                                        }
                                        else
                                        {
                                            if (allStrings[i][char_index] == '\r')
                                                break;
                                            lexem += allStrings[i][char_index] + "";
                                            end = char_index;
                                            char_index++;
                                        }
                                    }
                                    else
                                        break;
                                }
                                lexem = lexem.Trim();
                                if (start == end)
                                {
                                    Output.Items.Add(new OutputItem { Code = "19", Type = "Ошибка: недопустимый символ при начале объявления целого числа", Lexem = lexem + "", Symbol = Convert.ToString(start), String = Convert.ToString(i + 1) });
                                }
                                else if (start < end)
                                {
                                    Output.Items.Add(new OutputItem { Code = "19", Type = "Ошибка: недопустимые символы при начале объявления целого числа", Lexem = lexem + "", Symbol = Convert.ToString(start) + "-" + Convert.ToString(end), String = Convert.ToString(i + 1) });
                                }
                                //MessageBox.Show(corrected_string);
                                continue;
                            }
                        }
                        if (current_state == "NUMBERREM")
                        {
                            if (Input.Text != "" && char_index < Input.Text.Length && char_index < allStrings[i].Length && allStrings[i] != "")
                            {
                                if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                                {
                                    current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                    if (current_state == "DECIMAL")
                                    {
                                        corrected_string += allStrings[i][char_index];
                                        lexem = "";
                                        char_index++;
                                        continue;
                                    }
                                    if (current_state == "NUMBERREM")
                                    {
                                        lexem += allStrings[i][char_index];
                                        corrected_string += allStrings[i][char_index];
                                        char_index++;
                                        continue;
                                    }
                                    if (current_state == "ITEMS")
                                    {
                                        lexem += allStrings[i][char_index];
                                        corrected_string += allStrings[i][char_index];
                                        char_index++;
                                        continue;
                                    }
                                }
                                else
                                {
                                    errors = true;
                                    List<int> numbers = new List<int>();
                                    string corrected_lexem = corrected_string;
                                    while (true)
                                    {
                                        if (char_index < Input.Text.Length && char_index < allStrings[i].Length)
                                        {
                                            if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                                            {
                                                if (allStrings[i][char_index] == '.' || char_index == allStrings[i].Length - 1)
                                                {
                                                    if (allStrings[i][char_index] == '.')
                                                        current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                    break;
                                                }

                                                if (allStrings[i][char_index] == ',' || char_index == allStrings[i].Length - 1)
                                                {
                                                    if (allStrings[i][char_index] == ',')
                                                        current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                    break;
                                                }

                                                if (allStrings[i][char_index] == ']' || char_index == allStrings[i].Length - 1)
                                                {
                                                    if (allStrings[i][char_index] == ']')
                                                        current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                    break;
                                                }
                                                lexem += allStrings[i][char_index] + "";
                                                corrected_lexem += allStrings[i][char_index] + "";
                                                corrected_string = corrected_lexem;
                                                char_index++;
                                            }
                                            else if (allStrings[i][char_index] == ' ')
                                            {            
                                                int start = char_index;
                                                int end = char_index;
                                                while (true)
                                                {
                                                    if (char_index < Input.Text.Length && char_index < allStrings[i].Length)
                                                    {
                                                        if (allStrings[i][char_index] == ',')
                                                        {
                                                            break;
                                                        }
                                                        else if (allStrings[i][char_index] == ']')
                                                        {
                                                            break;
                                                        }
                                                        if (allStrings[i][char_index] == '\r')
                                                            break;
                                                        if (allStrings[i][char_index] == '\n')
                                                            break;
                                                        else if (allStrings[i][char_index] == ' ')
                                                        {
                                                            lexem += allStrings[i][char_index];
                                                            char_index++;
                                                            continue;
                                                        }
                                                        else
                                                        {
                                                            end = char_index;
                                                            lexem += allStrings[i][char_index];
                                                            char_index++;

                                                        }
                                                    }
                                                    else
                                                        break;
                                                }
                                                if (start != end)
                                                {
                                                    for (int x = start; x <= end; x++)
                                                    {
                                                        numbers.Add(x);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (allStrings[i][char_index] == '\r')
                                                    break;
                                                else if (allStrings[i][char_index] == ']')
                                                {
                                                    break;
                                                }
                                                lexem += allStrings[i][char_index] + "";
                                                numbers.Add(char_index);
                                                char_index++;
                                            }
                                        }
                                        else
                                            break;
                                    }

                                    if (numbers.Count > 0)
                                    {
                                        StringBuilder result = new StringBuilder();

                                        int startRange = numbers[0];
                                        int currentRange = numbers[0];

                                        for (int j = 1; j < numbers.Count; j++)
                                        {
                                            if (numbers[j] == currentRange + 1)
                                            {
                                                currentRange = numbers[j];
                                            }
                                            else
                                            {
                                                if (startRange != currentRange)
                                                {
                                                    result.Append(startRange).Append("-").Append(currentRange).Append(";");
                                                }
                                                else
                                                {
                                                    result.Append(currentRange).Append(";");
                                                }

                                                startRange = numbers[j];
                                                currentRange = numbers[j];
                                            }
                                        }

                                        if (startRange != currentRange)
                                        {
                                            result.Append(startRange).Append("-").Append(currentRange);
                                        }
                                        else
                                        {
                                            result.Append(currentRange);
                                        }

                                        Output.Items.Add(new OutputItem { Code = "19", Type = "Ошибка: неизвестные символы при записи целого числа", Lexem = lexem, Symbol = Convert.ToString(result), String = Convert.ToString(i + 1) });
                                        //MessageBox.Show(corrected_string);
                                        char_index++;
                                        continue;
                                    }
                                    char_index++;
                                    continue;
                                }
                            }
                            else
                                break;
                        }
                        if (current_state == "STRING")
                        {
                            if (transitions[current_state].ContainsKey(allStrings[i][char_index] + "") && allStrings[i][char_index] != '\"')
                            {
                                current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                lexem = allStrings[i][char_index] + "";
                                corrected_string += lexem;
                                char_index++;       
                                continue;
                            }
                            else if (transitions[current_state].ContainsKey(allStrings[i][char_index] + "") && allStrings[i][char_index] == '\"')
                            {
                                lexem = allStrings[i][char_index] + "";
                                if (char_index + 1 < allStrings[i].Length)
                                {
                                    if (allStrings[i][char_index + 1] == ']')
                                    {
                                        current_state = transitions[current_state]["\"]"];
                                        corrected_string += "\"";
                                        char_index++;
                                        continue;
                                    }
                                    if (allStrings[i][char_index + 1] == ',')
                                    {
                                        current_state = transitions[current_state]["\","];
                                        corrected_string += "\",";
                                        char_index++;
                                        char_index++;
                                        continue;
                                    }                                  
                                }
                                int count = allStrings[i].Count(c => c == '\"');
                                if (count > 2)
                                {
                                    errors = true;
                                    Output.Items.Add(new OutputItem { Code = "19", Type = "Ошибка: кавычки внутри кавычек", Lexem = lexem + "", Symbol = Convert.ToString(char_index), String = Convert.ToString(i + 1) });
                                    char_index++;
                                    continue;
                                }
                                if (count < 2)
                                {
                                    errors = true;
                                    Output.Items.Add(new OutputItem { Code = "19", Type = "Ошибка: нет закрывающей кавычки", Lexem = lexem + "", Symbol = Convert.ToString(char_index), String = Convert.ToString(i + 1) });
                                    char_index++;
                                    continue;
                                }
                            }
                        }
                        if (current_state == "END")
                        {
                            corrected_string += ']';
                            //MessageBox.Show(corrected_string);
                            break;
                        }
                        if (current_state == "DECIMAL")
                        {
                            if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                            {
                                current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                lexem = allStrings[i][char_index] + "";
                                corrected_string += lexem;
                                char_index++;
                                continue;
                            }
                            else if (allStrings[i][char_index] == '\r')
                            {
                                break;
                            }
                            else
                            {
                                errors = true;
                                int start = char_index;
                                int end = char_index;
                                while (true)
                                {
                                    if (char_index < Input.Text.Length && char_index < allStrings[i].Length)
                                    {
                                        if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                                        {
                                            current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                            corrected_string += allStrings[i][char_index];
                                            break;
                                        }
                                        else
                                        {
                                            if (allStrings[i][char_index] == '\r')
                                                break;
                                            lexem += allStrings[i][char_index] + "";
                                            end = char_index;
                                            char_index++;
                                        }
                                    }
                                    else
                                        break;
                                }
                                lexem = lexem.Trim();
                                if (start == end)
                                {
                                    string buf = "";
                                    if (lexem == " " || lexem == "")
                                        buf = "Разделитель(Пробел)";
                                    else
                                        buf = lexem;
                                    Output.Items.Add(new OutputItem { Code = "19", Type = "Ошибка: недопустимый символ при начале объявления числа с плавающей точкой", Lexem = buf + "", Symbol = Convert.ToString(start), String = Convert.ToString(i + 1) });
                                }
                                else if (start < end)
                                {
                                    string buf = "";
                                    if (lexem == " " || lexem == "")
                                        buf = "Разделитель(Пробел)";
                                    else
                                        buf = lexem;
                                    Output.Items.Add(new OutputItem { Code = "19", Type = "Ошибка: недопустимые символы при начале объявления числа с плавающей точкой", Lexem = buf + "", Symbol = Convert.ToString(start) + "-" + Convert.ToString(end), String = Convert.ToString(i + 1) });
                                }
                                //MessageBox.Show(corrected_string);
                                continue;
                            }
                        }
                        if (current_state == "DECIMALREM")
                        {
                            if (Input.Text != "" && char_index < Input.Text.Length && char_index < allStrings[i].Length && allStrings[i] != "")
                            {
                                if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                                {
                                    current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                    if (current_state == "DECIMALREM")
                                    {
                                        lexem += allStrings[i][char_index];
                                        corrected_string += allStrings[i][char_index];
                                        char_index++;
                                        continue;
                                    }
                                    if (current_state == "ITEMS")
                                    {
                                        lexem += allStrings[i][char_index];
                                        corrected_string += allStrings[i][char_index];
                                        char_index++;
                                        continue;
                                    }
                                }
                                else
                                {
                                    errors = true;
                                    List<int> numbers = new List<int>();
                                    string corrected_lexem = corrected_string;
                                    while (true)
                                    {
                                        if (char_index < Input.Text.Length && char_index < allStrings[i].Length)
                                        {
                                            if (transitions[current_state].ContainsKey(allStrings[i][char_index] + ""))
                                            {

                                                if (allStrings[i][char_index] == ']' || char_index == allStrings[i].Length - 1)
                                                {
                                                    if (allStrings[i][char_index] == ']')
                                                        current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                    break;
                                                }
                                                if (allStrings[i][char_index] == ',' || char_index == allStrings[i].Length - 1)
                                                {
                                                    if (allStrings[i][char_index] == ',')
                                                        current_state = transitions[current_state][allStrings[i][char_index] + ""];
                                                    break;
                                                }
                                                lexem += allStrings[i][char_index] + "";
                                                corrected_lexem += allStrings[i][char_index] + "";
                                                corrected_string = corrected_lexem;
                                                char_index++;
                                            }
                                            else if (allStrings[i][char_index] == ' ')
                                            {
                                                int start = char_index;
                                                int end = char_index;
                                                while (true)
                                                {
                                                    if (char_index < Input.Text.Length && char_index < allStrings[i].Length)
                                                    {
                                                        
                                                        if (allStrings[i][char_index] == ']')
                                                        {
                                                            break;
                                                        }
                                                        if (allStrings[i][char_index] == '\r')
                                                            break;
                                                        if (allStrings[i][char_index] == '\n')
                                                            break;
                                                        else if (allStrings[i][char_index] == ' ')
                                                        {
                                                            lexem += allStrings[i][char_index];
                                                            char_index++;
                                                            continue;
                                                        }
                                                        else
                                                        {
                                                            end = char_index;
                                                            lexem += allStrings[i][char_index];
                                                            char_index++;

                                                        }
                                                    }
                                                    else
                                                        break;
                                                }
                                                if (start != end)
                                                {
                                                    for (int x = start; x <= end; x++)
                                                    {
                                                        numbers.Add(x);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (allStrings[i][char_index] == '\r')
                                                    break;
                                                else if (allStrings[i][char_index] == ']')
                                                {
                                                    break;
                                                }
                                                lexem += allStrings[i][char_index] + "";
                                                numbers.Add(char_index);
                                                char_index++;
                                            }
                                        }
                                        else
                                            break;
                                    }

                                    if (numbers.Count > 0)
                                    {
                                        StringBuilder result = new StringBuilder();

                                        int startRange = numbers[0];
                                        int currentRange = numbers[0];

                                        for (int j = 1; j < numbers.Count; j++)
                                        {
                                            if (numbers[j] == currentRange + 1)
                                            {
                                                currentRange = numbers[j];
                                            }
                                            else
                                            {
                                                if (startRange != currentRange)
                                                {
                                                    result.Append(startRange).Append("-").Append(currentRange).Append(";");
                                                }
                                                else
                                                {
                                                    result.Append(currentRange).Append(";");
                                                }

                                                startRange = numbers[j];
                                                currentRange = numbers[j];
                                            }
                                        }

                                        if (startRange != currentRange)
                                        {
                                            result.Append(startRange).Append("-").Append(currentRange);
                                        }
                                        else
                                        {
                                            result.Append(currentRange);
                                        }

                                        Output.Items.Add(new OutputItem { Code = "19", Type = "Ошибка: неизвестные символы при записи числа с плавающей точкой", Lexem = lexem, Symbol = Convert.ToString(result), String = Convert.ToString(i + 1) });
                                        //MessageBox.Show(corrected_string);
                                        char_index++;
                                        continue;
                                    }
                                    char_index++;
                                    continue;
                                }
                            }
                            else
                                break;
                        }
                    }
                    else
                        break;
                }
                if (errors)
                {
                    Output.Items.Add(new OutputItem { Code = "", Type = "Предложено исправление:", Lexem = corrected_string, Symbol = "" , String = Convert.ToString(i + 1) });
                }


            }         
        }

      

       
        

        private void Grammatic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("C:\\Users\\myndc\\OneDrive\\Desktop\\coursework-master\\CompilerLab\\Grammar.html");
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

        private void GrammaticClass_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("C:\\Users\\myndc\\OneDrive\\Desktop\\coursework-master\\CompilerLab\\Classification.html");
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

        private void AnalysMethod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("C:\\Users\\myndc\\OneDrive\\Desktop\\coursework-master\\CompilerLab\\methodAnalysis.html");
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

        private void Formulation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("C:\\Users\\myndc\\OneDrive\\Desktop\\coursework-master\\CompilerLab\\problem.html");
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

        private void Troubleshooter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("C:\\Users\\myndc\\OneDrive\\Desktop\\coursework-master\\CompilerLab\\Neutralizing.html");
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

        private void Example_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("C:\\Users\\myndc\\OneDrive\\Desktop\\coursework-master\\CompilerLab\\testExamples.html");
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

        private void Literature_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("C:\\Users\\myndc\\OneDrive\\Desktop\\coursework-master\\CompilerLab\\LitList.html");
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

        private void SourceCode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/bruhspirit/coursework");
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
    }
}
