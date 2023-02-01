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
    /// Записываем в файл.
    /// </summary>
    public class JsonWriter
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public JsonWriter()
        {
        }
        /// <summary>
        /// Записываем в файл список пользователей.
        /// </summary>
        /// <param name="listic">Список пользователей.</param>
        public void WriteUsers(List<User> listic)
        {
            try
            {
                File.WriteAllText("users.json", JsonSerializer.Serialize(listic, listic.GetType()));
            }
            catch (Exception) { }
            
        }
        /// <summary>
        /// Записывает в файл список сообщений.
        /// </summary>
        /// <param name="listic">Список сообщений.</param>
        public void WriteMessages(List<MessageInfo> listic)
        {
            try
            {
                File.WriteAllText("messages.json", JsonSerializer.Serialize(listic, listic.GetType()));
            }
            catch (Exception) { }
        }
    }
}
