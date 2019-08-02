using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DbAccessLayer.Context;
using DbAccessLayer.Entities;
using DbAccessLayer.Models;
using System.Linq;

namespace DbAccessLayer.Repository
{
    public class VideoRepository : BaseRepository, IVideoRepository
    {
        public VideoRepository(AppDbContext context) : base(context)
        {
        }

        public UserEntity ApproveVideoRequest(VideoToFrom videoToFrom)
        {
          var videoRequest = _context.VideoRequests.Where(vidreq => vidreq.To == videoToFrom.To && vidreq.From == videoToFrom.From 
                                    && vidreq.IsConnected == false).FirstOrDefault();

            var videoRequestingUser = _context.Users.Where(user => user.Username == videoRequest.From).FirstOrDefault();

            var videoRequestToChange = _context.VideoRequests.Single(vid => vid == videoRequest);
            videoRequestToChange.IsConnected = true;

            _context.SaveChangesAsync();

            return videoRequestingUser;

        }

        public async Task<string> SendVideoRequest(VideoToFrom videoToFrom)
        {
            _context.VideoRequests.Add(videoToFrom);
            await _context.SaveChangesAsync();
            var callingUser = _context.Users.Where(o => o.Username == videoToFrom.To).FirstOrDefault();
            return callingUser.IPLocal;
        }
    }
}
