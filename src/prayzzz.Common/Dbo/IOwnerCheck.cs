namespace prayzzz.Common.Dbo
{
    public interface IOwnerCheck
    {
        bool IsOwnedByCurrentUser(OwnedDbo dbo);
    }
}