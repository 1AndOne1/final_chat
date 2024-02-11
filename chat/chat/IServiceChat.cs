using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace chat
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IServiceChat" в коде и файле конфигурации.
    [ServiceContract(CallbackContract = typeof(ChatCallBack))]
    public interface IServiceChat
    {
        [OperationContract]
        int Connect(string name);

        [OperationContract]

        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        
        void SendMessage(string message, int id);
    }

    public interface ChatCallBack
    {
        [OperationContract(IsOneWay = true)] 
        void SendMessageCallBack(string message);
    }
}
