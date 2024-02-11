using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.ServiceChat;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceChatCallback
    {
        bool isConnect = false;
        ServiceChatClient client;
        int Id;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
        }
        void ConnectUser()
        {
            if(!isConnect) 
            {
                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                Id =  client.Connect(tbUsername.Text);
                tbUsername.IsEnabled = false;
                bCon.Content = "Disconnect";
                isConnect = true;
            }
        }
        void DisconnectUser() 
        {
            if (isConnect)
            {
                client.Disconnect(Id);
                client = null;
                tbUsername.IsEnabled = true;
                bCon.Content = "Connect";
                isConnect = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnect) 
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }
        public void SendMessageCallBack(string message)
        {
            lbChat.Items.Add(message);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count-1]);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(client != null)
                {
                    client.SendMessage(tbMessage.Text, Id);
                    tbMessage.Text = string.Empty;
                }
            }
        }
    }
}
