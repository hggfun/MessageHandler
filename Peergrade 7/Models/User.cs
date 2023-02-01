using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Peergrade_7.Models
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User:IComparable
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Почта.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Определил CompareTo, чтоб сортировать список.
        /// </summary>
        /// <param name="obj">По сути другой пользователь.</param>
        /// <returns>Число 1,-1 или 0, для сортировки.</returns>
        public int CompareTo(object obj)
        {
            return this.Email.CompareTo(((User)obj).Email);
        }
    }
}