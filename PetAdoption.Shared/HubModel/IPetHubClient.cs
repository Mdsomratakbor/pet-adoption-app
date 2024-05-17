using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Shared.HubModel
{
    public interface IPetHubClient
    {
        Task PetIsBeingViewd(int petId);
        Task PetAdopted(int petId);
    }
}
