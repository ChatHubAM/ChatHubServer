using DbAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DbAccessLayer.Models;

namespace DbAccessLayer.Repository
{
    public interface IMessageRepository
    {
        /// <summary>
        /// Sends Messages to another user
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<bool> SendMessage(MessageEntity message);

        /// <summary>
        /// Checks unread messages of a user
        /// </summary>
        /// <param name="notif">parameter that indicates which user is </param>
        /// <returns></returns>
        Task<IEnumerable<MessageEntity>> UnreadMasseageNotification(MessageToFrom notif);

        /// <summary>
        /// If users checks the message make that massage status to read
        /// </summary>
        /// <param name="notif"></param>
        /// <returns></returns>
        Task MakeMessageRead(MessageToFrom notif);

        /// <summary>
        /// Brings all messages to user
        /// </summary>
        /// <param name="notif"></param>
        /// <returns></returns>
        Task<IEnumerable<MessageEntity>> GetAllMessages(MessageToFrom notif);
        Task<IEnumerable<MessageEntity>> GetAllUnreadMessages();
    }
}
