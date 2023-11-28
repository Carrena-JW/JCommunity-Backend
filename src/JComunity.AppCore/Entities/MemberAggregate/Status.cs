namespace JComunity.AppCore.Entities.MemberAggregate;

public enum MemberStatus
{
    Active, // 활성
    InActive, // 비활성 (제재에 의한 비활성)
    Human, // 휴먼상태 ~개월 사용 이력 없음
    Withdrawal // 탈퇴 후 재가입 못함
}
