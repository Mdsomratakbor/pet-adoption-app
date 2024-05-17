using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Shared.HubModel
{
    public interface IPetHubServer
    {
        Task ViewingThisPet(int petId);
        Task ReleaseViewingThisPet(int petId);
        Task PetAdopted(int petId);
    }
}
