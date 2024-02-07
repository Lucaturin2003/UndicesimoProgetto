using Events;

var video = new Video() { Title = "Video 1" };
var videoEncoder = new VideoEncoder();
var mailService = new MailService();
var messageService = new MessageService();


videoEncoder.VideoEncoded += mailService.OnVideoEncoded;
videoEncoder.VideoEncoded += messageService.OnVideoEncoded;
videoEncoder.Encode(video);

namespace Events
{
    public class Video
    {
        public string Title { get; set; }
    }

    public class VideoEventArgs: EventArgs
    {
        public required Video Video { get; set; }
    }

    public class VideoEncoder
    {
        public event EventHandler<VideoEventArgs> VideoEncoded;

        public void Encode(Video video)
        {
            Console.WriteLine("Encoding video...");

            OnVideoEncoded(video);
        }

        protected virtual void OnVideoEncoded(Video video)
        {
            if(VideoEncoded != null) 
                VideoEncoded(this, new VideoEventArgs() { Video = video});
        }
    }

    public class MailService
    {
        public void OnVideoEncoded(object source, VideoEventArgs e)
        {
            Console.WriteLine("MailService: Sending email...: " + e.Video.Title);
        }
    }
    public class MessageService
    {
        public void OnVideoEncoded(object source, VideoEventArgs e)
        {
            Console.WriteLine("MessageService: Sending text...: " + e.Video.Title);
        }
    }


}