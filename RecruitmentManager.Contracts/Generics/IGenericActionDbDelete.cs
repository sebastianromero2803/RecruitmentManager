using System.Threading.Tasks;

namespace RecruitmentManager.Contracts.Generics
{
    public interface IGenericActionDbDelete
    {
        Task<bool> DeleteAsync(int id);
    }
}
