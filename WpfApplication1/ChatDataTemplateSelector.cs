using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApplication1.Models;

namespace WpfApplication1
{
    public class ChatDataTemplateSelector: DataTemplateSelector
    {
        public DataTemplate MyMessageDataTemplate { get; set; }
        public DataTemplate OtherMessageDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item,
                   DependencyObject container)
        {
            if (((Message)item).author==((App)Application.Current).user.id)
            {
                return MyMessageDataTemplate;
            }
            else{
                return OtherMessageDataTemplate;
            }
        }
    }
}
