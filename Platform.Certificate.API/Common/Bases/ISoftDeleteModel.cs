namespace Platform.Certificate.API.Common.Bases
{
    public interface IEntity
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        bool IsDeleted { get; set; }
    }
}