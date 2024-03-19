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
            string str = Convert.ToString('-');
            tmp[stm].Add(str, next_stm);
            str = Convert.ToString('_');
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
            transitions["ITEMS"].Add("+", "NUMBER");
            transitions["ITEMS"].Add("-", "NUMBER");
            transitions["ITEMS"].Add("", "NUMBER");
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

        private string current_word = "";
        private void Input_TextChanged(object sender, EventArgs e)
        {
            string current_state = "DEF";
            bool g = true;
            Output.Items.Clear();
            string[] tmp = Input.Text.Split('\n');
            var splitted_by_char_strings = new List<List<char>>();
            string lexem = "";
            for (int i = 0; i < tmp.Length; i++)
            {
                splitted_by_char_strings.Add(new List<char>());
                string s = tmp[i];
                foreach (char c in s)
                {
                    splitted_by_char_strings[i].Add(c);
                }
            }
            int current_string = 1;
            int current_char = 0;
            string pr_lexem = "";
            bool nmr = false;
            while (current_state != "END")
            {
                List<char> chars_in_string = new List<char>();
                chars_in_string = splitted_by_char_strings[current_string - 1];

                if (current_state == "DEF")
                {
                    if (chars_in_string.Count > 0 && current_char < chars_in_string.Count)
                    {
                        if (chars_in_string[current_char] == ' ')
                        {
                            current_char++;
                            continue;
                        }
                        if (transitions[current_state].ContainsKey(chars_in_string[current_char] + ""))
                        {
                            current_state = transitions[current_state][chars_in_string[current_char] + ""];
                            if (current_char < chars_in_string.Count)
                                current_char++;
                        }
                        else
                        {
                            //ERROR
                            int code = 19;
                            string tmp_str = tmp[current_string - 1];
                            int items_count = Output.Items.Count;
                            Output.Items.Clear();
                            lexem = "";
                            string symbol = "";
                            int start = 0;

                            for (int i = 0; i <= current_char; i++)
                            {

                                if (tmp_str[i] == ' ' && lexem == "")
                                {
                                    start++;
                                    continue;
                                }

                                if (tmp_str[i] != '\r' && tmp_str[i] != '\n')
                                    lexem += tmp_str[i];
                            }
                            if (lexem != "")
                            {
                                if (tmp_str[current_char] != '\r' && tmp_str[current_char] != '\n')
                                {
                                    if (lexem.Length > 1)
                                        Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: недопустимые символы при начале объявления идентификатора", Lexem = lexem + "", Symbol = Convert.ToString(start) + "-" + Convert.ToString(current_char), String = "" + current_string });
                                    else
                                        Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: недопустимые символ при начале объявления идентификатора", Lexem = lexem + "", Symbol = current_char + "", String = "" + current_string });
                                }
                                else
                                {
                                    if (lexem.Length > 1)
                                        Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: недопустимые символы при начале объявления идентификатора", Lexem = lexem + "", Symbol = Convert.ToString(start) + "-" + Convert.ToString(current_char - 1), String = "" + current_string });
                                    else
                                        Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: недопустимые символ при начале объявления идентификатора", Lexem = lexem + "", Symbol = Convert.ToString(current_char) + "", String = "" + current_string });
                                }
                                if (tmp_str[current_char] == '=')
                                {
                                    Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: перед знаком равенства ожидался идентификатор", Lexem = lexem + "", Symbol = Convert.ToString(current_char), String = "" + current_string });
                                    current_state = "ASSIGNTMENT";
                                    lexem = "";
                                    continue;
                                }
                            }

                            lexem = "";
                            current_char++;
                            continue;
                        }
                    }
                    else
                        break;
                }
                if (current_state == "LISTNAME")
                {
                    if (chars_in_string.Count > 0 && current_char < chars_in_string.Count)
                    {

                        if (transitions[current_state].ContainsKey(chars_in_string[current_char] + ""))
                        {
                            if (chars_in_string[current_char] == '=')
                            {
                                current_state = transitions[current_state][chars_in_string[current_char] + ""];
                                if (current_char < chars_in_string.Count)
                                    current_char++;
                                pr_lexem = "";
                                continue;

                            }
                            else
                            {
                                current_char++;
                            }
                        }
                        else
                        {
                            int code = 19;
                            string tmp_str = tmp[current_string - 1];
                            string sym = "";
                            lexem = "";


                            if (tmp_str[current_char] != '\r' && tmp_str[current_char] != '\n')
                            {

                                for (int i = 0; i < tmp_str.Length; i++)
                                {
                                    if (tmp_str[i] == '=')
                                    {
                                        lexem += tmp_str[i];
                                        break;
                                    }
                                    if (lexem == "" && tmp_str[i] == ' ')
                                        continue;
                                    if (lexem != "" && tmp_str[i] == ' ')
                                        continue;
                                    if (transitions[current_state].ContainsKey(tmp_str[i] + "") && tmp_str[i] != ' ' && tmp_str[i] != '\r' && tmp_str[i] != '\n')
                                    {
                                        lexem += tmp_str[i];
                                        continue;
                                    }
                                    else
                                    {
                                        if (tmp_str[i] != '\r' && tmp_str[i] != '\n')
                                        {
                                            sym += Convert.ToString(i) + ';';
                                        }
                                    }
                                    if (tmp_str[i] != '\r' && tmp_str[i] != '\n')
                                    {
                                        lexem += tmp_str[i];
                                    }

                                }

                                StringBuilder output = new StringBuilder();

                                string[] numbers = sym.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                                if (numbers.Length > 0)
                                {
                                    int startRange = Convert.ToInt32(numbers[0]);
                                    int endRange = startRange;

                                    for (int i = 1; i < numbers.Length; i++)
                                    {
                                        int currentNumber = Convert.ToInt32(numbers[i]);

                                        if (currentNumber - 1 == endRange)
                                        {
                                            endRange = currentNumber;
                                        }
                                        else
                                        {
                                            if (startRange != endRange)
                                                output.Append(startRange + "-" + endRange + "; ");
                                            else
                                                output.Append(startRange + "; ");

                                            startRange = currentNumber;
                                            endRange = currentNumber;
                                        }
                                    }

                                    if (startRange != endRange)
                                        output.Append(startRange + "-" + endRange + "; ");
                                    else
                                        output.Append(startRange + "; ");

                                    string result = output.ToString();


                                    if (lexem != pr_lexem)
                                        Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: недопустимые символы в идентификаторе", Lexem = lexem, Symbol = result + "", String = "" + current_string });
                                    pr_lexem = lexem;

                                }

                            }
                            current_char++;
                        }
                    }
                    else
                        break;
                }
                if (current_state == "ASSIGNTMENT")
                {
                    if (chars_in_string.Count > 0 && current_char < chars_in_string.Count)
                    {
                        if (transitions[current_state].ContainsKey(chars_in_string[current_char] + ""))
                        {
                            if (chars_in_string[current_char] == '[')
                            {
                                current_state = transitions[current_state][chars_in_string[current_char] + ""];
                                if (current_char < chars_in_string.Count)
                                    current_char++;
                                continue;

                            }
                            else
                            {
                                current_char++;
                            }
                        }
                        else
                        {
                            int code = 19;
                            string tmp_str = tmp[current_string - 1];
                            string sym = "";
                            lexem = "";
                            if (!tmp_str.Contains('[') && g)
                            {
                                Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: ожидается объявление инициализации списка при помощи [", Lexem = lexem, Symbol = current_char + "", String = "" + current_string });
                                g = false;

                            }
                            if (tmp_str[current_char] != '\r' && tmp_str[current_char] != '\n' && current_char + 1 < tmp_str.Length)
                            {

                                for (int i = current_char + 1; i < tmp_str.Length; i++)
                                {
                                    if (tmp_str[i] != '[')
                                        break;
                                    if (lexem == "" && tmp_str[i] == ' ')
                                        continue;
                                    if (transitions[current_state].ContainsKey(tmp_str[i] + "") && tmp_str[i] != ' ' && tmp_str[i] != '\r' && tmp_str[i] != '\n')
                                    {
                                        lexem += tmp_str[i];
                                        continue;
                                    }
                                    else
                                    {
                                        if (tmp_str[i] != '\r' && tmp_str[i] != '\n')
                                        {
                                            sym += Convert.ToString(i) + ';';
                                        }
                                    }
                                    if (tmp_str[i] != '\r' && tmp_str[i] != '\n')
                                    {
                                        lexem += tmp_str[i];
                                    }

                                }

                                StringBuilder output = new StringBuilder();

                                string[] numbers = sym.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                                if (numbers.Length > 0)
                                {
                                    int startRange = Convert.ToInt32(numbers[0]);
                                    int endRange = startRange;

                                    for (int i = 1; i < numbers.Length; i++)
                                    {
                                        int currentNumber = Convert.ToInt32(numbers[i]);

                                        if (currentNumber - 1 == endRange)
                                        {
                                            endRange = currentNumber;
                                        }
                                        else
                                        {
                                            if (startRange != endRange)
                                                output.Append(startRange + "-" + endRange + "; ");
                                            else
                                                output.Append(startRange + "; ");

                                            startRange = currentNumber;
                                            endRange = currentNumber;
                                        }
                                    }

                                    if (startRange != endRange)
                                        output.Append(startRange + "-" + endRange + "; ");
                                    else
                                        output.Append(startRange + "; ");

                                    string result = output.ToString();


                                    if (lexem != pr_lexem)
                                        Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: недопустимые символы при инициализации списка", Lexem = lexem, Symbol = result + "", String = "" + current_string });
                                    pr_lexem = lexem;

                                }
                            }
                            current_char++;
                        }
                    }
                    else
                        break;
                }
                if (current_state == "ITEMS")
                {

                    lexem = "";

                    if (chars_in_string.Count > 0 && current_char < chars_in_string.Count)
                    {
                        int d;
                        if (Int32.TryParse(chars_in_string[current_char] + "", out d))
                        {
                            current_state = "NUMBERREM";
                            current_char++;
                            continue;
                        }
                        if (chars_in_string[current_char] == ',')
                        {
                            current_char++;
                            //lexem = "";
                            continue;
                        }
                        if (chars_in_string[current_char] == ' ' && current_word == "")
                        {
                            current_char++;
                            continue;
                        }

                        if (transitions[current_state].ContainsKey(chars_in_string[current_char] + ""))
                        {

                            if (chars_in_string[current_char] == '+' || chars_in_string[current_char] == '-')
                            {
                                current_state = transitions[current_state][chars_in_string[current_char] + ""];
                                if (current_char < chars_in_string.Count)
                                    current_char++;
                                continue;
                            }
                            else if (chars_in_string[current_char] == '"')
                            {
                                current_state = transitions[current_state][chars_in_string[current_char] + ""];
                                if (current_char < chars_in_string.Count)
                                    current_char++;
                                continue;
                            }
                        }
                        else
                        {
                            int code = 19;
                            string tmp_str = tmp[current_string - 1];
                            string sym = "";

                            if (tmp_str[current_char] != '\r' && tmp_str[current_char] != '\n')
                            {
                                bool space = false;
                                for (int i = current_char; i < tmp_str.Length; i++)
                                {
                                    if (lexem == "" && tmp_str[i] == ' ')
                                        continue;
                                    if (tmp_str[i] == ',')
                                    {
                                        lexem += tmp_str[i];
                                        break;
                                    }
                                    if (tmp_str[i] == ' ')
                                    {
                                        space = true;
                                        break;
                                    }
                                    else
                                    {
                                        lexem += tmp_str[i];
                                        sym += Convert.ToString(i) + ';';
                                        continue;
                                    }
                                }

                                StringBuilder output = new StringBuilder();

                                string[] numbers = sym.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                                if (numbers.Length > 0)
                                {
                                    int startRange = Convert.ToInt32(numbers[0]);
                                    int endRange = startRange;

                                    for (int i = 1; i < numbers.Length; i++)
                                    {
                                        int currentNumber = Convert.ToInt32(numbers[i]);

                                        if (currentNumber - 1 == endRange)
                                        {
                                            endRange = currentNumber;
                                        }
                                        else
                                        {
                                            if (startRange != endRange)
                                                output.Append(startRange + "-" + endRange + "; ");
                                            else
                                                output.Append(startRange + "; ");

                                            startRange = currentNumber;
                                            endRange = currentNumber;
                                        }
                                    }

                                    if (startRange != endRange)
                                        output.Append(startRange + "-" + endRange + "; ");
                                    else
                                        output.Append(startRange + "; ");

                                    string result = output.ToString();

                                    if (lexem != pr_lexem)
                                    {
                                        Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: недопустимые символы при объявлении элемента списка", Lexem = lexem, Symbol = result + "", String = "" + current_string });
                                        if (lexem.Last() != ',')
                                            Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: отсутствует разделитель", Lexem = lexem, Symbol = current_char + ";", String = "" + current_string });
                                    }

                                    pr_lexem = "";
                                    for (int x = 0; x < lexem.Length; x++)
                                    {

                                        if (x == 0)
                                            continue;
                                        else
                                            pr_lexem += lexem[x];
                                    }
                                }
                            }
                            current_char++;
                        }
                    }
                    else
                        break;
                }
                if (current_state == "NUMBER")
                {
                    lexem = "";

                    if (chars_in_string.Count > 0 && current_char < chars_in_string.Count)
                    {
                        int d;

                        if (transitions[current_state].ContainsKey(chars_in_string[current_char] + ""))
                        {
                            current_state = transitions[current_state][chars_in_string[current_char] + ""];
                            if (current_char < chars_in_string.Count)
                                current_char++;
                            continue;
                        }
                        else
                        {
                            int code = 19;
                            string tmp_str = tmp[current_string - 1];
                            string sym = "";

                            if (tmp_str[current_char] != '\r' && tmp_str[current_char] != '\n')
                            {
                                bool space = false;
                                for (int i = current_char; i < tmp_str.Length; i++)
                                {
                                    if (lexem == "" && tmp_str[i] == ' ')
                                        continue;
                                    if (tmp_str[i] == '\n' || tmp_str[i] == '\r')
                                        continue;
                                    if (tmp_str[i] == ',')
                                    {
                                        lexem += tmp_str[i];
                                        break;
                                    }
                                    if (tmp_str[i] == ' ')
                                    {
                                        space = true;
                                        break;
                                    }
                                    else
                                    {
                                        lexem += tmp_str[i];
                                        sym += Convert.ToString(i) + ';';
                                        continue;
                                    }
                                }

                                StringBuilder output = new StringBuilder();

                                string[] numbers = sym.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                                if (numbers.Length > 0)
                                {
                                    int startRange = Convert.ToInt32(numbers[0]);
                                    int endRange = startRange;

                                    for (int i = 1; i < numbers.Length; i++)
                                    {
                                        int currentNumber = Convert.ToInt32(numbers[i]);

                                        if (currentNumber - 1 == endRange)
                                        {
                                            endRange = currentNumber;
                                        }
                                        else
                                        {
                                            if (startRange != endRange)
                                                output.Append(startRange + "-" + endRange + "; ");
                                            else
                                                output.Append(startRange + "; ");

                                            startRange = currentNumber;
                                            endRange = currentNumber;
                                        }
                                    }

                                    if (startRange != endRange)
                                        output.Append(startRange + "-" + endRange + "; ");
                                    else
                                        output.Append(startRange + "; ");

                                    string result = output.ToString();

                                    if (lexem != pr_lexem)
                                    {
                                        string fin_lexem = lexem.Replace(',', ' ');
                                        Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: недопустимые символы при объявлении целого числа", Lexem = fin_lexem, Symbol = result + "", String = "" + current_string });
                                        if (lexem.Last() != ',')
                                            Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: отсутствует разделитель", Lexem = fin_lexem, Symbol = current_char + ";", String = "" + current_string });
                                    }

                                    pr_lexem = "";
                                    for (int x = 0; x < lexem.Length; x++)
                                    {

                                        if (x == 0)
                                            continue;
                                        else
                                            pr_lexem += lexem[x];
                                    }
                                }
                            }
                            current_char++;
                        }
                    }
                    else
                        break;
                }
                if (current_state == "NUMBERREM")
                {
                    lexem = "";

                    if (chars_in_string.Count > 0 && current_char < chars_in_string.Count)
                    {
                        int d;

                        if (transitions[current_state].ContainsKey(chars_in_string[current_char] + ""))
                        {
                            if (chars_in_string[current_char] == ',')
                            {
                                current_state = transitions[current_state][chars_in_string[current_char] + ""];
                                if (current_char < chars_in_string.Count)
                                    current_char++;
                                continue;
                            }
                            else if (chars_in_string[current_char] == ']')
                            {
                                current_state = transitions[current_state][chars_in_string[current_char] + ""];
                                if (current_char < chars_in_string.Count)
                                    current_char++;
                                continue;
                            }
                            else if (chars_in_string[current_char] == '.')
                            {
                                current_state = transitions[current_state][chars_in_string[current_char] + ""];
                                if (current_char < chars_in_string.Count)
                                    current_char++;
                                continue;
                            }
                            else
                                current_char++; ;

                        }
                        else
                        {
                            int code = 19;
                            string tmp_str = tmp[current_string - 1];
                            string sym = "";

                            if (tmp_str[current_char] != '\r' && tmp_str[current_char] != '\n')
                            {
                                bool space = false;
                                for (int i = current_char; i < tmp_str.Length; i++)
                                {
                                    if (lexem == "" && tmp_str[i] == ' ')
                                        continue;
                                    if (tmp_str[i] == '\n' || tmp_str[i] == '\r')
                                        continue;
                                    if (tmp_str[i] == ',')
                                    {
                                        lexem += tmp_str[i];
                                        break;
                                    }
                                    if (tmp_str[i] == ' ')
                                    {
                                        space = true;
                                        break;
                                    }
                                    else
                                    {
                                        lexem += tmp_str[i];
                                        sym += Convert.ToString(i) + ';';
                                        continue;
                                    }
                                }

                                StringBuilder output = new StringBuilder();

                                string[] numbers = sym.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                                if (numbers.Length > 0)
                                {
                                    int startRange = Convert.ToInt32(numbers[0]);
                                    int endRange = startRange;

                                    for (int i = 1; i < numbers.Length; i++)
                                    {
                                        int currentNumber = Convert.ToInt32(numbers[i]);

                                        if (currentNumber - 1 == endRange)
                                        {
                                            endRange = currentNumber;
                                        }
                                        else
                                        {
                                            if (startRange != endRange)
                                                output.Append(startRange + "-" + endRange + "; ");
                                            else
                                                output.Append(startRange + "; ");

                                            startRange = currentNumber;
                                            endRange = currentNumber;
                                        }
                                    }

                                    if (startRange != endRange)
                                        output.Append(startRange + "-" + endRange + "; ");
                                    else
                                        output.Append(startRange + "; ");

                                    string result = output.ToString();

                                    if (lexem != pr_lexem)
                                    {
                                        string fin_lexem = lexem.Replace(',', ' ');
                                        Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: недопустимые символы при объявлении целого числа", Lexem = fin_lexem, Symbol = result + "", String = "" + current_string });
                                        if (lexem.Last() != ',')
                                            Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: отсутствует разделитель", Lexem = fin_lexem, Symbol = current_char + ";", String = "" + current_string });
                                    }

                                    pr_lexem = "";
                                    for (int x = 0; x < lexem.Length; x++)
                                    {

                                        if (x == 0)
                                            continue;
                                        else
                                            pr_lexem += lexem[x];
                                    }
                                }
                            }
                            current_char++;
                        }
                    }
                    else
                        break;
                }
                if (current_state == "DECIMAL")
                {
                    lexem = "";

                    if (chars_in_string.Count > 0 && current_char < chars_in_string.Count)
                    {
                        int d;

                        if (transitions[current_state].ContainsKey(chars_in_string[current_char] + ""))
                        {
                            current_state = transitions[current_state][chars_in_string[current_char] + ""];
                            if (current_char < chars_in_string.Count)
                                current_char++;
                            continue;
                        }
                        else
                        {
                            int code = 19;
                            string tmp_str = tmp[current_string - 1];
                            string sym = "";

                            if (tmp_str[current_char] != '\r' && tmp_str[current_char] != '\n')
                            {
                                bool space = false;
                                for (int i = current_char; i < tmp_str.Length; i++)
                                {
                                    if (lexem == "" && tmp_str[i] == ' ')
                                        continue;
                                    if (tmp_str[i] == '\n' || tmp_str[i] == '\r')
                                        continue;
                                    if (tmp_str[i] == ',')
                                    {
                                        lexem += tmp_str[i];
                                        break;
                                    }
                                    if (tmp_str[i] == ' ')
                                    {
                                        space = true;
                                        break;
                                    }
                                    else
                                    {
                                        lexem += tmp_str[i];
                                        sym += Convert.ToString(i) + ';';
                                        continue;
                                    }
                                }

                                StringBuilder output = new StringBuilder();

                                string[] numbers = sym.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                                if (numbers.Length > 0)
                                {
                                    int startRange = Convert.ToInt32(numbers[0]);
                                    int endRange = startRange;

                                    for (int i = 1; i < numbers.Length; i++)
                                    {
                                        int currentNumber = Convert.ToInt32(numbers[i]);

                                        if (currentNumber - 1 == endRange)
                                        {
                                            endRange = currentNumber;
                                        }
                                        else
                                        {
                                            if (startRange != endRange)
                                                output.Append(startRange + "-" + endRange + "; ");
                                            else
                                                output.Append(startRange + "; ");

                                            startRange = currentNumber;
                                            endRange = currentNumber;
                                        }
                                    }

                                    if (startRange != endRange)
                                        output.Append(startRange + "-" + endRange + "; ");
                                    else
                                        output.Append(startRange + "; ");

                                    string result = output.ToString();

                                    if (lexem != pr_lexem)
                                    {
                                        string fin_lexem = lexem.Replace(',', ' ');
                                        Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: недопустимые символы при объявлении числа с плавающей точкой", Lexem = fin_lexem, Symbol = result + "", String = "" + current_string });
                                        if (lexem.Last() != ',')
                                            Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: отсутствует разделитель", Lexem = fin_lexem, Symbol = current_char + ";", String = "" + current_string });
                                    }

                                    pr_lexem = "";
                                    for (int x = 0; x < lexem.Length; x++)
                                    {

                                        if (x == 0)
                                            continue;
                                        else
                                            pr_lexem += lexem[x];
                                    }
                                }
                            }
                            current_char++;
                        }
                    }
                    else
                        break;
                }
                if (current_state == "DECIMALREM")
                {
                    lexem = "";

                    if (chars_in_string.Count > 0 && current_char < chars_in_string.Count)
                    {
                        int d;

                        if (transitions[current_state].ContainsKey(chars_in_string[current_char] + ""))
                        {
                            if (chars_in_string[current_char] == ',')
                            {
                                current_state = transitions[current_state][chars_in_string[current_char] + ""];
                                if (current_char < chars_in_string.Count)
                                    current_char++;
                                continue;
                            }
                            else if (chars_in_string[current_char] == ']')
                            {
                                current_state = transitions[current_state][chars_in_string[current_char] + ""];
                                if (current_char < chars_in_string.Count)
                                    current_char++;
                                continue;
                            }
                            else
                                current_char++; ;

                        }
                        else
                        {
                            int code = 19;
                            string tmp_str = tmp[current_string - 1];
                            string sym = "";

                            if (tmp_str[current_char] != '\r' && tmp_str[current_char] != '\n')
                            {
                                bool space = false;
                                for (int i = current_char; i < tmp_str.Length; i++)
                                {
                                    if (lexem == "" && tmp_str[i] == ' ')
                                        continue;
                                    if (tmp_str[i] == '\n' || tmp_str[i] == '\r')
                                        continue;
                                    if (tmp_str[i] == ',')
                                    {
                                        lexem += tmp_str[i];
                                        break;
                                    }
                                    if (tmp_str[i] == ' ')
                                    {
                                        space = true;
                                        break;
                                    }
                                    else
                                    {
                                        lexem += tmp_str[i];
                                        sym += Convert.ToString(i) + ';';
                                        continue;
                                    }
                                }

                                StringBuilder output = new StringBuilder();

                                string[] numbers = sym.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                                if (numbers.Length > 0)
                                {
                                    int startRange = Convert.ToInt32(numbers[0]);
                                    int endRange = startRange;

                                    for (int i = 1; i < numbers.Length; i++)
                                    {
                                        int currentNumber = Convert.ToInt32(numbers[i]);

                                        if (currentNumber - 1 == endRange)
                                        {
                                            endRange = currentNumber;
                                        }
                                        else
                                        {
                                            if (startRange != endRange)
                                                output.Append(startRange + "-" + endRange + "; ");
                                            else
                                                output.Append(startRange + "; ");

                                            startRange = currentNumber;
                                            endRange = currentNumber;
                                        }
                                    }

                                    if (startRange != endRange)
                                        output.Append(startRange + "-" + endRange + "; ");
                                    else
                                        output.Append(startRange + "; ");

                                    string result = output.ToString();

                                    if (lexem != pr_lexem)
                                    {
                                        string fin_lexem = lexem.Replace(',', ' ');
                                        Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: недопустимые символы при объявлении числа с плавающей точкой", Lexem = fin_lexem, Symbol = result + "", String = "" + current_string });
                                        if (lexem.Last() != ',')
                                            Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: отсутствует разделитель", Lexem = fin_lexem, Symbol = current_char + ";", String = "" + current_string });
                                    }

                                    pr_lexem = "";
                                    for (int x = 0; x < lexem.Length; x++)
                                    {

                                        if (x == 0)
                                            continue;
                                        else
                                            pr_lexem += lexem[x];
                                    }
                                }
                            }
                            current_char++;
                        }
                    }
                    else
                        break;
                }
                if (current_state == "STRING")
                {
                    lexem = "";

                    if (chars_in_string.Count > 0 && current_char < chars_in_string.Count)
                    {
                        int d;

                        if (transitions[current_state].ContainsKey(chars_in_string[current_char] + ""))
                        {

                            if (chars_in_string[current_char] == '\"')
                            {
                                if (current_char + 1 < chars_in_string.Count)
                                {
                                    if (chars_in_string[current_char + 1] == ',')
                                    {
                                        current_char++;
                                        current_state = transitions[current_state]["\","];
                                    }
                                    if (chars_in_string[current_char] == ']')
                                    {
                                        current_char++;
                                        current_state = transitions[current_state]["\","];
                                    }
                                    if (chars_in_string[current_char] == '\"')
                                    {
                                        int j = current_char + 1;
                                        while (j < chars_in_string.Count)
                                        {
                                            if (chars_in_string[j] == ']')
                                                break;
                                            if (chars_in_string[j] == ',')
                                                break;
                                            if (chars_in_string[j] == '\"')
                                            {
                                                string tmp_str = tmp[current_string - 1];
                                                Output.Items.Add(new OutputItem { Code = "" + Convert.ToString(19), Type = "Ошибка: недопустимый символ внутри кавычек", Lexem = tmp_str, Symbol = current_char + "", String = "" + current_string });
                                                break;
                                            }
                                            else
                                            {
                                                string tmp_str = tmp[current_string - 1];
                                                Output.Items.Add(new OutputItem { Code = "" + Convert.ToString(19), Type = "Ошибка: отсутствует разделитель", Lexem = tmp_str, Symbol = j + "", String = "" + current_string });
                                            }
                                            if (j + 1 < chars_in_string.Count)
                                                j++;
                                            else
                                                break;
                                        }
                                    }
                                }
                                current_char++;
                                continue;
                            }
                            else if (chars_in_string[current_char] == ' ' && current_word == "")
                            {
                                current_char++;
                                continue;
                            }

                            else
                                current_char++; ;

                        }
                        else
                        {
                            int code = 19;
                            string tmp_str = tmp[current_string - 1];
                            string sym = "";

                            if (tmp_str[current_char] != '\r' && tmp_str[current_char] != '\n')
                            {
                                bool space = false;
                                for (int i = current_char; i < tmp_str.Length; i++)
                                {
                                    if (lexem == "" && tmp_str[i] == ' ')
                                        continue;
                                    if (tmp_str[i] == '\n' || tmp_str[i] == '\r')
                                        continue;
                                    if (tmp_str[i] == ',')
                                    {
                                        lexem += tmp_str[i];
                                        break;
                                    }
                                    if (tmp_str[i] == ' ')
                                    {
                                        space = true;
                                        break;
                                    }
                                    else
                                    {
                                        lexem += tmp_str[i];
                                        sym += Convert.ToString(i) + ';';
                                        continue;
                                    }
                                }

                                StringBuilder output = new StringBuilder();

                                string[] numbers = sym.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                                if (numbers.Length > 0)
                                {
                                    int startRange = Convert.ToInt32(numbers[0]);
                                    int endRange = startRange;

                                    for (int i = 1; i < numbers.Length; i++)
                                    {
                                        int currentNumber = Convert.ToInt32(numbers[i]);

                                        if (currentNumber - 1 == endRange)
                                        {
                                            endRange = currentNumber;
                                        }
                                        else
                                        {
                                            if (startRange != endRange)
                                                output.Append(startRange + "-" + endRange + "; ");
                                            else
                                                output.Append(startRange + "; ");

                                            startRange = currentNumber;
                                            endRange = currentNumber;
                                        }
                                    }

                                    if (startRange != endRange)
                                        output.Append(startRange + "-" + endRange + "; ");
                                    else
                                        output.Append(startRange + "; ");

                                    string result = output.ToString();

                                    
                                        string fin_lexem = lexem.Replace(',', ' ');
                                        Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: недопустимые символы при объявлении числа строки/символа", Lexem = fin_lexem, Symbol = result + "", String = "" + current_string });
                                        if (lexem.Last() != ',')
                                            Output.Items.Add(new OutputItem { Code = "" + code, Type = "Ошибка: отсутствует разделитель", Lexem = fin_lexem, Symbol = current_char + ";", String = "" + current_string });
                                    

                                    pr_lexem = "";
                                    for (int x = 0; x < lexem.Length; x++)
                                    {

                                        if (x == 0)
                                            continue;
                                        else
                                            pr_lexem += lexem[x];
                                    }
                                }
                            }
                            current_char++;
                        }
                    }
                    else
                        break;
                }
                
            }
            string tmp_str2 = tmp[current_string - 1];
            if (tmp_str2.Contains('[') && !tmp_str2.Contains(']'))
                Output.Items.Add(new OutputItem { Code = "" + 19, Type = "Ошибка: в конце инициализации списка ожидается ]", Lexem = tmp_str2, Symbol = Convert.ToString(tmp_str2.Length - 1), String = "" + current_string });
            if (Output.Items.Count == 0)
                Errors.Visibility = Visibility.Visible;
            if (Output.Items.Count > 0)
                Errors.Visibility = Visibility.Hidden;
        }

        /* 1) DEF->letter LISTNAME
         2) LISTNAME->letter LISTNAME | = ASSIGNTMENT
         3) ASSIGNTMENT-> [ITEMS
         4) ITEMS-> [+| -] NUMBER | " STRING
         5) NUMBER->digit NUMBERREM
         6) NUMBERREM-> , ITEMS | ] | digit NUMBERREM | .DECIMAL
         7) DECIMAL->digit DECIMALREM
         8) DECIMALREM-> , ITEMS | ] | digit DECIMALREM
         9) STRING-> "] | ", ITEMS | symbol STRING*/

        private bool IdCheck(string str)
        {
            bool isValidated = true;

            for (int c = 0; c < str.Length; c++)
            {
                if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')))
                {
                    isValidated = false;
                }
            }
            return isValidated;
        }
        private void Lexer(string word)
        {
            int code = 0;
            string type = "";

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
            else if (word == " " || word == ";" || word == ",")
            {
                code = 8;
                type = "Разделитель";
            }
            else if (word.Contains('\r') && word.Length == 1)
            {
                code = 9;
                type = "Переход на новую строку";
            }
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
            // Output.Items.Add(new OutputItem { Code = "" + code, Type = type, Lexem = word, Symbol = symbol, String = "" + strings_count });
        }
    }
}
