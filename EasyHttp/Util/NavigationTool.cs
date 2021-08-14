using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EasyHttp.Util
{
    public class NavigationTool
    {
        public Stack<Page> NavigationStack { get; set; }
        public Frame frame { get; set; }
        public NavigationTool(Frame frame)
        {
            this.frame = frame;
        }
        public bool Navigation(Page page)
        {
            frame.Navigate(page);
            return true;
        }
        public bool GoBack()
        {
            frame.NavigationService.GoBack();
            GC.Collect();
            return true;
        }
    }
}
