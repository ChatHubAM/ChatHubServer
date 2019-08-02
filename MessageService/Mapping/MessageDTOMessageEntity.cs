using AutoMapper;
using DbAccessLayer.Entities;
using MessageService.Models;
using System;
using System.Collections.Generic;

namespace MessageService.Mapping
{
    /// <summary>
    /// Maps Message DTO to Message Entity
    /// </summary>
    public class MessageDTOMessageEntity : Profile
    {
        public MessageDTOMessageEntity()
        {
            CreateMap<MessageDTO, MessageEntity>();
        }
    }
}
