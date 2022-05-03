using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Client
{
    public static class UIHelpers
    {
        /// <summary>
        /// Show a menu item with an OK and Cancel Item. The menu will be closed after selecting either.
        /// </summary>
        /// <param name="prompt">Prompt displayed above the menu</param>
        /// <param name="okText">Text for the OK option</param>
        /// <param name="cancelText">Text for the Cancel option</param>
        /// <returns>true if the OK option was selected, false otherwise</returns>
        public static bool PromptConfirm(string prompt, string okText = "OK", string cancelText = "Cancel")
        {
            bool isOk = false;
            var confirm = new ConsoleMenu()
                .Add(okText, x => { isOk = true; x.CloseMenu(); })
                .Add(cancelText, m => m.CloseMenu())
                .Configure(c => c.WriteHeaderAction = () => { Console.WriteLine(prompt); });
            confirm.Show();
            return isOk;
        }

        /// <summary>
        /// Writes a prompt message 
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="includePressAnyKey"></param>
        public static void PromptAnyKey(string prompt = null, bool includePressAnyKey = true)
        {
            if (prompt != null)
            {
                Console.WriteLine(prompt);
            }
            if (includePressAnyKey)
            {
                Console.WriteLine("Press any key to continue...");
            }
            Console.ReadKey();

        }

        /// <summary>
        /// Adds a menu item that displays an editable property
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="itemNameSelector">delegate returning the menu item text</param>
        /// <param name="prompt">prompt text for the new value</param>
        /// <param name="setAction">action to set the new value</param>
        /// <returns></returns>
        public static ConsoleMenu AddPropertyEditor(this ConsoleMenu menu, Func<string> itemNameSelector, string prompt, Action<string> setAction) =>
            AddPropertyEditor(menu, itemNameSelector, prompt, setAction, Console.ReadLine);


        /// <summary>
        /// Adds a menu item that displays an editable property
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="itemNameSelector">delegate returning the menu item text</param>
        /// <param name="prompt">prompt text for the new value</param>
        /// <param name="setAction">action to set the new value</param>
        /// <returns></returns>
        public static ConsoleMenu AddPropertyEditor(this ConsoleMenu menu, Func<string> itemNameSelector, string prompt, Action<double> setAction) =>
            AddPropertyEditor(menu, itemNameSelector, prompt, setAction, () => double.Parse(Console.ReadLine()));

        /// <summary>
        /// Adds a menu item that displays an editable property
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="itemNameSelector">delegate returning the menu item text</param>
        /// <param name="prompt">prompt text for the new value</param>
        /// <param name="setAction">action to set the new value</param>
        /// <returns></returns>
        public static ConsoleMenu AddPropertyEditor(this ConsoleMenu menu, Func<string> itemNameSelector, string prompt, Action<int> setAction) =>
            AddPropertyEditor(menu, itemNameSelector, prompt, setAction, () => int.Parse(Console.ReadLine()));


        private static ConsoleMenu AddPropertyEditor<T>(this ConsoleMenu menu, Func<string> itemNameSelector, string prompt, Action<T> setAction, Func<T> reader)
        {
            menu.Add(itemNameSelector(), m =>
            {
                Console.Write(prompt);
                setAction(reader());
                m.CurrentItem.Name = itemNameSelector();
            });
            return menu;
        }

        /// <summary>
        /// Adds a menu item thad displays an editable reference to another entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="menu"></param>
        /// <param name="itemNameSelector">delegate returning the menu item text</param>
        /// <param name="entityManager">entity manager for the referenced entity type</param>
        /// <param name="setAction">action to set the foreign key value</param>
        /// <returns></returns>
        public static ConsoleMenu AddReferenceEditor<T>(this ConsoleMenu menu, Func<string> itemNameSelector, EntityManager<T> entityManager, Action<int> setAction, Action navigationPropertyUpdateAction) where T : Models.IEntity
        {
            menu.Add(itemNameSelector(), m =>
            {
                int foreignKey = default;
                entityManager.List(entity => foreignKey = entity.Id);
                if (foreignKey != default)
                {
                    setAction(foreignKey);
                    navigationPropertyUpdateAction();
                    m.CurrentItem.Name = itemNameSelector();
                }
            });
            return menu;
        }
    }
}
