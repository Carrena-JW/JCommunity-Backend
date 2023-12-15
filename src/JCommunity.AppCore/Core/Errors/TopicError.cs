namespace JCommunity.AppCore.Core.Errors;

public class TopicError
{
    
    public class NotFound : Error, IError
    {
        private const string TOPIC_NOT_FOUND_ERROR_CODE = "Topic.NotFound";
        public NotFound(string id) : base()
        {
            this.Message = "Topic not found.";

            WithMetadata("PropertyName", nameof(id));
            WithMetadata("AttemptedValue", id);
            WithMetadata("ErrorCode", TOPIC_NOT_FOUND_ERROR_CODE);
        }
    }

    public class TagNotFound : Error, IError
    {
        private const string TOPIC_TAG_NOT_FOUND_ERROR_CODE = "Topic.Tag.NotFound";
        public TagNotFound(string topicTagid) : base()
        {
            this.Message = "Topic tag not found.";

            WithMetadata("PropertyName", nameof(topicTagid));
            WithMetadata("AttemptedValue", topicTagid);
            WithMetadata("ErrorCode", TOPIC_TAG_NOT_FOUND_ERROR_CODE);
        }
    }


    public class NameNotUnique : Error, IError
    {
        private const string TOPIC_NAME_NOT_UNIQUE_ERROR_CODE = "Topic.Name.NotUnique";
        public NameNotUnique(string name) : base()
        {
            this.Message = "Topic Name is not unique.";

            WithMetadata("PropertyName", nameof(name));
            WithMetadata("AttemptedValue", name);
            WithMetadata("ErrorCode", TOPIC_NAME_NOT_UNIQUE_ERROR_CODE);
        }
    }

}

