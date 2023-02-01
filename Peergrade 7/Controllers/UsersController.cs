using Microsoft.AspNetCore.Mvc;
using Peergrade_7.Models;
using Peergrade_7.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Peergrade_7.Controllers
{
    /// <summary>
    /// Контроллер для пользователей и сообщений.
    /// </summary>
    [Route("")]
    public class UsersController : Controller
    {
        static JsonReader reader = new JsonReader();
        public static List<User> users = reader.GetUsers().ToList();

        /// <summary>
        /// Показывает всех пользователей.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllUsers")]
        public IActionResult Get()
        {
            return Ok(users);
        }

        /// <summary>
        /// Показывает пользователя по полученной почте.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("GetUserByEmail/{email}")]           // параметр для маршрутизации
        public IActionResult Get(string email)
        {
            var user = users.SingleOrDefault(p => p.Email == email);

            if (user == null)
            {
                return NotFound("This user is not found");
            }

            return Ok(user);
        }

        /// <summary>
        /// Создает 10 пользователей с рандомной генерацией имен и почт.
        /// </summary>
        /// <returns>Действие, выводящее всех пользователей.</returns>
        [HttpPost("GenerateUsers")]           // параметр для маршрутизации
        public IActionResult GenerateUsers()
        {
            users = new List<User>();
            for (int i = 0; i < 10; i++)
            {
                bool b = true;
                string email = null;
                //Хотел это в отдельный метод бахнуть, но вылезала какая-то ошибка, поэтому генерация строки внутри метода
                //(что конечно явное противоречие SOLID, но я реально не знаю как починить)
                while (b)
                {
                    string str1 = GenerateString(null);
                    email = str1 + "@brawl.stars";
                    b = false;
                    foreach (var person in users)
                    {
                        if (person.Email == email)
                            b = true;
                        break;
                    }
                }
                User user = new User();
                user.Email = email;
                user.UserName = GenerateString(null);
                users.Add(user);
            }
            users.Sort();
            //обновляем также список пользователей в файле.
            JsonWriter writer = new JsonWriter();
            writer.WriteUsers(users);
            return CreatedAtAction(nameof(Get), Get());
        }

        private static string GenerateString(string str1)
        {
            for (int j = 0; j < 5; j++)
            {
                Random rnd = new Random();
                str1 += (char)rnd.Next('a', 'z' + 1);
            }

            return str1;
        }

        /// <summary>
        /// Добавляем одного пользователя, имя и почта генерируются рандомно.
        /// </summary>
        /// <returns>Нового пользователя.</returns>
        [HttpPost("AddUser")]           // параметр для маршрутизации
        public IActionResult AddUser()
        {
            if (users.Count > 19)
                return BadRequest("It's already too much users");
            bool b = true;
            string email = null;
            while (b)
            {
                email = GenerateString(null) + "@brawl.stars";
                b = false;
                foreach (var person in users)
                {
                    if (person.Email == email)
                        b = true;
                    break;
                }
            }
            User user = new User();
            user.Email = email;
            user.UserName = GenerateString(null);
            users.Add(user);
            users.Sort();
            JsonWriter writer = new JsonWriter();
            writer.WriteUsers(users);
            return Ok(user);
        }

        /// <summary>
        /// Список сообщений.
        /// </summary>
        private static List<MessageInfo> messages = reader.GetMessages().ToList();

        /// <summary>
        /// Находим список сообщений для отпраителя и получателя.
        /// </summary>
        /// <param name="SenderId">Отправитель.</param>
        /// <param name="ReceiverId">Получатель.</param>
        /// <returns>Список сообщений.</returns>
        [HttpGet("GetMessageByAll/{SenderId},{ReceiverId}")]
        public IActionResult GetByAll(string SenderId, string ReceiverId)
        {
            List<MessageInfo> list = new List<MessageInfo>();
            foreach (var x in messages)
            {
                if (x.SenderId == SenderId && x.ReceiverId == ReceiverId)
                    list.Add(x);
            }
            return Ok(list);
        }

        /// <summary>
        /// Находим список сообщений для отпраителя.
        /// </summary>
        /// <param name="SenderId">Отправитель.</param>
        /// <returns>Список сообщений.</returns>
        [HttpGet("GetMessageBySender/{SenderId}")]
        public IActionResult GetBySenderId(string SenderId)
        {
            List<MessageInfo> list = new List<MessageInfo>();
            foreach (var x in messages)
            {
                if (x.SenderId == SenderId)
                    list.Add(x);
            }
            return Ok(list);
        }

        /// <summary>
        /// Находим список сообщений для получателя.
        /// </summary>
        /// <param name="ReceiverId">Получатель.</param>
        /// <returns>Список сообщений.</returns>
        [HttpGet("GetMessageByReceiver/{ReceiverId}")]
        public IActionResult GetByReceiverId(string ReceiverId)
        {
            List<MessageInfo> list = new List<MessageInfo>();
            foreach (var x in messages)
            {
                if (x.ReceiverId == ReceiverId)
                    list.Add(x);
            }
            return Ok(list);
        }

        /// <summary>
        /// Создает сообщения.
        /// </summary>
        /// <returns>Список сообщений.</returns>
        [HttpPost("GenerateMessages")]           // параметр для маршрутизации
        public IActionResult GenerateMessages()
        {
            messages = new List<MessageInfo>();
            for (int i = 0; i < 10; i++)
            {
                Random rnd = new Random();
                int n = rnd.Next(users.Count);
                string SenderId = users[n].Email;
                n = rnd.Next(users.Count);
                string ReceiverId = users[n].Email;
                MessageInfo new_message = new MessageInfo();
                new_message.Message = GenerateString(null); ;
                new_message.Subject = GenerateString(null); ;
                new_message.SenderId = SenderId;
                new_message.ReceiverId = ReceiverId;
                messages.Add(new_message);
            }
            //обновляем также список пользователей в файле.
            JsonWriter writer = new JsonWriter();
            writer.WriteMessages(messages);
            return Ok(messages);
        }
    }
}
