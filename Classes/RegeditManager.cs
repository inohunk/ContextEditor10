using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ContextEditor10.Classes
{
    class RegeditManager
    {
        public RegeditManager()
        {
            checkPath();
            initCollection();
            saveRegedit();
        }
        private void checkPath()
        {
            if (!Directory.Exists(PATH_ROOT))
            {
                Directory.CreateDirectory(PATH_ROOT);
            }
            if (!Directory.Exists(PATH_BACKUPS))
            {

                Directory.CreateDirectory(PATH_BACKUPS);
            }
        }
        public void initCollection()
        {
            contextMenuData.Clear();
            foreach (string s in KEY_MENU_ITEMS.GetSubKeyNames())
            {
                ContextMenuData.Add(s);
            }
        }
        //Directories with settings
        const string PATH_ROOT = "%AppData%\\ContextEditor\\";
        const string PATH_BACKUPS = PATH_ROOT + "Backups\\";

        private ObservableCollection<string> contextMenuData = new ObservableCollection<string>();

        public ObservableCollection<string> ContextMenuData { get => contextMenuData; set => contextMenuData = value; }

        //Regedit var's
        const string REGEDIT_PATH_MENU = "Directory\\Background\\shell";

        public static RegistryKey KEY_ROOT = Registry.ClassesRoot;

        public static RegistryKey KEY_MENU_ITEMS = KEY_ROOT.OpenSubKey(REGEDIT_PATH_MENU,true);

        public void addItem(string name,string command)
        {
            KEY_MENU_ITEMS.CreateSubKey(name);
            RegistryKey tempKey = KEY_MENU_ITEMS.OpenSubKey(name,true);
            tempKey.CreateSubKey("command");
            tempKey = KEY_MENU_ITEMS.OpenSubKey(name + "\\command",true);
            tempKey.SetValue("", command);

        }
        public void addItem(string name,string command, string pathToIcon)
        {
            KEY_MENU_ITEMS.CreateSubKey(name);
            RegistryKey tempKey = KEY_MENU_ITEMS.OpenSubKey(name, true);
            tempKey.CreateSubKey("command");
            if(pathToIcon == null || pathToIcon == "")
            {
                tempKey.SetValue("icon", ContextEditor10.Properties.Resources.icon);

            }
            else
            {
                tempKey.SetValue("icon", pathToIcon);

            }
            tempKey = KEY_MENU_ITEMS.OpenSubKey(name + "\\command", true);
            tempKey.SetValue("", command);

        }

        public string[] getItemByName(string name)
        {
            initCollection();
            
            return null;

        }
        private bool checkItem(string name)
        {
            if (contextMenuData.Contains(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void deleteItemByName(string name)
        {

            if (checkItem(name)) { 
                KEY_MENU_ITEMS.DeleteSubKeyTree(name);
            }
            initCollection();

        }
        public void saveRegedit()
        {
            DateTime date = DateTime.UtcNow;
            string name = date.ToShortTimeString().Replace(" ", "").Replace(":","!");
             
            string command = "reg export HKEY_CLASSES_ROOT\\Directory\\Background\\shell\\ "+PATH_BACKUPS+name+".reg" + " /y";
            System.Diagnostics.Process.Start("cmd.exe", "/C " + command);
        }

    }
}
