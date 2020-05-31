using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TC_MVVM.ViewModel.Base;
using TC_MVVM.Model;
using System.Security;
using System.IO.IsolatedStorage;

namespace TC_MVVM.ViewModel
{
    class MainWindowViewModel
    {
        public PanelViewModel Left { get; set; }
        public PanelViewModel Right { get; set; }
        
        public MainWindowViewModel()
        {
            Left = new PanelViewModel();
            Right = new PanelViewModel();
        }

        private ICommand _copyL = null;
        private ICommand _copyR = null;
        public ICommand CopyL
        {
            get
            {
                if (_copyL == null)
                {
                    _copyL = new RelayCommand(
                        arg => {
                            
                            if (Left.CurrentItem != null)
                            {
                                new Copy(Left.CurrentItem, Right.CurrentDir);
                                Right.DirContains();
                                Left.CurrentItem = null;
                            }
                        },
                        arg => Left.IsFile() && !Right.IsFile() && 
                               !Right.DirContent.Contains(Left.CurrentItem)
                        );
                }
                return _copyL;
            }
        }
        public ICommand CopyR
        {
            get
            {
                if (_copyR == null)
                {
                    _copyR = new RelayCommand(
                        arg => {

                            if (Right.CurrentItem != null)
                            {
                                new Copy(Right.CurrentItem, Left.CurrentDir);
                                Left.DirContains();
                                Right.CurrentItem = null;
                            }
                        },
                        arg => Right.IsFile() && !Left.IsFile() && 
                               !Left.DirContent.Contains(Right.CurrentItem)
                        );
                }
                return _copyR;
            }
        }
    }
}
