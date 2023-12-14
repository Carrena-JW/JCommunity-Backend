namespace JCommunity.AppCore.Entities.Topics;

public class TopicTagMap : IEntityMapTable
{ 
   public Guid  TopicId { get; private set; }
   public Guid TopicTagId { get; private set; }
}
 