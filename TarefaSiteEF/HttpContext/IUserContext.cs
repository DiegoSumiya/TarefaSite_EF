

namespace TarefaSiteEF.HttpContext
{
    public interface IUserContext
    {
        public bool IsAuthenticated { get; }
        public string GetUserEmail();
    }
}
