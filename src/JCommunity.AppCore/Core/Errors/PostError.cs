namespace JCommunity.AppCore.Core.Errors;

public class PostError 
{
    public class NotFound : Error, IError
    {

        private const string POST_NOT_FOUND_ERROR_CODE = "Post.NotFound";
        public NotFound(string id) : base()
        {
            this.Message = "Post not found.";

            WithMetadata("PropertyName", nameof(id));
            WithMetadata("AttemptedValue", id);
            WithMetadata("ErrorCode", POST_NOT_FOUND_ERROR_CODE);
        }
    }

    public class NotImage : Error, IError
    {
          
        private const string POST_NOT_IMAGE_ERROR_CODE = "Post.NotImage";
        public NotImage(IFormFile image) : base()
        {
            this.Message = "Post image not found.";

            WithMetadata("PropertyName", nameof(image));
            WithMetadata("AttemptedValue", image);
            WithMetadata("ErrorCode", POST_NOT_IMAGE_ERROR_CODE);
        }
    }
}
 
