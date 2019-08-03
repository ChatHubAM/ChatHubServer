using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DbAccessLayer.Entities;
using DbAccessLayer.Models;
using DbAccessLayer.Repository;
using MessageService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class MessageController : Controller
    {
        private readonly IMessageRepository messageRepository;
        private readonly IMapper mapper;
        public MessageController(IMessageRepository messageRepository, IMapper mapper)
        {
            this.mapper = mapper; //DI
            this.messageRepository = messageRepository; //DI
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageDTO message)
        {
            var newMessage = mapper.Map<MessageDTO, MessageEntity>(message);

            if (await messageRepository.SendMessage(newMessage))
            {
                return Ok();
            }
            else
            {
                BadRequest("Message is not sent");
            }

            return BadRequest();
        }

        [Route("unreadmessages")]
        [HttpPost]
        public async Task<IEnumerable<MessageDTO>> ReadUnreadMessages(MessageToFrom notif)
        {
            var unreadMessages = await messageRepository.UnreadMasseageNotification(notif);
            var unreadMessagesDTO = new List<MessageDTO>();

            foreach (var messageEntity in unreadMessages)
            {
                var messageDTO = new MessageDTO
                {
                    From = messageEntity.From,
                    To = messageEntity.To,
                    MessageText = messageEntity.MessageText
                };

                unreadMessagesDTO.Add(messageDTO);
            }

            return unreadMessagesDTO;
        }

        [Route("readallmessages")]
        [HttpPost]
        public async Task<IEnumerable<MessageDTO>> ReadAllMessages(MessageToFrom notif)
        {   
            var allEntityMessages = await messageRepository.GetAllMessages(notif);
            var allMessagesDTO = new List<MessageDTO>();

            foreach (var message in allEntityMessages)
            {
                var messageDTO = new MessageDTO
                {
                    From = message.From,
                    To = message.To,
                    MessageText = message.MessageText
                };

                allMessagesDTO.Add(messageDTO);
            }
            return allMessagesDTO;
        }

        [Route("read")]
        [HttpPost]
        public async Task MakeMessageRead(MessageToFrom notif)
        {
            await messageRepository.MakeMessageRead(notif);
        }

        [Route("allunread")]
        [HttpGet]
        public async Task<IEnumerable<MessageDTO>> GetAllUnreadMessages()
        {
            var allUnreadMessagesEntity = await messageRepository.GetAllUnreadMessages();
            var allUnreadMessagesDTO = new List<MessageDTO>();

            foreach (var unreadMessage in allUnreadMessagesEntity)
            {
                var messageDTO = new MessageDTO
                {
                    From = unreadMessage.From,
                    To = unreadMessage.To,
                    MessageText = unreadMessage.MessageText
                };

                allUnreadMessagesDTO.Add(messageDTO);
            }
            return allUnreadMessagesDTO;
        }
    }
}
