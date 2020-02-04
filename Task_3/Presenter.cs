using System;
using Microsoft.Win32;
using System.Reflection;

namespace AdditionTaskWPF
{
    class Presenter
    {
        MainWindow view;
        OpenFileDialog choiceFile; // клас диалоговых окон
        Assembly reflection;


        public Presenter(MainWindow mainWindow)
        {
            this.view = mainWindow;
            view.Clear += ClearEvetn;
            view.Open += OpenEvent;
            choiceFile = new OpenFileDialog();
        }


        // то что поместим в собитие (делегат) ***.Invoke(object sender, EventArgs e)
        #region Подписчики на собитие

        private void ClearEvetn(object sender, EventArgs e)
        {
            view.textBox.Clear();
            view.textBox.Text = "Chose assembly - click 'Open'";
        }



        /// <summary>
        /// Тут вся логика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenEvent(object sender, EventArgs e)
        {
            try
            {
                string showInfo = default; // мета данные о всей сборке

                // фильт форматов которые покажет диалоговое окно
                choiceFile.Filter = "Все форматы сборок: (*.exe;*.dll)|*.dll;*.exe|Сборка: (*.exe)|*.exe|Сборка (*.dll)|*.dll";

                // откроем окно
                if (choiceFile.ShowDialog() != null)
                {
                    reflection = Assembly.LoadFrom(choiceFile.FileName);

                    view.textBox.Clear();
                    view.textBox.Text += $"Соборка по темному пути: {choiceFile.FileName}" + Environment.NewLine + Environment.NewLine;

                    showInfo += "Типы и инвормация о членах:" + Environment.NewLine;
                    Type[] allTypes = reflection.GetTypes(); // вытаскиваем все
                    foreach (Type item in allTypes)
                    {
                        showInfo += item.Name + Environment.NewLine + new string('-', 25) + Environment.NewLine;


                        #region Находим данные о конструкторах
                        ConstructorInfo[] ctorArray = item.GetConstructors();

                        // если конструкторов нет, значит этот стереотип не относится к классам
                        if (ctorArray.Length != 0)
                        {
                            showInfo += "Конструкторы:" + Environment.NewLine;
                            foreach (var ctor in ctorArray)
                            {
                                showInfo += GetSignatureCtor(ctor) + Environment.NewLine;
                            }
                            showInfo += Environment.NewLine;

                        }
                        else
                        {
                            showInfo += "Даный стереотип не имеет конструкторов" + Environment.NewLine + Environment.NewLine;
                        }
                        #endregion

                        #region Находим данные о методах
                        MethodInfo[] methArray = item.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.InvokeMethod);

                        // обезательно провериить есть ли методы в даном типе, что бы не писать (канкатенировать) лишнего
                        if (methArray.Length != 0)
                        {
                            showInfo += "Методы:" + Environment.NewLine;
                            foreach (var meth in methArray)
                            {
                                showInfo += GetSignaturMeth(meth) + Environment.NewLine;
                            }
                            showInfo += Environment.NewLine;
                        }
                        else
                            showInfo += "Без методов" + Environment.NewLine + Environment.NewLine;
                        #endregion

                        #region Находим данные о свойствах
                        PropertyInfo[] propArray = item.GetProperties();

                        // обезательно провериить есть ли свойства в даном типе, что бы не писать (канкатенировать) лишнего
                        if (propArray.Length != 0)
                        {
                            showInfo += "Свойства:" + Environment.NewLine;
                            foreach (var prop in propArray)
                            {
                                showInfo += $"{prop.PropertyType.Name} {prop.Name}" + Environment.NewLine;
                            }
                            showInfo += Environment.NewLine;
                        }
                        else
                            showInfo += "Без свойств" + Environment.NewLine + Environment.NewLine;
                        #endregion
                    }
                }
                view.textBox.Text += showInfo;
            }
            catch (Exception ex)
            {
                view.textBox.Text = ex.Message;
            }
        }
        #endregion

        /// <summary>
        /// Построить сигнатуру метода, с возвращаемым значением
        /// </summary>
        /// <param name="meth"></param>
        /// <returns></returns>
        static string GetSignaturMeth(MethodInfo meth)
        {
            string signature = $"{meth.ReturnType} {meth.Name} ( ";

            ParameterInfo[] arguments = meth.GetParameters();
            for (int count = 0; count < arguments.Length;)
            {
                signature += $"{arguments[count].ParameterType} {arguments[count].Name}";
                count++;
                if (count != arguments.Length)
                    signature += ", ";
            }
            return signature + " )";
        }

        /// <summary>
        /// Построить сигнатуру конструктора, с возвращаемым значением
        /// </summary>
        /// <param name="ctor"></param>
        /// <returns></returns>
        static string GetSignatureCtor(ConstructorInfo ctor)
        {
            string signature = $"{ctor.Name} ( ";

            ParameterInfo[] arguments = ctor.GetParameters();
            for (int count = 0; count < arguments.Length;)
            {
                signature += $"{arguments[count].ParameterType.Name} {arguments[count].Name}";
                count++;
                if (count != arguments.Length)
                    signature += ", ";
            }
            return signature + " )";
        }

    }
}
