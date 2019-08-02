using DbAccessLayer.Context;
using DbAccessLayer.Entities;
using DbAccessLayer.Models;
using DbAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLayer.Repository
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MessageEntity>> UnreadMasseageNotification(MessageToFrom notif)
        {
            //If User's message is unread, returns message list
            var unreadMessages = _context.Messages.Where(mess => mess.IsRead == false && mess.To == notif.To && mess.From == notif.From);
            return await unreadMessages.ToListAsync();
        }

        public async Task<IEnumerable<MessageEntity>> GetAllMessages(MessageToFrom notif)
        {
            //gets all Users messages from selected User 
            var allMessages = _context.Messages.Where(mess =>(mess.To == notif.To && mess.From == notif.From) || ( mess.To == notif.From && mess.From == notif.To));
            return await allMessages.ToListAsync();
        }

        public async Task MakeMessageRead(MessageToFrom notif)
        {
            //makes User's unread messages read of a selected User 
            var unreadMessages = _context.Messages.Where(mess => mess.IsRead == false && mess.To == notif.To && mess.From == notif.From);

            foreach (var message in unreadMessages)
            {
                var messageToChange = _context.Messages.Single(mess => mess == message);
                messageToChange.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> SendMessage(MessageEntity message)
        {
            try
            {
                //Adds message to Db 
                using (_context.Messages.AddAsync(message))
                {
                    await _context.SaveChangesAsync();
                    return true; //message is senc succesfully
                }
            }
            catch
            {
                return false; //message is not sent
            }
        }

        public async Task<IEnumerable<MessageEntity>> GetAllUnreadMessages()
        {
            var unreadMessages = _context.Messages.Where(message => message.IsRead == false);
            return await unreadMessages.ToListAsync();
        }
    }
}
