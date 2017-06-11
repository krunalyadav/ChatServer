using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatServer.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Guid Key { get; set; }

        [InverseProperty("FromUser")]
        public ICollection<ChatLog> FromUserChatLog { get; set; }

        [InverseProperty("ToUser")]
        public ICollection<ChatLog> ToUserChatLog { get; set; }
    }
}