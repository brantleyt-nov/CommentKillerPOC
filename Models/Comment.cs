namespace CommentKillerPOC.Models
{
    public enum CommentType
    {
        Standard,
        Wireline,
        TagPlug,
        WellState
    }

    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public CommentType Type { get; set; }
    }
}
