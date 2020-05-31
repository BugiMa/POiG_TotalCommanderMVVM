using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using TC_MVVM.Properties;

using TC_MVVM.Model;

namespace TC_MVVM.ViewModel
{
    using Base;
    using System.Runtime.InteropServices.WindowsRuntime;

    class PanelViewModel: ViewModelBase
    {
        private string currentDir;
        private string currentDrive;
        private string currentItem;
        private List<string> dirContent;
        private List<string> drives;

        public string CurrentDir
        {
            get => currentDir; set { currentDir = value; DirContains(); OnPropertyChanged(); }
        }
        public string CurrentDrive 
        {
            get => currentDrive; set { currentDrive = value; CurrentDir = value; OnPropertyChanged(); }
        }
        public string CurrentItem
        {
            get => currentItem; set { currentItem = value; ChangeDir(value); OnPropertyChanged(); }
        }
        public List<string> DirContent
        {
            get => dirContent; set { dirContent = value; OnPropertyChanged(); }
        }
        public List<string> Drives
        {
            get => drives; set { drives = value; OnPropertyChanged(); }
        }

        public PanelViewModel()
        {
            AvailableDrives();
            CurrentDrive = Drives[0];
            CurrentDir = CurrentDrive;
            DirContains();
            CurrentItem = null;
        }

        public void AvailableDrives()
        {
            Drives = Directory.GetLogicalDrives().ToList();
        }

        public void DirContains()
        {
            DirContent = new List<string> ();
            
            if (!drives.Contains(currentDir))
                DirContent.Add(Resources.DotDot);

            List<string> SubDirs = Directory.GetDirectories(currentDir).ToList();
            SubDirs.ForEach(s => DirContent.Add( s.Insert(0, Resources.D) ));
           
            List<string> Files = Directory.GetFiles(currentDir).ToList();
            DirContent.AddRange(Files);
        }

        public void ChangeDir(string dir)
        {
            if (dir != null)
            {
                if (dir == Resources.DotDot)
                {
                    CurrentDir = Directory.GetParent(CurrentDir).ToString();
                    currentItem = null;
                }
                else if (dir.Contains(Resources.D))
                {
                    CurrentDir = dir.Remove(0,3);
                    currentItem = null;
                }
            } 
        }

        public bool IsFile()
        {
            if (currentItem == null || currentItem.Contains("<D>"))
                return false;
            else return true;
        }
    }
}
