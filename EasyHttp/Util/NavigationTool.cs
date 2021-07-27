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
        public NavigationTool()
        {
            NavigationStack = new Stack<Page>();
        }
        public bool Navigation(Frame frame, Page page)
        {
            frame.Navigate(page);
            NavigationStack.Push(page);
            return true;
        }
        public bool GoBack(Frame frame)
        {
            if (NavigationStack.Count <= 1) return false;
            NavigationStack.Pop();
            frame.Navigate(NavigationStack.Last());
            return true;
        }
    }
}
