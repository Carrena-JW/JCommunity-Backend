namespace JCommunity.AppCore.Core.Errors;

public class PostError 
{
    public class NotImage : Error, IError
    {
          
        private const string POST_NOT_IMAGE_ERROR_CODE = "Post.NotImage";
        public NotImage(IFormFile image) : base()
        {
            this.Message = "Member not found.";

            WithMetadata("PropertyName", nameof(image));
            WithMetadata("AttemptedValue", image);
            WithMetadata("ErrorCode", POST_NOT_IMAGE_ERROR_CODE);
        }
    }
}
 
