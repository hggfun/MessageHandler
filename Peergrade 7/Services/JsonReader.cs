using Microsoft.AspNetCore.Hosting;
using Peergrade_7.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Peergrade_7.Services
{
    /// <summary>
    /// Считывает из Json файла информацию.
    /// </summary>
    public class JsonReader
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public JsonReader()
        {
        }

        /// <summary>
        /// Получаем список пользователей из файла.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        public IEnumerable<User> GetUsers()
        {
            if (File.Exists("users.json"))
                using (var jsonFileReader = File.OpenText("users.json"))
                {
                    List<User> listic =  JsonSerializer.Deserialize<List<User>>(jsonFileReader.ReadToEnd(),
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                    listic.Sort();
                    return listic;
                }
            else
                //Если файла не будет, просто вернем пустоту.
                return new List<User>();
        }

        /// <summary>
        /// Считываем сообщения из файла.
        /// </summary>
        /// <returns>Список сообщений.</returns>
        public IEnumerable<MessageInfo> GetMessages()
        {
            if (File.Exists("messages.json"))
                using (var jsonFileReader = File.OpenText("messages.json"))
                {
                    return JsonSerializer.Deserialize<List<MessageInfo>>(jsonFileReader.ReadToEnd(),
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                }
            else
                //Если файла нет, то вернем пустоту.
                return new List<MessageInfo>();
        }
    }
}
