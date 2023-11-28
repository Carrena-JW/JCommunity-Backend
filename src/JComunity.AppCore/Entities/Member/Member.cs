namespace JComunity.Domain.Entities.Users;

public class Member
{

    private MemberId Id { get; set; }
    private string Name { get; set; }
    private string NickName { get; set; }
    private string Email { get; set; }
    private string Password { get; set; }
}
