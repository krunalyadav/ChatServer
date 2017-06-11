namespace ChatServer.Models
{
    public class ChatLog
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public int FromUserId { get; set; }

        public int ToUserId { get; set; }

        public virtual User FromUser { get; set; }

        public virtual User ToUser { get; set; }
    }
}
