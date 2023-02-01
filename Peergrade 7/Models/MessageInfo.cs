using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peergrade_7.Models
{
    public class MessageInfo
    {
        /// <summary>
        /// Тема письма.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Содержание письма.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Отправитель.
        /// </summary>
        public string SenderId { get; set; }
        /// <summary>
        /// Получатель.
        /// </summary>
        public string ReceiverId { get; set; }
    }
}
