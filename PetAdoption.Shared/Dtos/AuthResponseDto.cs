using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Shared.Dtos
{

        public record AuthResponseDto (int UserId, string Name, string Token);
    
}
