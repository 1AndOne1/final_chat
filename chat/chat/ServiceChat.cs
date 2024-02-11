using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace chat
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    

    public class ServiceChat : IServiceChat
    {
        List<User> users = new List<User>();
        int newId = 1;


        public int Connect(string name)
        {
            User user = new User() {
                Id = newId,
                Name = name,
                operationContext = OperationContext.Current
        };
            newId++;

            SendMessage(user.Name + "Has been connected to chat",0);
            users.Add(user);
            return user.Id;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(x => x.Id == id);
            if(user!=null)
            {
                users.Remove(user);
                SendMessage(user.Name + "Has disconnected", 0);
            }
        }

        public void SendMessage(string message, int id)
        {
            foreach(var item in users) {
                string answer = DateTime.Now.ToShortTimeString();
                var user = users.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    answer += "- " + user.Name + " ";
                }
                answer += message;

                item.operationContext.GetCallbackChannel<ChatCallBack>().SendMessageCallBack(answer);
            }
        }
    }
}
