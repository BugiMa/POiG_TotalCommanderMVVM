using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace TC_MVVM.Model
{
    class Copy
    {
        public Copy(string file, string destination)
        {
            string fileName = file.Remove(0, file.LastIndexOf(@"\"));
            
            File.Copy(file, destination + fileName);
        }

    }
}
