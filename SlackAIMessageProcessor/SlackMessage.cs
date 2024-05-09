using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace SlackAIMessageProcessor
{
    public class SlackMessage
    {
        public string? Subtype { get; set; }
        public string? Text { get; set; }
        public string? Username { get; set; }
        public string? Type { get; set; }
        public string? Ts { get; set; }
        [JsonProperty(PropertyName = "bot_id")]
        public string? BotId { get; set; }
        [JsonProperty(PropertyName = "app_id")]
        public string? AppId { get; set; }
        public List<Block> Blocks { get; set; }
        public string? User { get; set; }
        [JsonProperty(PropertyName = "client_msg_id")]
        public string? ClientMsgId { get; set; }
        public string? Team { get; set; }
        [JsonProperty(PropertyName = "user_profile")]
        public UserProfile UserProfile { get; set; }
        public List<Reaction> Reactions { get; set; }
        [JsonProperty(PropertyName = "thread_ts")]
        public string? ThreadTs { get; set; }
        [JsonProperty(PropertyName = "reply_count")]
        public int? ReplyCount { get; set; }
        [JsonProperty(PropertyName = "reply_user_count")]
        public int? ReplyUsersCount { get; set; }
        [JsonProperty(PropertyName = "reply_users")]
        public List<string?> ReplyUsers { get; set; }
        public List<Reply> Replies { get; set; }
        [JsonProperty(PropertyName = "is_locked")]
        public bool? IsLocked { get; set; }
        public bool? Subscribed { get; set; }
        public Edited Edited { get; set; }
        [JsonProperty(PropertyName = "parent_user_id")]
        public string? ParentUserId { get; set; }
    }

    public class Block
    {
        public string? Type { get; set; }
        [JsonProperty(PropertyName = "block_id")]
        public string? BlockId { get; set; }
        public List<Element> Elements { get; set; }
        public int? Indent { get; set; }
        public int? Border { get; set; }
        public string? Style { get; set; }
    }

    public class Element
    {
        public string? Type { get; set; }
        public List<Element> Elements { get; set; }
        public string? Text { get; set; }
        public string? Url { get; set; }
        [JsonProperty(PropertyName = "user_id")]
        public string? UserId { get; set; }
        public string? Range { get; set; }
        [JsonProperty(PropertyName = "style_bold")]
        public bool? StyleBold { get; set; }
        [JsonProperty(PropertyName = "style_code")]
        public bool? StyleCode { get; set; }
    }

    public class UserProfile
    {
        [JsonProperty(PropertyName = "avatar_hash")]
        public string? AvatarHash { get; set; }
        [JsonProperty(PropertyName = "image_72")]
        public string? Image72 { get; set; }
        [JsonProperty(PropertyName = "first_name")]
        public string? FirstName { get; set; }
        [JsonProperty(PropertyName = "real_name")]
        public string? RealName { get; set; }
        [JsonProperty(PropertyName = "display_name")]
        public string? DisplayName { get; set; }
        public string? Team { get; set; }
        public string? Name { get; set; }
        [JsonProperty(PropertyName = "is_restricted")]
        public bool IsRestricted { get; set; }
        [JsonProperty(PropertyName = "is_ultra_restricted")]
        public bool IsUltraRestricted { get; set; }
    }

    public class Reaction
    {
        public string? Name { get; set; }
        public List<string?> Users { get; set; }
        public int Count { get; set; }
    }

    public class Reply
    {
        public string? User { get; set; }
        public string? Ts { get; set; }
    }

    public class Edited
    {
        public string? User { get; set; }
        public string? Ts { get; set; }
    }
}
