using RecruitmentManager.Contracts.Generics;
using RecruitmentManager.Entities.Entities;

namespace RecruitmentManager.Contracts.Repository
{
    public interface IClientRepository: IGenericActionDbAddUpdate<Client>, IGenericActionDbQuery<Client>
    {
    }
}
