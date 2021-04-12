using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SticksyProtocol
{
    public interface IData { }
    public enum CommandUser { SignUp, SignIn }

    [Serializable]
    public class Sign : IData  //запрос пользователя серверу на регистрацию/авторизацию
    {
        public string login { get; }
        public string password { get; }
        public CommandUser command { get; }

        public Sign(string login, string password, CommandUser command)
        {
            this.login = login;
            this.password = password;
            this.command = command;
        }
    }


    [Serializable]
    public class CreateStick : IData  //запрос пользователя серверу на создание стика
    {
        public int idCreator { get; }

        public CreateStick(int idCreator)
        {
            this.idCreator = idCreator;
        }
    }

    [Serializable]
    public class DelStick : IData  //запрос пользователя серверу на удаление стика
    {
        public int idStick { get; }

        public DelStick(int idStick)
        {
            this.idStick = idStick;
        }
    }

    [Serializable]
    public class EditStick : IData  //запрос пользователя серверу на редактирование стика (кроме добавления другого пользователя к стику) 
    {
        public Stick stick { get; }

        public EditStick(Stick stick)
        {
            this.stick = stick;
        }
    }

    [Serializable]
    public class GetUsers : IData { }

    [Serializable]
    public class AddFriend : IData  //запрос пользователя серверу на добавление другого пользователя к стику
    {
        public int idCreator { get; }
        public int idFriend { get; }
        public int idStick { get; }
        public AddFriend(int idCreator, int idFriend, int idStick)
        {
            this.idCreator = idCreator;
            this.idFriend = idFriend;
            this.idStick = idStick;
        }
    }

    [Serializable]
    public class AnswerId : IData  // ответ сервера пользователю - id стика или пользователя или null в случае отказа (если при регистрации уже есть пользователь с данным логином)
    {
        public int id { get; }
        public AnswerId(int id)
        {
            this.id = id;
        }
    }

    [Serializable]
    public class AnswerUser : IData  //ответ сервера пользователю - сам user или null в случае отказа (если нет пользоввателя с таким логином и паролем)
    {
        public User user { get; }

        public AnswerUser(User user)
        {
            this.user = user;
        }
    }

    [Serializable]
    public class AnswerListUser : IData  //ответ сервера пользователю - список всех пользователей
    {
        public List<Friend> users { get; }

        public AnswerListUser(List<Friend> users)
        {
            this.users = users;
        }
    }
    public class Transfer
    {
        private static BinaryFormatter formatter = new BinaryFormatter();
        public static void SendData(TcpClient clientSocket, IData data)
        {
            formatter.Serialize(clientSocket.GetStream(), data);
        }

        public static async void SendDataAsync(TcpClient clientSocket, IData data)
        {
            await Task.Run(() => formatter.Serialize(clientSocket.GetStream(), data));
        }

        public static IData ReceiveData(TcpClient clientSocket)
        {
            return (IData)formatter.Deserialize(clientSocket.GetStream());
        }
    }
}
