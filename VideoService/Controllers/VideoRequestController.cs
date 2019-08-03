using AutoMapper;
using DbAccessLayer.Models;
using DbAccessLayer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VideoRequestController : Controller
    {
        private readonly IVideoRepository videoRepository;
        public VideoRequestController(IVideoRepository videoRepository, IMapper mapper)
        {
            this.videoRepository = videoRepository; //DI
        }

        [HttpPost]
        [Route("send")]
        public async Task<string>  SendVideoRequest(VideoToFrom videoToFrom)
        {
            var ip = await videoRepository.SendVideoRequest(videoToFrom);
            return ip;
        }

        [HttpPost]
        [Route("approve")]
        public string ApproveVideoRequest(VideoToFrom videoToFrom)
        {
            var videoRequestingUserEntity = videoRepository.ApproveVideoRequest(videoToFrom);
            return videoRequestingUserEntity.IPLocal;
        }
    }
}
