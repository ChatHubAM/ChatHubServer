using DbAccessLayer.Entities;
using DbAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLayer.Repository
{
    public interface IVideoRepository
    {
        Task<string> SendVideoRequest(VideoToFrom videoToFrom);

        UserEntity ApproveVideoRequest(VideoToFrom videoToFrom);

    }
}
